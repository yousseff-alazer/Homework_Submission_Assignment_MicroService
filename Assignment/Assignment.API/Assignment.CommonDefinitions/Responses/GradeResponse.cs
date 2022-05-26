using Assignment.API.Assignment.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Assignment.API.Assignment.CommonDefinitions.Responses
{
    public class GradeResponse : BaseResponse
    {
        [JsonProperty("Data")]
        public List<GradeRecord> GradeRecords { get; set; }
    }
}
