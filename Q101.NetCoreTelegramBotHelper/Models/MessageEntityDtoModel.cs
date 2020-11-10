namespace Q101.NetCoreTelegramBotHelper.Models
{
    /// <summary>
    /// Message Entity DTO model
    /// </summary>
    public class MessageEntityDtoModel
    {
        /// <summary>
        /// Offset
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Length
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
    }
}
