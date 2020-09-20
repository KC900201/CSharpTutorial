/**
Created on 18 Sep 2020

@author: setsu
@filename: BankProject.cs
@description: Sample cs structure to learn about data structure of C#
@coding: C#
========================
Date          Comment
========================
09182020      First revision
09202020	  Default constructor, constructor management and overwriting
09202020	  Components and interface
 **/

using System;

// enum
enum AccountState
{
	New,
	Active,
	UnderAudit,
	Frozen,
	Closed
};

/*
struct Account
{
	public AccountState State;
	public string Name;
	public string Address;
	public int AccountNumber;
	private int Balance;
	public int Overdraft;
};
*/

// Interface implementation - 09202020
public interface IAccount
{
	void PayInFunds(decimal amount);
	bool WithdrawFunds(decimal amount);
	decimal GetBalance();
}

public class Account : IAccount
{
	private decimal balance = 0;
	private string name, address;

	// static variables
	private static decimal minIncome;
	private static int minAge;

	// default constructor - 09202020
	public Account()
    {
		Console.WriteLine("New account created");
    }


	// Constructor overloading - 09202020
	public Account(string inName, string inAddress, decimal inBalance)
    {
		balance = inBalance;
		name = inName;
		address = inAddress;

		Console.WriteLine("New account created");
		Console.WriteLine("Name: " + name);
		Console.WriteLine("Address: " + address);
		Console.WriteLine("Balance: " + balance);
	}

	// "this" method - 09202020
	public Account (string inName, string inAddress) : 
		this(inName, inAddress, 0)
    {
		Console.WriteLine("zero balance");
    }

	public Account(string inName) :
	this(inName, "Not supplied", 0)
	{
		Console.WriteLine("no address and zero balance");
	}

	public bool WithdrawFunds (decimal amount)
    {
		if (balance < amount)
        {
			return false;
        }

		balance -= amount;
		return true;
    }

	public void PayInFunds (decimal amount)
    {
		balance += amount;
    }

	public decimal GetBalance ()
    {
		return balance;
    }

	// static method
	public static bool AccountAllowed(decimal income, int age)
    {
		if ((income >= minIncome) && (age >= minAge))
        {
			return true;
        } else
        {
			return false;
        }
    }
}

public class BankProject
{
	// default constructor
	public BankProject()
    {
		Console.WriteLine("Testing default constructor.");
    }

	public static void Main()
    {
		/*
		Account MyAccount;
		MyAccount.State = AccountState.Active;
		MyAccount.Name = "Kwong Cheong";
		MyAccount.Address = "Malaysia";
		MyAccount.AccountNumber = 123234343;
		MyAccount.Balance = 0;
		MyAccount.Overdraft = -10000;
		Console.WriteLine("Name is " + MyAccount.Name);
		Console.WriteLine("Account state is " + MyAccount.State);
		Console.WriteLine("Balance is " + MyAccount.Balance);
		*/

		Account test = new Account();
		//		test.PayInFunds(23343);
		//		Console.WriteLine("Balance: " + test.GetBalance());
		// Test new account
		Account johnAcc = new Account("John Smith", "123 street, US", 32222);
		Account willAcc = new Account("Will Smith");
		Account jamAcc = new Account("Jam Hsiao", "Taipei, Taiwan");
	}
}
