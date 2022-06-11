using SozlukProject.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Dtos.Response
{
    public class LoginResponse<T> : BaseResponse
    {
        public override string Message { get; set; }
        public override bool Success { get; set; }
        public T? Data { get; set; }
        public string? Jwt { get; set; }

        // Failed
        public LoginResponse()
        {
            Message = "Login failed.";
            Success = false;
        }

        public LoginResponse(string message)
        {
            Message = message;
            Success = false;
        }

        // Succesfull
        public LoginResponse(T data, string jwt)
        {
            Message = "Login successful.";
            Success = true;
            Data = data;
            Jwt = jwt;
        }

        public LoginResponse(string message, T data, string jwt)
        {
            Message = message;
            Success = true;
            Data = data;
            Jwt = jwt;
        }
    }
}
