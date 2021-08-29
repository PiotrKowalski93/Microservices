using Newtonsoft.Json;

namespace Employees.Infrastructure.ExceptionDetails
{
    public class GeneralExceptionDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
