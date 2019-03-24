using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DBServerAPI.Models
{
    public class Response
    {
        public object Result { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        public Response SetError(string error)
        {
            this.Errors.Add(error);
            return this;
        }
        public Response SetResponse(object result)
        {
            this.Result = result;
            return this;
        }

        public Response SetStatusCode(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
            return this;
        }

        public bool HasErrors()
        {
            return Errors.Count > 0 ? true : false;
        }
    }
}
