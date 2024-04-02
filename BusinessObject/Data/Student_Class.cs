using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Data
{
    [Table("tb_Student_Class")]
    public class Student_Class
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Student_Class_id { get; set; }

        [ForeignKey(nameof(Class_Role))]
        public int Class_Role_id { get; set; }
        public virtual Class_Role Class_Role { get; set; }

        [ForeignKey(nameof(Student_Course))]
        public int Student_Course_id { get; set; }
        public virtual Student_Course Student_Course { get; set; }

    }
}
