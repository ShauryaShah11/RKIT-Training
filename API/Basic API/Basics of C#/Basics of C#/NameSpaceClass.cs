using System;

/// <summary>
/// Namespace is used to organize code. With namespaces, one project can contain multiple classes with the same name but in different namespaces.
/// </summary>
namespace MyNamespace
{
    /// <summary>
    /// The NameSpaceClass class demonstrates a simple class within a namespace.
    /// </summary>
    class NameSpaceClass
    {
        /// <summary>
        /// Displays a greeting message to the console.
        /// </summary>
        public void Display()
        {
            Console.WriteLine("Hello from MyNamespace!");
        }
    }
}

//namespace OuterNamespace
//{
//    namespace InnerNamespace
//    {
//        /// <summary>
//        /// The NameSpaceClass class demonstrates a simple class within a nested namespace.
//        /// </summary>
//        class NameSpaceClass
//        {
//            /// <summary>
//            /// Displays a greeting message to the console.
//            /// </summary>
//            public void Greet()
//            {
//                Console.WriteLine("Hello from InnerNamespace!");
//            }
//        }
//    }
//}




