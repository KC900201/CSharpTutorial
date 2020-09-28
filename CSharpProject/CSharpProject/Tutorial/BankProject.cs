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
09212020	  Abstract class, Multiple interfaces, Method overriding, Parent class extension
09252020	  ToString, object
09262020	  Set and Get method
09282020	  Hashtable and search performance; Advanced Programming
**/

using System;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;

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
	string RudeLetterString();
	string GetName();
	// interface and properties
	int Age
    {
		set;
		get;
    }
}

// Interface for Bank (09282020)
// A container class that provide methods which find a particular
// method based on the name of the holder. We can express the behaviour that
// that we need from our bank in terms of interface
interface IBank
{
	IAccount FindAccount(string name);
	bool StoreAccount(IAccount account);
}

// Multiple interfaces - 09212020
public interface IPrintToPaper
{
	void DoPrint();
}

public class ArrayBank : IBank
{
	private IAccount[] accounts;
	public ArrayBank(int bankSize)
	{
		accounts = new IAccount[bankSize];
	}

	public IAccount FindAccount(string name)
	{
		int position = 0;

		for (position = 0; position < accounts.Length; position++)
		{
			if (accounts[position] == null)
			{
				continue;
			}

			if (accounts[position].GetName() == name)
			{
				return accounts[position];
			}
		}
		return null;
	}

	public bool StoreAccount(IAccount account)
	{
		int position = 0;

		for (position = 0; position < accounts.Length; position++)
		{
			if (accounts[position] == null)
			{
				accounts[position] = account;
				return true;
			}
		}
		return false;
	}
}

public class HashBank: IBank
{
	Hashtable bankHashtable = new Hashtable();

	public IAccount FindAccount(string name)
    {
		return bankHashtable[name] as IAccount;
    }

	public bool StoreAccount(IAccount account)
    {
		bankHashtable.Add(account.GetName(), account);
		return true;
    }
}


public class BabyAccount: Account, IPrintToPaper
{
	private decimal balance = 0;
	private int age;

	public void DoPrint()
    {
		Console.WriteLine("Print");
    }

	// Method overriding - 09212020
	public override bool WithdrawFunds(decimal amount)
    {
		if (amount > 10 || balance < amount)
        {
			return false;
        }

		// base - reference to the thing which has been overridden
		return base.WithdrawFunds(amount); // refer Account.WithdrawFunds()
		/*
		balance -= amount;

		return true;
		*/
    }

	public override string RudeLetterString()
    {
		return "Tell daddy you are overdrawn";
    }
	// End method overriding 

	public void PayInFunds(decimal amount)
    {
		balance += amount;
    }

	// 09262020
	public void setBalance(decimal balance)
    {
		this.balance = balance;
    }

	public decimal getBalance ()
    {
		return this.balance;
    }
}

// Abstract class - 09212020
public abstract class AAccount : IAccount
{
	private decimal balance = 0;
	private string name;
	public abstract string RudeLetterString();
	int IAccount.Age 
	{
		set;
		get;
	}

    public virtual bool WithdrawFunds(decimal amount)
    {
		if (balance < amount)
        {
			return false;
        }

		balance = balance - amount;
		return true; 
    }

	public decimal GetBalance()
    {
		return this.balance;
    }

	public string GetName()
    {
		return this.name;
    }

	public void PayInFunds(decimal amount)
    {
		this.balance += amount;
    }
}

public class Account : AAccount
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

	// method overriding - 09212020
	public override string RudeLetterString()
    {
		return "You are overdrawn";
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

	public Account(string inName, decimal inBalance)
    {
		this.name = inName;
		this.balance = inBalance;
    }

	// 09252020
	public override string ToString()
	{
		return "Name: " + name + ", balance: " + balance;
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

	/*Need to change declaration of method to make overriding work - 09212020*/
	public virtual bool WithdrawFunds (decimal amount)
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

	public decimal getBalance ()
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

public class CustomerAccount: IAccount
{
	private string name;
	private decimal balance = 0;

    int IAccount.Age { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public CustomerAccount (string newName, decimal initialBalance)
    {
		this.balance = initialBalance;
		this.name = newName;

		Console.WriteLine("New account created");
		Console.WriteLine("Name: " + this.name);
		Console.WriteLine("Balance: " + this.balance);
	}

	public virtual bool WithdrawFunds (decimal amount)
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
		return this.balance;
    }

	public string GetName()
    {
		return this.name;
    }

	// Saving An Account method - 09282020
	public bool Save (string fileName)
    {
		// given the name of the file that the account is to be stored in. 
		// It writes out the name of the customer and the balance of the 
		// account.
		try
		{
			System.IO.TextWriter textOut = new System.IO.StreamWriter(fileName);
			textOut.WriteLine(name);
			textOut.WriteLine(balance);
			textOut.Close();
        } 
		catch
        {
			return false;
        }

		return true;
    }

	// Loading an Account method - 09282020
	public static CustomerAccount Load(string fileName)
    {
		CustomerAccount result = null;
		System.IO.TextReader textIn = null;

		try
        {
			textIn = new System.IO.StreamReader(fileName);
			string nameText = textIn.ReadLine();
			string balanceText = textIn.ReadLine();
			decimal balance = decimal.Parse(balanceText);
			result = new CustomerAccount(nameText, balance);
        }
		catch
        {
			// returns null to indicate that load failed if 
			// anything bad happens
			Console.WriteLine("Load failed");
			return null;
        }
		finally
        {
			// close file to prevent error
			if (textIn != null)
				textIn.Close();
        }

		return result;
    }

    string IAccount.RudeLetterString()
    {
        throw new NotImplementedException();
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

		//Account test = new Account();
		//		test.PayInFunds(23343);
		//		Console.WriteLine("Balance: " + test.getBalance());
		// Test new account
		/*
		Account johnAcc = new Account("John Smith", "123 street, US", 32222);
		Account willAcc = new Account("Will Smith");
		Account jamAcc = new Account("Jam Hsiao", "Taipei, Taiwan");
		BabyAccount jamBaby = new BabyAccount();
		Account testToStr = new Account("Nana", 2244);

		Console.WriteLine(testToStr);
		Console.WriteLine("Account: " + jamAcc.RudeLetterString());
		jamBaby.setBalance(355445);
		Console.WriteLine("Baby account balance: " + jamBaby.getBalance());
		Console.WriteLine("Baby account: " + jamBaby.RudeLetterString());
		*/
		// save account
		CustomerAccount robAcc = new CustomerAccount("Rob Well", 9999);

		Console.WriteLine("Save an account");
		robAcc.Save("test.txt");

		// load an account
		Console.WriteLine("Load an account");
		CustomerAccount testLoad = CustomerAccount.Load("test.txt");
		if (testLoad == null)
        {
			Console.WriteLine("File does not exist");
        }

		// Store an account in a bank (09282020)
		IBank testBank = new ArrayBank(50);

	}
}
