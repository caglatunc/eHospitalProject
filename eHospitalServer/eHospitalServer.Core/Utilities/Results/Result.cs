using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eHospitalServer.Core.Utilities.Results;
public sealed class Result<T>
{
    public T? Data { get; set; }
    public List<string>? ErrorMessages { get; set; }
    public bool IsSuccess { get; set; } = true;
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

    public Result(T data)
    {
        Data = data;
    }

    public Result(HttpStatusCode statusCode, List<string> errorMessages)
    {
        IsSuccess = false;
        StatusCode = statusCode;
        ErrorMessages = errorMessages;
    }

    public Result(HttpStatusCode statusCode, string errorMessage)
    {
        IsSuccess = false;
        StatusCode = statusCode;
        ErrorMessages = new() { errorMessage };
    }

    public static implicit operator Result<T>(T data)
    {
        return new(data);
    }
    public static implicit operator Result<T>((HttpStatusCode statusCode, List<string> errorMessages) parameters)
    {
        return new(parameters.statusCode, parameters.errorMessages);
    }
    public static implicit operator Result<T>((HttpStatusCode statusCode, string errorMessage) parameters)
    {
        return new(parameters.statusCode, parameters.errorMessage);
    }
}
