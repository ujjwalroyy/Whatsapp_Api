using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCrud.Models
{
    public class WhatsAppMessage
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; }
        public string TemplateName { get; set; } = "hello_world";
        public string LanguageCode { get; set; } = "en_US";

 
    }
}