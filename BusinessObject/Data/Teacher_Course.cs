using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Data
{
    [Table("tb_Teacher_Course")]
    public class Teacher_Course
    {
        [Key, ForeignKey("User")]
        public int Teacher_Coures_id { get; set; }
        public string Teaching_major {  get; set; }
        public string Fullname { get; set; }
        [ForeignKey(nameof(Course))]
        public int Course_id { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Teacher_Class> Teacher_Class { get; set; } = new List<Teacher_Class>();
        public User User { get; set; }
    }
}
