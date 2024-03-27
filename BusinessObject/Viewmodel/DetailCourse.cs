using BusinessObject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Viewmodel
{
    public class DetailCourse
    {
        public int Student_Class_id { get; set; }
        public Teacher_Class Teacher_Class { get; set; }

    }
}
