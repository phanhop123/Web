using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Data
{
    [Table("tb_Class_Role")]
    public class Class_Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Class_Role_id { get; set; }
        public string Name { get; set; }
      
        public virtual ICollection<Teacher_Class> Teacher_Classes { get; set; } = new List<Teacher_Class>();
        public virtual ICollection<Student_Class> Student_Classes { get; set; } = new List<Student_Class>();




    }
}
