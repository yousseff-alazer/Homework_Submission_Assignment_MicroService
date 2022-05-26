using System;
using System.Collections.Generic;

#nullable disable

namespace Assignment.API.Assignment.DAL.DB
{
    public partial class Assignment
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string StudentName { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModificationDate { get; set; }
        public long? ModifiedBy { get; set; }
        public string FileAttachment { get; set; }
        public string TeachersNote { get; set; }
        public long? GradeId { get; set; }

        public virtual Grade Grade { get; set; }
    }
}
