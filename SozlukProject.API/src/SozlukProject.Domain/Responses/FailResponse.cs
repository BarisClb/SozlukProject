using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Domain.Responses
{
    public class FailResponse : BaseResponse
    {
        public override string Message { get; set; } = "Failed Request.";
        public override bool Success { get; set; } = false;

        public FailResponse(string message)
        {
            Message = message;
        }
        public FailResponse()
        {

        }
    }
}
