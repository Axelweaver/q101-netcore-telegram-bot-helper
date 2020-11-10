using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Q101.NetCoreTelegramBotHelper.Abstract;
using Q101.NetCoreTelegramBotHelper.Config.Abstract;
using Q101.NetCoreTelegramBotHelper.Models;

namespace Q101.NetCoreTelegramBotHelper.Concrete
{
    /// <summary>
    /// Telegram bot helper
    /// </summary>
    public class TelegramBotHelper : ITelegramBotHelper
    {
        /// <summary>
        /// Configuration
        /// </summary>
        private readonly ITelegramBotHelperConfig _telegramBotHelperConfig;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="telegramBotHelperConfig"></param>
        public TelegramBotHelper(ITelegramBotHelperConfig telegramBotHelperConfig)
        {
            _telegramBotHelperConfig = telegramBotHelperConfig
                                       ?? throw new ArgumentNullException(nameof(telegramBotHelperConfig));

            if (string.IsNullOrEmpty(_telegramBotHelperConfig.Token))
            {
                throw new ArgumentException("Missing bot token");
            }

            _telegramBotHelperConfig.BaseUrl = telegramBotHelperConfig.BaseUrl ?? "https://api.telegram.org/bot";
        }

        /// <inheritdoc />
        public async Task<ApiResponseDtoModel<MessageDtoModel>> SendPhoto(string chatId, string filePath)
        {
            var fileName = Path.GetFileName(filePath);

            var fileBytes = await File.ReadAllBytesAsync(filePath);

            return await SendPhoto(chatId, fileName, fileBytes);
        }

        /// <inheritdoc />
        public async Task<ApiResponseDtoModel<MessageDtoModel>> SendPhoto(string chatId, string fileName, byte[] bytes)
        {
            var url = $"{_telegramBotHelperConfig.BaseUrl}{_telegramBotHelperConfig.Token}/sendphoto";

            using (var httpClient = new HttpClient())
            {
                var formData = new MultipartFormDataContent();

                formData.Add(new StringContent(chatId), "chat_id");

                formData.Add(new ByteArrayContent(bytes, 0, bytes.Length), "photo", fileName);

                using (var request = await httpClient.PostAsync(url, formData))
                {
                    var responseContent = request.Content.ReadAsStringAsync().Result;

                    if (!request.IsSuccessStatusCode)
                    {
                        throw new Exception(
                            $"Server error response:\n{request.StatusCode} {request.ReasonPhrase}\n{responseContent}");
                    }

                    var responseObject =
                        JsonConvert.DeserializeObject<ApiResponseDtoModel<MessageDtoModel>>(
                            responseContent,
                            new UnixDateTimeConverter());

                    return responseObject;
                }
            }
        }

        /// <inheritdoc />
        public async Task<ApiResponseDtoModel<MessageDtoModel>> SendHtmlMessage(string chatId,
                                                                                string text,
                                                                                int? replyToMessageId = null,
                                                                                bool? disableNotification = null)
        {
            var message = GetSendMessageDtoModel(chatId, text, "HTML", replyToMessageId, disableNotification);

            return await SendMessage(message);
        }

        /// <inheritdoc />
        public async Task<ApiResponseDtoModel<MessageDtoModel>> SendMarkupMessage(string chatId,
            string text,
            int? replyToMessageId = null,
            bool? disableNotification = null)
        {
            var message = GetSendMessageDtoModel(chatId, text, "Markup", replyToMessageId, disableNotification);

            return await SendMessage(message);
        }

        /// <inheritdoc />
        public async Task<ApiResponseDtoModel<MessageDtoModel>> SendMessage(SendMessageDtoModel message)
        {
            using (var httpClient = GetHttpClient())
            {
                var requestUri = new Uri(
                    $"{_telegramBotHelperConfig.BaseUrl}{_telegramBotHelperConfig.Token}/sendmessage");

                var jsonContent = JsonConvert.SerializeObject(
                    message,
                    Newtonsoft.Json.Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                using (var request = await httpClient.PostAsync(requestUri, requestContent))
                {
                    var responseContent = request.Content.ReadAsStringAsync().Result;

                    if (!request.IsSuccessStatusCode)
                    {
                        throw new Exception(
                            $"Server error response:\n{request.StatusCode} {request.ReasonPhrase}\n{responseContent}");
                    }

                    var responseObject =
                        JsonConvert.DeserializeObject<ApiResponseDtoModel<MessageDtoModel>>(
                            responseContent,
                            new UnixDateTimeConverter());

                    return responseObject;
                }
            }
        }

        /// <inheritdoc />
        public async Task<ApiResponseDtoModel<MessageDtoModel>> SendSimpleMessage(string chatId,
                                                                                  string text,
                                                                                  int? replyToMessageId = null,
                                                                                  bool? disableNotification = null)
        {
            var message = GetSendMessageDtoModel(chatId, text, null, replyToMessageId, disableNotification);

            return await SendMessage(message);
        }


        private SendMessageDtoModel GetSendMessageDtoModel(string chatId,
                                                           string text,
                                                           string parseMode,
                                                           int? replyToMessageId = null,
                                                           bool? disableNotification = null)
        {
            var message = new SendMessageDtoModel
            {
                ChatId = chatId,
                Message = text,
                ParseMode = parseMode,
                ReplyToMessageId = replyToMessageId,
                DisableNotification = disableNotification
            };

            return message;
        }

        private HttpClient GetHttpClient()
        {
            var proxy =
                string.IsNullOrEmpty(_telegramBotHelperConfig.HttpProxyHost)
                    ? null
                    : new WebProxy
                    {
                        Address = new Uri(
                            $"http://{_telegramBotHelperConfig.HttpProxyHost}:{_telegramBotHelperConfig.HttpProxyPort}"),
                        BypassProxyOnLocal = false,
                        UseDefaultCredentials = false,
                        Credentials = string.IsNullOrEmpty(_telegramBotHelperConfig.HttpProxyUserName)
                            ? null
                            : new NetworkCredential(
                                userName: _telegramBotHelperConfig.HttpProxyUserName,
                                password: _telegramBotHelperConfig.HttpProxyUserPassword)
                    };

            var httpClient = proxy == null
                ? new HttpClient()
                : new HttpClient(new HttpClientHandler { Proxy = proxy });

            return httpClient;
        }
    }
}
