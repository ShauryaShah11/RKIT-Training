using StackOrmConsole.DTO;
using StackOrmConsole.Models;
using StackOrmConsole.Services;
using System;

namespace StackOrmConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DEPT01Service deptService = new DEPT01Service();


            EMP01Service empService = new EMP01Service();

            //Department Operations
            //Response addDeptResponse = deptService.Post(new DTODEPT01 { P01101 = 1, P02102 = "HR", P03103 = "Human Resource", P04104 = "John Doe" });
            //Console.WriteLine(addDeptResponse.Message);

            //Response addEmpResponse = empService.Post(new DTOEMP01 { P01101 = 1, P02102 = "Shaurya Shah", P04104 = 1 });
            //Console.WriteLine(addEmpResponse.Message);

            //Response editDeptResponse = deptService.Post(new DTODEPT01 { P01101 = 1, P02102 = "Develoment", P03103 = "Software Development", P04104 = "Steve Jobs" });
            //Console.WriteLine(editDeptResponse.Message);

            //Response editEmpResponse = empService.Post(new DTOEMP01 { P01101 = 1, P02102 = "John Doe", P04104 = 1 });
            //Console.WriteLine(editEmpResponse.Message);
            

            Response deleteEmpResponse = empService.Post(new DTOEMP01 { P01101 = 1, P02102 = "John Doe", P04104 = 1 });
            Console.WriteLine(deleteEmpResponse.Message);

            Response deleteDeptResponse = deptService.Post(new DTODEPT01 { P01101 = 1, P02102 = "Develoment", P03103 = "Software Development", P04104 = "Steve Jobs" });
            Console.WriteLine(deleteDeptResponse.Message);

            // Keep the console open
            Console.ReadLine();
        }
    }
}
