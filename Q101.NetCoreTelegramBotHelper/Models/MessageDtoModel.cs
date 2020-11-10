using System;
using System.Collections.Generic;

namespace Q101.NetCoreTelegramBotHelper.Models
{
    /// <summary>
    /// Message DTO model
    /// </summary>
    public class MessageDtoModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// chat
        /// </summary>
        public ChatDtoModel Chat { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<MessageEntityDtoModel> Entities { get; set; }


    }
}
