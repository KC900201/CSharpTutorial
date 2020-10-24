/**
Created on 24 Oct 2020

@author: setsu
@filename: AccountTest.cs
@description: Sample cs structure to learn about data structure of C#
@coding: C#
========================
Date          Comment
========================
10242020      First revision
              using namespace to specify class usage
**/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
//using CustomerBanking; // 10242020

namespace CustomerBanking
{
    class AccountTest
    {
        public static void Main()
        {
            CustomerBanking.Account test = new CustomerBanking.Account(); // 10242020
            //Account test = new Account();
            test.PayInFunds(50);
            Console.WriteLine("Balance: " + test.GetBalance());
            Thread.Sleep(50);
        }
    }
}