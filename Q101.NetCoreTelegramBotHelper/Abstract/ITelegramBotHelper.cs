﻿using System.Threading.Tasks;
using Q101.NetCoreTelegramBotHelper.Models;

namespace Q101.NetCoreTelegramBotHelper.Abstract
{
    /// <summary>
    /// Telegram bot helper for send message etc
    /// </summary>
    public interface ITelegramBotHelper
    {
        /// <summary>
        /// Send photo from file
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task<ApiResponseDtoModel<MessageDtoModel>> SendPhoto(string chatId, string filePath);

        /// <summary>
        /// Send photo from bytes of file
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="fileName"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        Task<ApiResponseDtoModel<MessageDtoModel>> SendPhoto(string chatId, string fileName, byte[] bytes);

        /// <summary>
        /// Send message
        /// </summary>
        /// <returns></returns>
        Task<ApiResponseDtoModel<MessageDtoModel>> SendMessage(SendMessageDtoModel message);

        /// <summary>
        /// Send simple text message
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format @channelusername</param>
        /// <param name="text">Text of the message to be sent</param>
        /// <param name="replyToMessageId">[Optional] If the message is a reply, ID of the original message</param>
        /// <param name="disableNotification">[Optional] Sends the message silently. Users will receive a notification with no sound.</param>
        /// <returns></returns>
        Task<ApiResponseDtoModel<MessageDtoModel>> SendSimpleMessage(string chatId,
                                                                     string text,
                                                                     int? replyToMessageId = null,
                                                                     bool? disableNotification = null);

        /// <summary>
        /// Send text message with markup
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format @channelusername</param>
        /// <param name="text">Text of the message to be sent</param>
        /// <param name="replyToMessageId">[Optional] If the message is a reply, ID of the original message</param>
        /// <param name="disableNotification">[Optional] Sends the message silently. Users will receive a notification with no sound.</param>
        /// <returns></returns>
        Task<ApiResponseDtoModel<MessageDtoModel>> SendMarkupMessage(string chatId,
                                                                     string text,
                                                                     int? replyToMessageId = null,
                                                                     bool? disableNotification = null);

        /// <summary>
        /// Send text message with html
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format @channelusername</param>
        /// <param name="text">Text of the message to be sent</param>
        /// <param name="replyToMessageId">[Optional] If the message is a reply, ID of the original message</param>
        /// <param name="disableNotification">[Optional] Sends the message silently. Users will receive a notification with no sound.</param>
        Task<ApiResponseDtoModel<MessageDtoModel>> SendHtmlMessage(string chatId,
                                                                   string text,
                                                                   int? replyToMessageId = null,
                                                                   bool? disableNotification = null);

    }
}
