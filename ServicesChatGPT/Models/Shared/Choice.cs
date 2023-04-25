using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesChatGPT.Models.Shared
{
    public class Choice
    {
        public int Index { get; set; }
        public Message Message { get; set; }
        public string Finish_reason { get; set; }
    }
}
