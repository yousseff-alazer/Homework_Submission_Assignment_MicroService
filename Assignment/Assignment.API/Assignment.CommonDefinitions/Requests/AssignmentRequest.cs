using Assignment.API.Assignment.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.API.Assignment.CommonDefinitions.Requests
{
    public class AssignmentRequest : BaseRequest
    {

        public AssignmentRecord AssignmentRecord { get; set; }
    }
}
