using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain
{
    public class ServiceResult<T> where T:class
    {
        public bool IsSuccess { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }

        public ServiceResultType Type { get; set; }

        public ServiceResult<T> Ok()
        {
            return Ok("Operation completed successfully.");
        }

        public ServiceResult<T> Ok(string message)
        {
            IsSuccess = true;
            return SetMessage(null,message, ServiceResultType.Success);
        }

        public ServiceResult<T> Ok(T data,string message)
        {
            IsSuccess = true;
            return SetMessage(data, message, ServiceResultType.Success);
        }

        public ServiceResult<T> Info(string message, bool isSuccess = true)
        {
            IsSuccess = isSuccess;
            return SetMessage(null, message, ServiceResultType.Info);
        }

        public ServiceResult<T> Info(T data,string message, bool isSuccess = true)
        {
            IsSuccess = isSuccess;
            return SetMessage(data, message, ServiceResultType.Info);
        }

        public ServiceResult<T> Warning(string message, bool isSuccess = true)
        {
            IsSuccess = isSuccess;
            return SetMessage(null, message, ServiceResultType.Warning);
        }

        public ServiceResult<T> Warning(T data,string message, bool isSuccess = true)
        {
            IsSuccess = isSuccess;
            return SetMessage(data, message, ServiceResultType.Warning);
        }

        public ServiceResult<T> Error(T data)
        {
            this.Data = data;
            return Error(data,"An error has ocurred while processing your request.");
        }

        public ServiceResult<T> Error(T data,string message)
        {
            IsSuccess = false;
            return SetMessage(data, message, ServiceResultType.Error);
        }
        public ServiceResult<T> Error(string message)
        {
            IsSuccess = false;
            return SetMessage(null, message, ServiceResultType.Error);
        }

        public ServiceResult<T> SetMessage(T data,string message, ServiceResultType type)
        {
            Message = message;
            Type = type;
            Data = data;
            return this;
        }
    }
    public enum ServiceResultType
    {
        Info = 100,
        Success = 200,
        Warning = 202,
        Error = 500
    }
}
