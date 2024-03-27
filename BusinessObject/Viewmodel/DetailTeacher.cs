using BusinessObject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Viewmodel
{
    public class DetailTeacher
    {
        public int Teacher_id { get; set; }
        public Teacher_Course Teacher_Course { get; set; }
    }
}
