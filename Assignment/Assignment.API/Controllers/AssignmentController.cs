using Assignment.API.Assignment.CommonDefinitions.Records;
using Assignment.API.Assignment.CommonDefinitions.Requests;
using Assignment.API.Assignment.CommonDefinitions.Responses;
using Assignment.API.Assignment.DAL.DB;
using Assignment.BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AssignmentController : ControllerBase
    {
        private readonly homework_dbContext _context;

        public AssignmentController(homework_dbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAll")]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var assignmentResponse = new AssignmentResponse();
            try
            {
                var assignmentRequest = new AssignmentRequest
                {
                    _context = _context
                };
                assignmentResponse = AssignmentService.ListAssignment(assignmentRequest);
            }
            catch (Exception ex)
            {
                assignmentResponse.Message = ex.Message;
                assignmentResponse.Success = false;
               
            }
            return Ok(assignmentResponse);
        }

        /// <summary>
        /// Return Assignment With id .
        /// </summary>
        [HttpGet("{id}", Name = "GetAssignment")]
        [Produces("application/json")]
        public IActionResult GetAssignment(int id)
        {
            var assignmentResponse = new AssignmentResponse();
            try
            {
                var assignmentRequest = new AssignmentRequest
                {
                    _context = _context,
                    AssignmentRecord = new AssignmentRecord
                    {
                        Id = id
                    }
                };
                assignmentResponse = AssignmentService.ListAssignment(assignmentRequest);
            }
            catch (Exception ex)
            {
                assignmentResponse.Message = ex.Message;
                assignmentResponse.Success = false;
               
            }
            return Ok(assignmentResponse);
        }

        /// <summary>
        /// Return List Of Assignments With filter valid and any  needed filter like by assignment name, date range (to - from), 
        /// and individual student name, grade (A - F, incomplete, ungraded)  .
        /// examble filter by student name
        /// {
        /// "assignmentRecord": {
        ///"studentName": "josef"
        ///}
        ///}
        /// </summary>
        [HttpPost]
        [Route("GetFiltered")]
        [Produces("application/json")]
        public IActionResult GetFiltered([FromBody] AssignmentRequest model)
        {
            var assignmentResponse = new AssignmentResponse();
            try
            {
                if (model == null)
                {
                    model = new AssignmentRequest();
                }
                model._context = _context;
                
                assignmentResponse = AssignmentService.ListAssignment(model);

            }
            catch (Exception ex)
            {
                assignmentResponse.Message = ex.Message;
                assignmentResponse.Success = false;
               
            }

            return Ok(assignmentResponse);
        }

        /// <summary>
        /// Creates Assignment, Uncheck Send empty value in Id and other unwanted properties.
        /// </summary>
        [HttpPost]
        [Route("Add")]
        [Produces("application/json")]
        public IActionResult Add([FromForm] AssignmentRecord model)
        {
            var assignmentResponse = new AssignmentResponse();
            try
            {
                if (model == null)
                {
                    assignmentResponse.Message = "Empty Body";
                    assignmentResponse.Success = false;
                    return Ok(assignmentResponse);
                }
                var assignmentRequest = new AssignmentRequest
                {
                    _context = _context,
                    AssignmentRecord =model,
                    BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase
                 };
                assignmentResponse = AssignmentService.AddAssignment(assignmentRequest);
            }
            catch (Exception ex)
            {
                assignmentResponse.Message = ex.Message;
                assignmentResponse.Success = false;
               
            }

            return Ok(assignmentResponse);
        }

        /// <summary>
        /// Update Assignment,  Uncheck Send empty value in Id and other unwanted properties.
        /// </summary>
        [HttpPost]
        [Route("Edit")]
        [Produces("application/json")]
        public IActionResult Edit([FromForm] AssignmentRecord  model)
        {
            var assignmentResponse = new AssignmentResponse();
            try
            {
                if (model == null)
                {
                    assignmentResponse.Message = "Empty Body";
                    assignmentResponse.Success = false;
                    return Ok(assignmentResponse);
                }
                var assignmentRequest = new AssignmentRequest
                {
                    _context = _context,
                    AssignmentRecord = model,
                    BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase
                };
                assignmentResponse = AssignmentService.EditAssignment(assignmentRequest);
            }
            catch (Exception ex)
            {
                assignmentResponse.Message = ex.Message;
                assignmentResponse.Success = false;
               
            }
            return Ok(assignmentResponse);
        }

        /// <summary>
        /// Remove Assignment .
        /// </summary>
        [HttpPost]
        [Route("Delete")]
        [Produces("application/json")]
        public IActionResult Delete([FromBody] AssignmentRecord model)
        {
            var assignmentResponse = new AssignmentResponse();
            try
            {
                if (model == null)
                {
                    assignmentResponse.Message = "Empty Body";
                    assignmentResponse.Success = false;
                    return Ok(assignmentResponse);
                }

                var assignmentRequest = new AssignmentRequest
                {
                    _context = _context,
                    AssignmentRecord = model,
                    BaseUrl = Request.Scheme + "://" + Request.Host.Value + Request.PathBase
                };
                assignmentResponse = AssignmentService.DeleteAssignment(assignmentRequest);
            }
            catch (Exception ex)
            {
                assignmentResponse.Message = ex.Message;
                assignmentResponse.Success = false;
               
            }

            return Ok(assignmentResponse);
        }

    }
}