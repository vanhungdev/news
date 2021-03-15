using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace news_FE.models
{
    public  class CUDResult
    {
     
        //return when action
        public int status { get; set; }
        public string message { get; set; }
        public CUDResult(int status, string message)
        {
            this.status = status;
            this.message = message;
        }

     
    }
}
