using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace news_FE.Models
{
    public class ObjectResult<T>
    {
        public int code { get; set; }
        public ResultMessage message { get; set; }
        public dataResult<T> data { get; set; }
    }
    public class dataResult<T>{
        public string token { get; set; }
        public T data { get; set; }
    }
    public class ResultMessage
    {
        public string Title { set; get; }
        public string Message { set; get; }
        public string ExMessage { get; set; }
        public int Popup { get; set; }
    }

}