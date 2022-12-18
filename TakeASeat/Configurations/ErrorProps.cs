using Newtonsoft.Json;

namespace TakeASeat.Configurations
{
    public class ErrorProps
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
