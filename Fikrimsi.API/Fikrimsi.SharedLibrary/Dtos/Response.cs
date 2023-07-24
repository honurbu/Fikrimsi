using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fikrimsi.SharedLibrary.Dtos
{
    public class Response<T> where T : class
    {
        public T Data { get; private set; }
        public int StatusCode { get; private set; }
        public ErrorDto Errors { get; private set; }

        [JsonIgnore]
        public bool IsSuccessful { get; set; }

        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Fail(ErrorDto error, int statusCode)
        {
            return new Response<T> { Errors = error, StatusCode = statusCode, IsSuccessful = false };
        }

        public static Response<T> Fail(string errorMessage, int statusCode, bool isShow)
        {
            var errorDto = new ErrorDto(errorMessage, isShow);
            return new Response<T> { Errors = errorDto, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
