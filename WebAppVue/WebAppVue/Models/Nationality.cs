using System;
using System.Collections.Generic;

namespace WebAppVue.Models
{
    public partial class Nationality
    {
        public Nationality()
        {
            Student = new HashSet<Student>();
        }

        public int NationalityId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Student> Student { get; set; }
    }
}
