using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ManagementUsers.BLL.DTOs.Response
{
    public class Response<T>
    {
        [JsonConstructor]
        public Response()
        {
            _code = (int)HttpStatusCode.OK;
        }

        public Response(T? data, int code = (int)HttpStatusCode.OK, string? message = null)
        {
            Data = data;
            _code = code;
            Message = message;
        }

        private int _code = (int)HttpStatusCode.OK;

        [JsonIgnore]
        public bool IsSuccess => _code is >= 200 and <= 299;

        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
