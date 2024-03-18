using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Data
{
	[Table("tb_Category_Course")]
	public class Category_Course
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Category_coures_id { get; set; }
		public string Category_name { get; set; }
		public string Category_description { get; set; }
		public virtual ICollection<Course> Courses { get; set; } = new List<Course>();



	}
}
