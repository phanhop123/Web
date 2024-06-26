﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Data
{
	[Table("tb_User")]
	public class User
	{
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public int User_id { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string? Phone { get; set; }
		[StringLength(10)]
		public string? Fullname { get; set; }
		public string? Detail { get; set; }
		public string? Sex_name { get; set; }
		public string? CCCD { get; set; }

		[ForeignKey(nameof(Role))]
		public int Role_id { get; set; }
		public virtual Role Role { get; set; }
        public virtual ICollection<Teacher_Course> Teacher_Courses { get; set; } 
        public virtual ICollection<Student_Course> Student_Courses { get; set; } 





    }
}
