using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.API.Assignment.CommonDefinitions.Records;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Assignment.BL.Services.Managers
{
    public class AssignmentServiceManager
    {
        private const string AssignmentPath = "{0}/ContentFiles/Assignment/{1}";

        public static API.Assignment.DAL.DB.Assignment AddOrEditAssignment(string baseUrl, AssignmentRecord record, API.Assignment.DAL.DB.Assignment oldAssignment = null)
        {
            if (oldAssignment == null)//new assignment
            {
                oldAssignment = new API.Assignment.DAL.DB.Assignment();
                oldAssignment.CreationDate = DateTime.Now;
            }
            else
            {
                oldAssignment.ModificationDate = DateTime.Now;
            }
            if (!string.IsNullOrWhiteSpace(record.Name))
            {
                oldAssignment.Name = record.Name;
            }
            if (!string.IsNullOrWhiteSpace(record.StudentName))
            {
                oldAssignment.StudentName = record.StudentName;
            }
            if (record.SubmissionDate != null)
            {
                oldAssignment.SubmissionDate = record.SubmissionDate;
            }
            if (!string.IsNullOrWhiteSpace(record.TeachersNote))
            {
                oldAssignment.TeachersNote = record.TeachersNote;
            }
            if (record.GradeId != null&&record.GradeId>0)
            {
                oldAssignment.GradeId = record.GradeId;
            }
            //upload
            if (record.FormFile != null)
            {
                var allowedExtensions = new[] { ".pdf", ".PDF", ".jpeg", ".JPEG" ,".jpg", ".JPG", ".png", ".PNG" };
                var extension = Path.GetExtension(record.FormFile.FileName);
                if (allowedExtensions.Contains(extension))
                {
                    var file = record.FormFile.OpenReadStream();
                    var fileName = record.FormFile.FileName;
                    if (file.Length > 0)
                    {
                        var newFileName = Guid.NewGuid().ToString() + "-" + fileName;
                        var physicalPath = string.Format(AssignmentPath, Directory.GetCurrentDirectory() + "/wwwroot", newFileName);
                        string dirPath = Path.GetDirectoryName(physicalPath);

                        if (!Directory.Exists(dirPath))
                            Directory.CreateDirectory(dirPath);
                        var virtualPath = string.Format(AssignmentPath, baseUrl, newFileName);

                        using (var stream = new FileStream(physicalPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        oldAssignment.FileAttachment = virtualPath;
                    }
                }
            }
            return oldAssignment;
        }

        public static IQueryable<AssignmentRecord> ApplyFilter(IQueryable<AssignmentRecord> query, AssignmentRecord assignmentRecord)
        {
            if (assignmentRecord.Id > 0)
                query = query.Where(c => c.Id == assignmentRecord.Id);
            if (assignmentRecord.GradeId > 0)
                query = query.Where(c => c.GradeId == assignmentRecord.GradeId);


            if (!string.IsNullOrWhiteSpace(assignmentRecord.Name))
                query = query.Where(c => c.Name != null && c.Name.Trim().Contains(assignmentRecord.Name.Trim()));

            if (!string.IsNullOrWhiteSpace(assignmentRecord.StudentName))
                query = query.Where(c => c.StudentName != null && c.StudentName.Trim().Contains(assignmentRecord.StudentName.Trim()));

            if (!string.IsNullOrWhiteSpace(assignmentRecord.TeachersNote))
                query = query.Where(c => c.TeachersNote != null && c.TeachersNote.Trim().Contains(assignmentRecord.TeachersNote.Trim()));
            if (!string.IsNullOrWhiteSpace(assignmentRecord.FinalGrade))
                query = query.Where(c => c.FinalGrade != null && c.FinalGrade.Trim().Contains(assignmentRecord.FinalGrade.Trim()));

            if (assignmentRecord.To!=null&& assignmentRecord.To>DateTime.MinValue)
                query = query.Where(c => c.SubmissionDate != null && c.SubmissionDate.Value<=assignmentRecord.To.Value);

            if (assignmentRecord.From != null && assignmentRecord.From > DateTime.MinValue)
                query = query.Where(c => c.SubmissionDate != null && c.SubmissionDate.Value >= assignmentRecord.From.Value);

            if (assignmentRecord.Ungraded != null)
                if (assignmentRecord.Ungraded==true)
                {
                    query = query.Where(c => c.SubmissionDate != null && c.GradeId==null);
                }
                else
                {
                    query = query.Where(c => c.SubmissionDate != null && c.GradeId != null);
                }
            if (assignmentRecord.Incomplete != null&& assignmentRecord.Incomplete == true)
                   query = query.Where(c => c.SubmissionDate != null && c.GradeId == null&&!string.IsNullOrWhiteSpace(c.TeachersNote));

            return query;
        }
    }
}