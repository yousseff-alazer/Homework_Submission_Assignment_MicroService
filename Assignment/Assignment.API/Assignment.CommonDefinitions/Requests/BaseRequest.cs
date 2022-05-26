using Assignment.API.Assignment.DAL.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.API.Assignment.CommonDefinitions.Requests
{
    public class BaseRequest
    {
        public homework_dbContext _context;

        public  int DefaultPageSize = 70;

        public bool IsDesc { get; set; }

        public string OrderByColumn { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
        public string BaseUrl { get; set; }
    }
}