/**
Created on 18 Oct 2020

@author: setsu
@filename: Threading.cs
@description: Coding tutorial to learn on C# Threading
@coding: C#
========================
Date          Comment
========================
10182020      First revision
**/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CSharpProject.Tutorial
{
    class Threading
    {
        // Starting a thread
        static private void busyLoop()
        {
            long count;
            for (count = 0; count < 10000000L; count +=1)
            {
                Console.WriteLine("Counting: {0}", count);
            }
        }

        //static void Main()
        public static void testMain()
        {
            /* The Thread class provides a number of methods that your program can use to control what it does. To start the 
             * thread running you can use the Start method:
             */
            ThreadStart busyLoopMethod = new ThreadStart(busyLoop);
//            Thread t1 = new Thread(busyLoopMethod);

            /* Creates a thread called t1 and starts it running the busyLoop method. It also calls busyLoop directly from the Main method. 
             * This means that there are two processes actively*/
//            t1.Start();
//            busyLoop();

            for (int i = 0; i < 100; i++)
            {
                Thread t1 = new Thread(busyLoopMethod);
                t1.Start();

                if (t1.ThreadState == ThreadState.Running)
                {
                    Console.WriteLine("Thread Running");
                }

                Thread.Sleep(500);

            }
            busyLoop();
        }
    }
}
