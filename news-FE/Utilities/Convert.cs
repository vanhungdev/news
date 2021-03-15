
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace news_FE.Utilities
{
    public static class JConvert
    {
        public static T jsonconvert<T>(string getJsonRepons)
        {
            T result;
            try
            {
                result = JsonConvert.DeserializeObject<T>(getJsonRepons);
            }
            catch
            {
                throw;
            }
            return result;
        }
        public static IEnumerable<T> jsonconvertList<T>(string getJsonRepons)
        {

            try
            {
                return JsonConvert.DeserializeObject<List<T>>(getJsonRepons);
            }
            catch
            {
                return null;
                throw;

            }
        }
    }
}