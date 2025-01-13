using ServiceStack.DataAnnotations;
using System;

namespace StackOrmConsole.POCO
{
    public class EMP01
    {
        [AutoIncrement]
        [PrimaryKey]
        public int P01F01 { get; set; } // Employee Id
        [StringLength(50)]
        public string P02F02 { get; set; } // Employee Name
        public DateTime P03F03 { get; set; } // Hire Date
        [ForeignKey(typeof(DEPT01))]
        public int P04F04 { get; set; } // Foreign Key -> DEPT01 Table        

    }
}
