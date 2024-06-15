using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.Helper
{
    public class ServiceResponse<T>
    {
        private ServiceResponse(int statusCode, List<string> errors, string message = null)
        {
            StatusCode = statusCode;
            Errors = errors;
            Message = message;
        }

        private ServiceResponse(Exception ex)
        {
            Errors = new List<string> { ex.Message.ToString() };
        }

        private ServiceResponse(int statusCode, T data, string message = null)
        {
            StatusCode = statusCode;
            Data = data;
            Message = message;
        }

        public bool Success
        {
            get
            {
                return Errors == null || Errors.Count == 0;
            }
        }

        public T Data { get; set; }
        public int StatusCode { get; set; } = 200;
        public List<string> Errors { get; set; } = new List<string>();
        public string Message { get; set; } // Yeni eklenen özellik

        public static ServiceResponse<T> ReturnException(Exception ex)
        {
            return new ServiceResponse<T>(ex);
        }

        public static ServiceResponse<T> ReturnFailed(int statusCode, List<string> errors)
        {
            return new ServiceResponse<T>(statusCode, errors);
        }

        public static ServiceResponse<T> ReturnFailed(int statusCode, string errorMessage)
        {
            return new ServiceResponse<T>(statusCode, new List<string> { errorMessage });
        }

        public static ServiceResponse<T> ReturnSuccess(string message = null)
        {
            return new ServiceResponse<T>(200, null, message);
        }

        public static ServiceResponse<T> ReturnResultWith200(T data, string message = null)
        {
            return new ServiceResponse<T>(200, data, message);
        }

        public static ServiceResponse<T> ReturnResultWith201(T data, string message = null)
        {
            return new ServiceResponse<T>(201, data, message);
        }

        public static ServiceResponse<T> ReturnResultWith204(string message = null)
        {
            return new ServiceResponse<T>(204, null, message);
        }

        public static ServiceResponse<T> Return500(string message = "An unexpected fault happened. Try again later.")
        {
            return new ServiceResponse<T>(500, new List<string> { message }, message);
        }

        public static ServiceResponse<T> Return409(string message)
        {
            return new ServiceResponse<T>(409, new List<string> { message }, message);
        }

        public static ServiceResponse<T> Return422(string message)
        {
            return new ServiceResponse<T>(422, new List<string> { message }, message);
        }

        public static ServiceResponse<T> Return404(string message = "Not Found")
        {
            return new ServiceResponse<T>(404, new List<string> { message }, message);
        }

        public static ServiceResponse<T> ReturnError(string message)
        {
            return new ServiceResponse<T>(400, new List<string> { message }, message);
        }
    }
}
