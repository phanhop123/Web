using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Data
{
	[Table("tb_Student_Course")]
	public class Student_Course
    {
		[Key,ForeignKey("User")]
		public int Student_id { get; set; } 
        public string Major { get; set; }
        public string Fullname { get; set; }
        public virtual ICollection<Student_Class> Student_Classes { get; set; } = new List<Student_Class>();
        public User User { get; set; }

    }
}
