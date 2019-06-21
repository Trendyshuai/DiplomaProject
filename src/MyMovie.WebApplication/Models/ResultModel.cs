using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MyMovie.WebApplication.Models
{
    public class ResultModel
    {

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}