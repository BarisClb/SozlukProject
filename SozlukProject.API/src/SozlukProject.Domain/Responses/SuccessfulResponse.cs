using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Domain.Responses
{
    public class SuccessfulResponse<T> : BaseResponse
    {
        public override string Message { get; set; } = "Successful Request.";
        public override bool Success { get; set; } = true;
        public T? Data { get; set; }

        public SuccessfulResponse(string message, T data)
        {
            Message = message;
            Data = data;
        }
        public SuccessfulResponse(string message)
        {
            Message = message;
        }
        public SuccessfulResponse(T data)
        {
            Data = data;
        }
    }
}
