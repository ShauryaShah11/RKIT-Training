using Advance_Of_C_.Data_Serialization;
using Advance_Of_C_.File_Operations;
using System;

namespace Advance_Of_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Abstarct Clas
            //Dog dog = new Dog();
            //dog.MakeSound();

            // Final Class
            //FinalClass finalClass = new FinalClass();
            //finalClass.FinalMessage();

            // Static class
            //MathUtilities mu = new MathUtilities();
            //int sum = MathUtilities.Add(10, 15);
            //Console.WriteLine("Sum is "+sum);

            // Normal Class
            //Car car = new Car();
            //car.Drive();
            //car.Make = "Tata";
            //car.Model = "Punch";
            //Console.WriteLine($"Car Make: {car.Make} , Car Model : {car.Model}");

            // Partial Class
            //Employee emp = new Employee();
            //emp.Name = "Shaurya";
            //emp.Age = 20;

            //Console.WriteLine($"Employee Name: {emp.Name}, Employee Age: {emp.Age}");

            // Nested Class
            //OuterClass.InnerClass inner = new OuterClass.InnerClass();
            //inner.DisplayMessage();

            // SingleTon Instance
            //Singleton singleton = Singleton.Insatnce;
            //singleton.ShowMessage();

            // Generic Class
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

            // File Opearation
            //FileStreamExample fs = new FileStreamExample("test.txt");
            //fs.AppendToFile("Hi I am Shaurya Shah");
            //fs.DisplayFileInfo();
            //fs.CheckFilePermissions();
            // Binary Serialization
            //BinarySerialization bs = new BinarySerialization();
            //bs.Serialize();
            //bs.DeSerialize();

            // XML Serialization
            //XMLSerialization xs = new XMLSerialization();
            //xs.Serialize();
            //xs.Deserialize();

            // JSON Serialization
            //JSONSerialization js = new JSONSerialization();
            //js.Serialize();
            //js.Deserialize();
            LambdaExpression le = new LambdaExpression();
            le.Practice();

            Console.ReadKey();
        }
    }
}
