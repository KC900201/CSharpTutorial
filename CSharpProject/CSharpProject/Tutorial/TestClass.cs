/**
Created on 18 Sep 2020

@author: setsu
@filename: BankProject.cs
@description: Sample cs structure to learn about data structure of C#
@coding: C#
========================
Date          Comment
========================
09252020      First revision
09252020     Object testing for equals
 **/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;

namespace CSharpProject.Tutorial
{
    public class Point
    {
        public int x;
        public int y;

        /*
        public override bool Equals(object obj)
        {
            Point p = (Point) obj;
            
            if ((p.x == x) && (p.y == y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        */
        public void stringCompare()
        {
            string s1 = "Emelio";
            string s2 = "Emelio";
            char c1 = s1[5];


            if (s1.Length == s2.Length)
            {
                Console.WriteLine("Same");
            }
            
            if (s1.Equals(s2))
            {
                Console.WriteLine("Still same");
            }

            Console.WriteLine(c1);

        }
    }

    public class TestClass
    {
        public static void TestMain()
//        public static void Main()
        {
            Point spacePosition = new Point();
            spacePosition.x = 1;
            spacePosition.y = 2;

            Point shipPosition = new Point();
            shipPosition.x = 1;
            shipPosition.y = 2;

            if (shipPosition.Equals(spacePosition))
            {
                Console.WriteLine("Bang!");
            }

            Point testString = new Point();
            testString.stringCompare();

        }
    }
}
