﻿using Newtonsoft.Json;

namespace HotelMgt.Dtos
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string Errors { get; set; }

        public Response(int statusCode, bool success, string msg, T data, string errors)
        {
            Data = data;
            Succeeded = success;
            StatusCode = statusCode;
            Message = msg;
            Errors = errors;
        }
        public Response()
        {
        }

        /// <summary>
        /// Sets the data to the appropriate response
        /// at run time
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Response<T> Fail(string errorMessage)
        {
            return new Response<T> { Succeeded = false, Message = errorMessage };
        }
        public static Response<T> Success(T data)
        {
            return new Response<T> { Succeeded = true, Data = data };
        }
        public override string ToString() => JsonConvert.SerializeObject(this);
       
    }
}
