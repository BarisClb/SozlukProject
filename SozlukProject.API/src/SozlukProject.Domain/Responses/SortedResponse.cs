using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Domain.Responses
{
    public class SortedResponse<T, G> : SuccessfulResponse<T>
    {
        public G SortInfo { get; set; }

        public SortedResponse(string message, T data, G pageInfo) : base(message, data)
        {
            Message = message;
            Data = data;
            SortInfo = pageInfo;
        }
        public SortedResponse(T data, G pageInfo) : base(data)
        {
            Data = data;
            SortInfo = pageInfo;
        }
    }
}
