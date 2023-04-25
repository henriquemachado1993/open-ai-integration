using ServicesChatGPT.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesChatGPT.Models.Shared
{
    public class Message
    {
        /// <summary>
        /// The role of the author of this message. One of "system", "user", or "assistant".
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// The contents of the message.
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// The name of the author of this message. May contain a-z, A-Z, 0-9, and underscores, with a maximum length of 64 characters.
        /// </summary>
        public string Name { get; set; }
    }
}
