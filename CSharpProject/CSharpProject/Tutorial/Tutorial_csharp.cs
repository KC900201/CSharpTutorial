/**
Created on 06 Sep 2020

@author: setsu
@filename: Tutorial_csharp.cs
@coding: C#
========================
Date          Comment
========================
09062020      First revision
**/

// Example of C# program

using System; // instruction to compiler to tell we want to use things from namespace

class GlazerCalc // class is a container that holds data and program code. Every class needs an identifier (name)
{ 
    // Example code
    void CalcGlaze() 
    {
        double width, height, woodLength, glassArea;
        string widthString, heightString;

        // Constants
        const double MAX_WIDTH = 20.0;
        const double MIN_WIDTH = 0.5;
        const double MAX_HEIGHT = 250.0;
        const double MIN_HEIGHT = 1.0;

        Console.Write("Input window width: ");
        widthString = Console.ReadLine();
        width = double.Parse(widthString);

        Console.Write("Input window height: ");
        heightString = Console.ReadLine();
        height = double.Parse(heightString);

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

        woodLength = 2 * (width + height) * 3.25;
        glassArea = 2 * (width + height);

        Console.WriteLine("The length of the wood is " + woodLength.ToString() + " feet");
        Console.WriteLine("The area of the glass is " + glassArea.ToString() + " square metres");
    }

    static void Main() // main function of class, mandatory
    {
        GlazerCalc g1 = new GlazerCalc();

        g1.CalcGlaze();
    }
    
}
