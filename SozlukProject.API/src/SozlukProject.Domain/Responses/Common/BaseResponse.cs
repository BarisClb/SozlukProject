using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Domain.Responses
{
    public abstract class BaseResponse
    {
        public abstract string Message { get; set; }
        public abstract bool Success { get; set; }
    }
}
