/**
Created on 24 Oct 2020

@author: setsu
@filename: AccountManagement.cs
@description: Sample cs structure to learn about data structure of C#
@coding: C#
========================
Date          Comment
========================
10242020      First revision
**/

using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBanking
{
    
    public interface IAccount
    {
        void PayInFunds(decimal amount);
        bool WithdrawFunds(decimal amount);
        decimal GetBalance();
    }
    
    public class Account: IAccount
    {
        private decimal balance = 0;

        public void PayInFunds (decimal amount)
        {
            this.balance += amount;
        }

        public decimal GetBalance()
        {
            return balance;
        }
         
        public bool WithdrawFunds (decimal amount)
        {
            if (amount < 0)
            {
                return false;
            }

            if (this.balance >= amount)
            {
                this.balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
