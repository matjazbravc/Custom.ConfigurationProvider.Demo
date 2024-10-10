using Newtonsoft.Json;
using System;

namespace Custom.Configuration.Provider.Demo.Errors;

[Serializable]
[JsonObject(IsReference = false)]
public class ApiError(int statusCode, string message)
{
  [JsonProperty("statusCode")]
  public int StatusCode { get; set; } = statusCode;

  [JsonProperty("error")]
  public string Message { get; set; } = message;

  public override string ToString()
  {
    return JsonConvert.SerializeObject(this);
  }
}
