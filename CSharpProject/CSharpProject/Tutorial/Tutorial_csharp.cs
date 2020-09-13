/**
Created on 06 Sep 2020

@author: setsu
@filename: Tutorial_csharp.cs
@coding: C#
========================
Date          Comment
========================
09062020      First revision
09072020      Pass by reference, Nested printings, method libraries
09112020      Variables concept, Exception
09132020      Switch case statement, File I/O
**/

// Example of C# program

using System; // instruction to compiler to tell we want to use things from namespace
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;

class GlazerCalc // class is a container that holds data and program code. Every class needs an identifier (name)
{
    // Global variable - 09112020
    static int testVar = 0;
    static int size = 12;
    // Array C#
    static string[] month = new string[size];

    static void OtherMethod()
    {
        testVar = 999;
    }

    static void InsertMonth()
    {
        foreach (int i in Enumerable.Range(0, 12))
        {
            month[i] = Console.ReadLine();
            Console.WriteLine(i + " month: " + month[i]);
        }
    }

    // Example code
    static void CalcGlaze() 
    {
        double width, height, woodLength, glassArea;
        string widthString, heightString;

        // Constants
        const double MAX_WIDTH = 20.0;
        const double MIN_WIDTH = 0.5;
        const double MAX_HEIGHT = 250.0;
        const double MIN_HEIGHT = 1.0;
        /*
        Console.Write("Input window width: ");
        widthString = Console.ReadLine();
        width = double.Parse(widthString);

        Console.Write("Input window height: ");
        heightString = Console.ReadLine();
        height = double.Parse(heightString);

        // Conditional if-else
        
        if (width < MIN_WIDTH)
        {
            Console.WriteLine("input value less than min width value");
            width = MIN_WIDTH;
        } 
        else if (width > MAX_WIDTH) 
        { 
            Console.WriteLine("input value more than max width value");        
            width = MAX_WIDTH;
        }

        if (height < MIN_HEIGHT)
        {
            Console.WriteLine("input value less than min height value");
            height = MIN_HEIGHT;
        }
        else if (height > MAX_HEIGHT)
        {
            Console.WriteLine("input value more than max height value");
            height = MAX_HEIGHT;
        }
        */
        // Looping method
        do
        {
            Console.Write("Give the width of the window between " + MIN_WIDTH + " and " + MAX_WIDTH + ": ");
            width = double.Parse(Console.ReadLine());
        } while (width < MIN_WIDTH || width > MAX_WIDTH);

        do
        {
            Console.Write("Give the height of the window between " + MIN_HEIGHT + " and " + MAX_HEIGHT + ": ");
            height = double.Parse(Console.ReadLine());
        } while (height < MIN_HEIGHT || height > MAX_HEIGHT);

        woodLength = 2 * (width + height) * 3.25;
        glassArea = 2 * (width + height);

        Console.WriteLine("The length of the wood is " + woodLength.ToString() + " feet");
        Console.WriteLine("The area of the glass is " + glassArea.ToString() + " square metres");
    }

    // Learning neater printings - 09072020
    public static void printVal()
    {
        int a = 1232;
        float b = 123.44422235f;
        long c = 1232343443;

        Console.WriteLine("a: {0} b: {1} c: {2}", a, b, c);
        // Control real number precision
        Console.WriteLine("b: {0:0000.000}", b);
        // Print in columns
        Console.WriteLine("c: {0:10:0}, a: {1, 15:0.00}", c, a);
    }

    // Pass by Reference - 09072020
    public static void passByRef(ref int i)
    {
        i += 1;
        Console.WriteLine("i = {0}", i);
    }

    // Method Libraries - 09072020
    static string readString(String prompt)
    {
        string result; 
        do { 
            Console.Write(prompt); result = Console.ReadLine(); 
        } while (result == ""); 
        return result;
    }

    static int readInt(string prompt, int low, int high)
    {
        int result; 
        do { 
            string intString = readString(prompt); 
            result = int.Parse(intString); 
        } while ((result < low) || (result > high)); 
        return result;
    }

    public void testRead()
    {
        string name;
        int age;

        name = readString("Enter your name : ");
        age = readInt("Enter your age : ", 0, 100);
    }

    protected static void testException()
    {
        try
        {
            throw new Exception("Lalalaala");
        } catch (Exception e)
        {
            Console.WriteLine("Exception caught: " + e.Message);
        }
    }

    // 09132020
    protected static void testSwitch()
    {
        Console.Write("Write your choice: ");
        string s = Console.ReadLine();
        int i = 100;
        switch(s)
        {
            case "a":
                testException();
                break;
            case "b":
                passByRef(ref i);
                break;
            case "c":
                CalcGlaze();
                break;
            case "d":
                printVal();
                break;
            default:
                Console.WriteLine("Invalid command!");
                break;
        }
    }

    static void fileIO()
    {
        // New class
        Console.WriteLine("Opening a new file");
        StreamWriter writer;
        writer = new StreamWriter("test.txt");

        writer.WriteLine("Hello World!");

        Console.WriteLine("Close file");
        writer.Close();
    }
    // End 09132020

    static void Main() // main function of class, mandatory
    {
        GlazerCalc g1 = new GlazerCalc();
        int i = 99;
    //    g1.CalcGlaze();
    //    g1.printVal();
    //    g1.passByRef(ref i);
    //    g1.testRead();

        try // Simple try catch exception nest (09112020)
        {
            /*
            Console.WriteLine("testVar: {0}", testVar);
            OtherMethod();
            Console.WriteLine("testVar: {0}", testVar);
            Console.WriteLine("Append array");
            */
//            testSwitch();
            fileIO();
            //throw new Exception("Yes, I've done it.");
            //            InsertMonth();
            //testException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            // Do nothing
        }
    }
    
}
