using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Api.Models.Error
{
    public class ErrorResultBaseModel
    {
        [JsonProperty("message")]
        public string Message { get; protected set; }

        [JsonProperty("errors")]
        public List<ErrorResultModel> Errors { get; protected set; }
    }

    public class ErrorResultModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        [JsonProperty("message")]
        public string Message { get; }

        public ErrorResultModel(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}
