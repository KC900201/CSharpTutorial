using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpProject.Tutorial
{
    class Class1
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        static void Lola()
        {
            float a, b, c;
            Class1 aClass = new Class1();

            Console.WriteLine("Please enter an integer a: ");
            a = int.Parse(Console.ReadLine());

            Console.WriteLine("Please enter an integer b: ");
            b = int.Parse(Console.ReadLine());

            c = aClass.Add((int)a, (int)b); // typecasting example of a and b

            Console.WriteLine("Result of adding: " + c.ToString());

        }
    }
}
