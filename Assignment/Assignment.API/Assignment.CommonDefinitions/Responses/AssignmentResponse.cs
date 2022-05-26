using Assignment.API.Assignment.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Assignment.API.Assignment.CommonDefinitions.Responses
{
    public class AssignmentResponse : BaseResponse
    {
        [JsonProperty("Data")]
        public List<AssignmentRecord> AssignmentRecords { get; set; }
    }
}
