﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ChatRequest
    {
        public string Message { get; set; }
        public string UserName { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
