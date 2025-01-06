using System;
using System.IO;
namespace BasicCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //HelloWorld hw = new HelloWorld();
            //hw.PrintMessage();

            //Console.ReadLine();

            //DataType dt = new DataType();
            //dt.DisplayMessage();
            //dt.ConvertToConversion();
            //dt.NumericFormating();

            //Operator op = new Operator();
            //op.ArithmeticOperator(20, 10);
            //op.UnaryArithmeticOperator(20, 10);
            //op.RelationalOperator(20, 10);
            //op.LogicalOperator(true, false);

            //Statement st = new Statement();
            //st.PracticeStatements();

            //ArrayPractice ap = new ArrayPractice();
            //ap.PracticeSingleDimensionArray();
            //ap.PracticeMultiDimensionArray();
            //ap.PracticeJaggedArray();

            //Car c = new Car("BMW", 100);
            //c.Accelerate(50);
            //c.Move();

            //Animal myAnimal = new Animal();  // Create a Animal object
            //Animal myPig = new Pig();  // Create a Pig object
            //Animal myDog = new Dog();  // Create a Dog object

            //myAnimal.AnimalSound();
            //myPig.AnimalSound();
            //myDog.AnimalSound();

            //Circle c = new Circle(24.50D);
            //Console.WriteLine("Area of circle is :"+string.Format("{0:0.00}",c.GetArea()));

            //IAnimal myCat = new Cat();
            //IAnimal myCow = new Cow();
            //myCat.MakeSound();
            //myCow.MakeSound();

            // Interface
            //MuiltiLevelInheritance.Dog dog = new MuiltiLevelInheritance.Dog();
            //dog.Bark();
            //dog.Eat();
            //dog.Breathe();

            //Horse horse = new Horse();
            //horse.MakeSound();
            //horse.Move();

            //BaseKeywordExample.Dog myDog = new BaseKeywordExample.Dog("Buddy");
            //myDog.Describe();
            //myDog.MakeSound();

            //BankAccount ba = new BankAccount();
            //ba.Deposit(100);
            //ba.Withdraw(50);

            //ScopeModifier sm = new ScopeModifier();
            //sm.Display();
            //sm.ShowAge();
            //DerivedClass dc = new DerivedClass();
            //dc.Display();

            //MyNamespace.NameSpaceClass nsc = new MyNamespace.NameSpaceClass();
            //nsc.Display();

            //MathOperations mathOperations = new MathOperations();
            //Console.WriteLine("Addition is :" + mathOperations.add(2, 3));
            //Console.WriteLine("Subtraction is :" + mathOperations.sub(20, 10));
            //Console.WriteLine("Multiplication is :" + mathOperations.mul(10, 2));
            //Console.WriteLine("Divison is :" + mathOperations.div(10, 2));

            //CollectionFramework cf = new CollectionFramework();
            //cf.ListPractice();
            //cf.DictionaryPractice();
            //cf.StackPractice();
            //cf.QueuePractice();
            //cf.HashSetPractice();
            //cf.DictionaryWithListPractice();
            //cf.ListWithDictionaryPractice();

            //Enumerations em = new Enumerations();
            //em.EnumPractice();

            DataTableClass dataTableClass = new DataTableClass();
            dataTableClass.DataTablePractice();

            //ExceptionHandlingExample exceptionHandlingExample = new ExceptionHandlingExample();
            //exceptionHandlingExample.HandleExceptionWithTryCatchFinally();
            //try
            //{
            //    exceptionHandlingExample.HandleExceptionWithThrow(15);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //exceptionHandlingExample.CustomException();

            //StringClass sc = new StringClass();
            //sc.StringPractice();

            //DataTimeClass dataTimeClass = new DataTimeClass();
            //dataTimeClass.DatePractice();

            //string fileName = "test.txt";
            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            //FileUtility fileUtility = new FileUtility();
            //fileUtility.CreateFile(filePath);
            //fileUtility.ReadFile(filePath);

            //fileUtility.AppendToFile(filePath);
            //fileUtility.ReadFile(filePath);
            //fileUtility.CopyFile(filePath, destinationPath: Path.Combine(Directory.GetCurrentDirectory(), "test2.txt"), true);
            //fileUtility.DeleteFile(filePath);
            //fileUtility.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "TestFolder"));
            //fileUtility.CreateSubdirectory(Path.Combine(Directory.GetCurrentDirectory(), "TestFolder"), "SubFolder");

            //Miscellaneous m = new Miscellaneous();
            //m.Boxing();
            //m.UnBoxing();
            //m.StringToArray();

            //string temp = "1, 22";
            //Console.WriteLine(temp.Contains("2"));

        }
    }
}
