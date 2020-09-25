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
using System.Text;

namespace CSharpProject.Tutorial
{
    public class Point
    {
        public int x;
        public int y;

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
    }

    public class TestClass
    {
        public static void Main()
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
        }
    }
}
