using Newtonsoft.Json;

namespace TakeASeat.ProgramConfigurations.DTO
{
    public class ErrorProps
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
