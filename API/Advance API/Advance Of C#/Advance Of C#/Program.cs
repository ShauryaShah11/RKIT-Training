using Advance_Of_C_.Base_Class_Library;
using Advance_Of_C_.Database_Operation;
using Advance_Of_C_.Dynamic;
using Advance_Of_C_.Extension_Methods;
using Advance_Of_C_.File_Operations;
using Advance_Of_C_.LINQOperations;
using Advance_Of_C_.Security_And_CryptoGraphy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Advance_Of_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Abstract Class
            //Abstarct Clas
            //Dog dog = new Dog();
            //dog.MakeSound();
            #endregion

            #region Final Class
            //Final Class
            //FinalClass finalClass = new FinalClass();
            //finalClass.FinalMessage();
            #endregion

            #region Static Class
            //Static class
            //MathUtilities mu = new MathUtilities();
            //int sum = MathUtilities.Add(10, 15);
            //Console.WriteLine("Sum is " + sum);
            #endregion

            #region Normal Class
            //Normal Class
            //Car car = new Car();
            //car.Drive();
            //car.Make = "Tata";
            //car.Model = "Punch";
            //Console.WriteLine($"Car Make: {car.Make} , Car Model : {car.Model}");
            #endregion

            #region Partial Class
            //Partial Class
            //Employee emp = new Employee();
            //emp.Name = "Shaurya";
            //emp.Age = 20;

            //Console.WriteLine($"Employee Name: {emp.Name}, Employee Age: {emp.Age}");
            #endregion

            #region Nested Class
            //Nested Class
            //OuterClass.InnerClass inner = new OuterClass.InnerClass();
            //inner.DisplayMessage();
            #endregion

            #region Singleton Instance
            //SingleTon Instance
            //Singleton singleton = Singleton.Insatnce;
            //singleton.ShowMessage();
            #endregion

            #region Generic Class
            //Generic Class
            //GenericClass<string> genericClass = new GenericClass<string>();
            //genericClass.AddItemInList("Shaurya");
            //genericClass.AddItemInList("John");
            //Console.WriteLine("Item is :" + genericClass.GetItemFromList(0));

            //genericClass.AddItemInDict("India", "Delhi");
            //genericClass.AddItemInDict("USA", "Washington DC");
            //genericClass.AddItemInDict("UK", "London");
            //genericClass.PrintDict();

            //GenericClass<int> genericClass1 = new GenericClass<int>();
            //genericClass1.AddItemInList(1);
            //genericClass1.AddItemInList(2);
            //Console.WriteLine("Item is :" + genericClass.GetItemFromList(0));

            //GenericClassWithWhere<List<string>> classConstaint = new GenericClassWithWhere<List<string>>();
            //classConstaint.DisplayType();

            //GenericClassWithStruct<int> structIntConstarint = new GenericClassWithStruct<int>();
            //Console.WriteLine("Addition is:"+ structIntConstarint.Add(10,15));

            //GenericClassWithStruct<DateTime> structDateTimeConstraint = new GenericClassWithStruct<DateTime>();
            // Console.WriteLine("Addition is :"+ structConstraint.Add(DateTime.Now, DateTime.UtcNow)); //Exception will be thrown

            // Example with 'new()' constraint
            //GenericClassWithConstructor<DisplayableClass> constructorConstraint = new GenericClassWithConstructor<DisplayableClass>();
            //DisplayableClass instance = constructorConstraint.CreateInstance();
            //Console.WriteLine($"Created Instance: {instance.Name}");

            // Example with base class constraint
            //GenericClassWithBase<SomeBaseClass> baseConstraint = new GenericClassWithBase<SomeBaseClass>();
            //baseConstraint.DisplayBaseProperty(new SomeBaseClass());

            // Example with interface constraint
            //GenericClassWithInterface<DisplayableClass> interfaceConstraint = new GenericClassWithInterface<DisplayableClass>();
            //interfaceConstraint.Display(new DisplayableClass());
            #endregion

            #region File Operations
            //File Opearation
            //FileStreamExample fs = new FileStreamExample("test.txt");
            //fs.AppendToFile("Hi I am Shaurya Shah");
            //fs.DisplayFileInfo();
            //fs.CheckFilePermissions();

            //StreamWriterExample sw = new StreamWriterExample("test.txt");
            //sw.ReadFile();
            #endregion

            #region Binary Serialization
            //Binary Serialization
            //BinarySerialization bs = new BinarySerialization();
            //bs.Serialize();
            //bs.DeSerialize();
            #endregion

            #region XML Serialization
            //XML Serialization
            //XMLSerialization xs = new XMLSerialization();
            //xs.Serialize();
            //xs.Deserialize();
            #endregion

            #region JSON Serialization
            //JSON Serialization
            //JSONSerialization js = new JSONSerialization();
            //js.Serialize();
            //js.Deserialize();
            #endregion

            #region Extension Methods
            //Extension Methods
            //LambdaExpression le = new LambdaExpression();
            //le.Practice();

            //Extension Methods
            //string str = "Hello World!";
            //int wordCount = str.WordCount();
            //Console.WriteLine("WordCount is :"+wordCount);
            //int x = 10;
            //x.Increment();
            //Console.WriteLine($"After Increment X value is :{x}");
            //x.RefIncrement();
            //Console.WriteLine($"After Ref Incremnet X value is {x}");
            #endregion

            #region Object Reset using Extension Methods
            //object myObject = new object();

            //Console.WriteLine($"Before Reset: {myObject != null}"); // Output: True

            //// Call the extension method
            ////myObject.ResetObject(); // Will not work
            //ObjectExtensions.ResetObject(ref myObject);

            //Console.WriteLine($"After Reset: {myObject == null}"); // Output: True
            #endregion

            #region Filter Any Types of Data in List
            //List<Employee> employees = Data.GetAllEmployees();

            //var filteredEmployee = employees.Filter(emp => emp.IsManager == "Yes");
            //foreach (var employee in filteredEmployee)
            //{
            //    Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Manager: {employee.IsManager}, Salary: {employee.Salary}");
            //}
            #endregion

            #region LINQ Operation

            //LINQWithList obj1 = new LINQWithList();
            //obj1.Test();

            //LINQWithDataTable obj2 = new LINQWithDataTable();
            //obj2.Test();

            // Max salary
            //var maxSalary = employees.Max(emp => emp.Salary);
            //Console.WriteLine($"Max Salary is: {maxSalary}");

            //// Min salary
            //var minSalary = employees.Min(emp => emp.Salary);
            //Console.WriteLine($"Min Salary is: {minSalary}");

            //var avgSalary = employees.Average(emp => emp.Salary);
            //Console.WriteLine($"Avg Salary is: {avgSalary}");

            //var result = from emp in employees.GetHighSalariedEmployee(50000)
            //                select new
            //                {
            //                    Name = emp.Name,
            //                    avgSalary = emp.Salary
            //               };


            #endregion

            #region  Security and Cryptography

            //string plainText, key, iv;
            //Console.WriteLine("enter plain text to encrypt: ");
            //plainText = Console.ReadLine();

            //SymmetricEncryptionExample se = new SymmetricEncryptionExample();
            //key = se.GetKey();
            //iv = se.GetIV();

            //string cipherText = se.Encrypt(plainText, key, iv);
            //Console.WriteLine($"Cipher Text is :{cipherText}");
            //plainText = se.Decrypt(cipherText, key, iv);

            //Console.WriteLine($"Plain text is : {plainText}");

            #region hash password with salt
            //PasswordExample passwordExample = new PasswordExample();

            //// Example: Create a hashed password and store the salt
            //Console.WriteLine("Enter Your password you want to hashed: ");
            //string password = Console.ReadLine();
            //byte[] salt = passwordExample.GenerateSalt();
            //string hashedPassword = passwordExample.HashedPassword(password, salt);

            //Console.WriteLine($"Salt (Base64): {Convert.ToBase64String(salt)}");
            //Console.WriteLine($"Hashed Password: {hashedPassword}");

            //// Example: Verify a user's input password
            //Console.WriteLine("Enter Password to check with hashed password :");
            //string inputPassword = Console.ReadLine(); // Correct password
            //bool isPasswordCorrect = passwordExample.VerifyPassword(inputPassword, salt, hashedPassword);
            //Console.WriteLine($"Password verification (correct password): {isPasswordCorrect}");

            //string wrongPassword = "WrongPassword456"; // Incorrect password
            //bool isWrongPasswordCorrect = passwordExample.VerifyPassword(wrongPassword, salt, hashedPassword);
            //Console.WriteLine($"Password verification (wrong password): {isWrongPasswordCorrect}");
            #endregion
            #endregion

            #region Dynamic Type
            //DynamicType dt = new DynamicType();
            //dt.DynamicDemo();
            #endregion

            #region Database With C#
            SelectOperation sc = new SelectOperation();

            InsertOperation insertOperation = new InsertOperation();
            //insertOperation.UnsafeInsertDemo();
            insertOperation.SafeInsertDemo();
            sc.SelectDemo();


            UpdateOperation updateOperation = new UpdateOperation();
            updateOperation.UpdateDemo();
            sc.SelectDemo();


            DeleteOperation deleteOperation = new DeleteOperation();
            deleteOperation.DeleteDemo();
            sc.SelectDemo();

            #endregion

            Console.ReadKey();
        }
    }
}
