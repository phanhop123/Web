using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Data
{
	[Table("tb_Role")]
	public class Role
	{
		
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public int Role_id { get; set; }
		public string? Role_name { get; set; }
		public virtual ICollection<User> Users { get; set; } = new List<User>();

	}
}
