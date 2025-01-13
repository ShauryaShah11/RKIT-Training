using ServiceStack.DataAnnotations;

namespace StackOrmConsole.POCO
{
    public class DEPT01
    {
        [PrimaryKey]
        [AutoIncrement]
        public int P01F01 { get; set; }   // DepartmentId
        [StringLength(50)]
        public string P02F02 { get; set; } // Short Department Name
        [StringLength(50)]
        [Required]
        public string P03F03 { get; set; } // Full Department Name
        [StringLength(50)]
        [Required]
        public string P04F04 { get; set; } // Department Head
    }
}
