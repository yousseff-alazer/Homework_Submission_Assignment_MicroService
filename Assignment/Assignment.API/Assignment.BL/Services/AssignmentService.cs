using Assignment.API.Assignment.CommonDefinitions.Records;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Assignment.API.Assignment.CommonDefinitions.Requests;
using Assignment.API.Assignment.CommonDefinitions.Responses;
using System.Net;
using Assignment.BL.Services.Managers;

namespace Assignment.BL.Services
{
    public class AssignmentService : BaseService
    {
        public static AssignmentResponse ListAssignment(AssignmentRequest request)
        {
            var res = new AssignmentResponse();
            RunBase(request, res, (AssignmentRequest req) =>
             {
                 try
                 {
                     var query = request._context.Assignments.Where(c => !c.IsDeleted.Value).Select(c => new AssignmentRecord
                     {
                         Id = c.Id,
                         Name =  c.Name,
                         StudentName = c.StudentName,
                         SubmissionDate = c.SubmissionDate,
                         FileAttachment = c.FileAttachment,
                         TeachersNote = c.TeachersNote,
                         GradeId = c.GradeId,
                         FinalGrade=c.Grade!=null?c.Grade.Name:""
                     });

                     if (request.AssignmentRecord != null)
                         query = AssignmentServiceManager.ApplyFilter(query, request.AssignmentRecord);

                     res.TotalCount = query.Count();

                     query = OrderByDynamic(query, request.OrderByColumn, request.IsDesc);
                     query = request.PageSize > 0 ? ApplyPaging(query, request.PageSize, request.PageIndex) : ApplyPaging(query, request.DefaultPageSize, 0);

                     res.AssignmentRecords = query.ToList();
                     res.Message = HttpStatusCode.OK.ToString();
                     res.Success = true;
                     res.StatusCode = HttpStatusCode.OK;
                 }
                 catch (Exception ex)
                 {
                     res.Message = ex.Message;
                     res.Success = false;
                 }
                 return res;
             });
            return res;
        }

        public static AssignmentResponse DeleteAssignment(AssignmentRequest request)
        {
            var res = new AssignmentResponse();
            RunBase(request, res, (AssignmentRequest req) =>
             {
                 try
                 {
                     var model = request.AssignmentRecord;
                     var assignment = request._context.Assignments.FirstOrDefault(c => !c.IsDeleted.Value && c.Id == model.Id);
                     if (assignment != null)
                     {
                         //update assignment IsDeleted
                         assignment.IsDeleted = true;
                         assignment.ModificationDate = DateTime.Now;
                         request._context.SaveChanges();

                         res.Message = HttpStatusCode.OK.ToString();
                         res.Success = true;
                         res.StatusCode = HttpStatusCode.OK;
                     }
                     else
                     {
                         res.Message = "Invalid assignment";
                         res.Success = false;
                     }
                 }
                 catch (Exception ex)
                 {
                     res.Message = ex.Message;
                     res.Success = false;
                 }
                 return res;
             });
            return res;
        }

        public static AssignmentResponse EditAssignment(AssignmentRequest request)
        {
            var res = new AssignmentResponse();
            RunBase(request, res, (AssignmentRequest req) =>
            {
                try
                {
                    var model = request.AssignmentRecord;
                    var assignment = request._context.Assignments.Find(model.Id);
                    if (assignment != null)
                    {
                        //update whole assignment
                        assignment = AssignmentServiceManager.AddOrEditAssignment(request.BaseUrl, request.AssignmentRecord, assignment);
                        request._context.SaveChanges();

                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Invalid assignment";
                        res.Success = false;
                    }
                }
                catch (Exception ex)
                {
                    res.Message = ex.Message;
                    res.Success = false;
                }
                return res;
            });
            return res;
        }

        public static AssignmentResponse AddAssignment(AssignmentRequest request)
        {
            var res = new AssignmentResponse();
            RunBase(request, res, (AssignmentRequest req) =>
            {
                try
                {
                    var AssignmentExist = request._context.Assignments.Any(m => m.Name.ToLower() == request.AssignmentRecord.Name.ToLower()&& m.StudentName.ToLower() == request.AssignmentRecord.StudentName.ToLower() && !m.IsDeleted.Value);
                    if (!AssignmentExist)
                    {
                        var assignment = AssignmentServiceManager.AddOrEditAssignment(request.BaseUrl, request.AssignmentRecord);
                        request._context.Assignments.Add(assignment);
                        request._context.SaveChanges();
                        res.Message = HttpStatusCode.OK.ToString();
                        res.Success = true;
                        res.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        res.Message = "Assignment already exist";
                        res.Success = false;
                    }
                }
                catch (Exception ex)
                {
                    res.Message = ex.Message + " " + ex.StackTrace + " " + ex.InnerException +ex.Data;
                    res.Success = false;
                }
                return res;
            });
            return res;
        }
    }
}