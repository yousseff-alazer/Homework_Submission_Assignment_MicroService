using Assignment.API.Assignment.DAL.DB;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace Assignment.API.Assignment.CommonDefinitions.Records
{
    public  class AssignmentRecord
    {
        [DefaultValue(0)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string StudentName { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string FileAttachment { get; set; }
        public string TeachersNote { get; set; }
        public long? GradeId { get; set; }
        public string FinalGrade { get; set; }
        public IFormFile FormFile { get; set; }
        public DateTime? To { get; set; }
        public DateTime? From { get; set; }
        public bool? Ungraded { get; set; }
        public bool? Incomplete { get; set; }
    }
}
