using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCrud.Dto
{
    public class SendMediaRequest
    {
        public string PhoneNumber { get; set; }
        public string MediaId { get; set; }
        public string Caption { get; set; }
    }

}