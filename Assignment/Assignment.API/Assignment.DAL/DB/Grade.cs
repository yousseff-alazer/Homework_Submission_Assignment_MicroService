using System;
using System.Collections.Generic;

#nullable disable

namespace Assignment.API.Assignment.DAL.DB
{
    public partial class Grade
    {
        public Grade()
        {
            Assignments = new HashSet<Assignment>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
