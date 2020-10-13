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
10022020	  Multiple accounts saving
10092020	  Handling different kinds of account,
			  Business objects and editing
10132020	  Editor class
**/

using System;
using System.Collections;
using System.Diagnostics.Contracts;
using System.IO;
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
	void SetName(string name);

	bool ValidateName(string name);
	bool Save(string filename);
	void Save(System.IO.TextWriter textOut);

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


// 10132020
public class AccountEditTextUI
{
	private IAccount account;

	public AccountEditTextUI(CustomerAccount inAccount)
    {
		this.account = inAccount;
    }

	public void EditName()
    {
		string newName;

		Console.WriteLine("Edit user name");

		while(true)
        {
			Console.Write("Enter new name: ");
			newName = Console.ReadLine();

			if (this.account.ValidateName(newName))
            {
				this.account.SetName(newName);
				break;
            }
        }

//		this.account.SetName(newName);
	}

	public void PayInFunds ()
    {
		decimal amount;

		Console.WriteLine("Pay In Funds");

		while (true)
		{
			Console.Write("Enter amount: ");
			amount = decimal.Parse(Console.ReadLine());

			if (amount > 0)
			{
				this.account.PayInFunds(amount);
				break;
			}
		}
	}

	public void WithDrawFunds()
	{
		decimal amount;

		Console.WriteLine("Withdraw Funds");

		while (true)
		{
			Console.Write("Enter amount: ");
			amount = decimal.Parse(Console.ReadLine());

			if (amount > 0)
			{
				this.account.WithdrawFunds(amount);
				break;
			}
		}
	}

	public void DoEdit (CustomerAccount account)
    {
		string command;
		do
		{
			Console.WriteLine("Editing account for {0}", account.GetName());
			Console.WriteLine("Enter name to edit name");
			Console.WriteLine("Enter pay to pay in funds");
			Console.WriteLine("Enter draw to draw out funds");
			Console.WriteLine("Enter exit to exit program");
			Console.Write("Enter command: ");
			command = Console.ReadLine().Trim().ToLower();

			switch(command)
            {
				case "name":
					EditName();
					break;
				case "pay":
					PayInFunds();
					break;
				case "draw":
					WithDrawFunds();
					break;
				default:
					Console.WriteLine("Invalid command");
					break;
            }

		} while (command != "exit");
    }

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

	public void Save(System.IO.TextWriter textOut)
	{
		textOut.WriteLine(bankHashtable.Count);
		foreach (CustomerAccount acc in bankHashtable.Values)
		{
			acc.Save(textOut);
		}
	}

	public static HashBank Load(System.IO.TextReader textIn)
    {
		HashBank result = new HashBank();
		string countString = textIn.ReadLine();
		int count = int.Parse(countString);

		for(int i = 0; i < count; i++)
        {
			//			CustomerAccount account = CustomerAccount.Load(textIn);
			// Override using factory class to create bank accounts
			string className = textIn.ReadLine();
			IAccount account = AccountFactory.MakeAccount(className, textIn);
			result.bankHashtable.Add(account.GetName(), account.GetBalance());
        }

		return result;
    }
}


public class BabyAccount: Account, IPrintToPaper
{
	private decimal balance = 0;
	private string parentName;
	private int age;

	public string GetParentName ()
    {
		return this.parentName;
    }

	public void DoPrint()
    {
		Console.WriteLine("Print");
    }

	// Saving a child class - 10092020
	public void Save(System.IO.TextWriter textOut)
    {
		base.Save(textOut);
		textOut.WriteLine(parentName);
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
	public void SetBalance(decimal balance)
    {
		this.balance = balance;
    }

	public decimal GetBalance ()
    {
		return this.balance;
    }

	// Full Baby Account implementation (10/09/2020)
	public BabyAccount(string newName, decimal initialBalance,
		string inParentName) : base(newName, initialBalance)
    {
		this.parentName = inParentName;
    }

	// Create a Baby Account from constructors of parent class - 10092020
	public BabyAccount (System.IO.TextReader textIn) : base(textIn) 
	{
		this.parentName = textIn.ReadLine();
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

	public void SetName(string name)
    {
		this.name = name;
    }

	public void PayInFunds(decimal amount)
    {
		this.balance += amount;
    }

    bool IAccount.Save(string filename)
    {
        throw new NotImplementedException();
    }

    void IAccount.Save(TextWriter textOut)
    {
        throw new NotImplementedException();
    }

    public abstract bool ValidateName(string name);
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

	// Multiple accounts saving (method overloading) - 10022020
	public void Save(System.IO.TextWriter textOut)
	{
		textOut.WriteLine(this.name);
		textOut.WriteLine(this.balance);
		textOut.Close();
	}

	// Saving An Account method - 09282020
	public bool Save(string fileName)
	{
		// given the name of the file that the account is to be stored in. 
		// It writes out the name of the customer and the balance of the 
		// account.

		// Calling another save method - 10022020
		System.IO.TextWriter textOut = null;

		try
		{
			/*
			System.IO.TextWriter textOut = new System.IO.StreamWriter(fileName);
			textOut.WriteLine(name);
			textOut.WriteLine(balance);
			*/
			textOut = new System.IO.StreamWriter(fileName);
			Save(textOut);

			//			textOut.Close();
		}
		catch
		{
			return false;
		}
		finally
		{
			if (textOut != null)
				textOut.Close();
		}

		return true;
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
	 // 10092020
	public Account(System.IO.TextReader textIn)
    {
		this.name = textIn.ReadLine();
		this.balance = decimal.Parse(textIn.ReadLine());
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

    public override bool ValidateName(string name)
    {
        throw new NotImplementedException();
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

	// Validation method - 10092020
	public bool ValidateName (string name)
	{
		if (name == null)
        {
			Console.WriteLine("Name parameter null");
			return false;
        }
		else if (name.Trim().Length == 0)
        {
			Console.WriteLine("No text in name");
			return false;
        } 
	
		return true;
    }

	public string GetName()
    {
		return this.name;
    }

	// 10092020
	public void SetName(string name)
    {
		if (ValidateName(name)) // validation before setting
        {
			this.name = name;
		}
	}

	// Saving An Account method - 09282020
	public bool Save (string fileName)
    {
		// given the name of the file that the account is to be stored in. 
		// It writes out the name of the customer and the balance of the 
		// account.

		// Calling another save method - 10022020
		System.IO.TextWriter textOut = null;

		try
		{
			/*
			System.IO.TextWriter textOut = new System.IO.StreamWriter(fileName);
			textOut.WriteLine(name);
			textOut.WriteLine(balance);
			*/
			textOut = new System.IO.StreamWriter(fileName);
			Save(textOut);

//			textOut.Close();
        } 
		catch
        {
			return false;
        }
		finally
        {
			if (textOut != null)
				textOut.Close();
        }

		return true;
    }

	// Multiple accounts saving (method overloading) - 10022020
	public void Save (System.IO.TextWriter textOut)
    {
		textOut.WriteLine(this.name);
		textOut.WriteLine(this.balance);
		textOut.Close();
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

	// Loading an Account method - 10022020
	public static CustomerAccount Load(System.IO.TextReader textIn)
	{
		CustomerAccount result = null;

		try
		{
			string name = textIn.ReadLine();
			decimal balance = decimal.Parse(textIn.ReadLine());
			result = new CustomerAccount(name, balance);
		}
		catch
		{
			return null;
		}

		return result;
	}

    string IAccount.RudeLetterString()
    {
        throw new NotImplementedException();
    }
}

// Factory class - 10092020
class AccountFactory
{
	public static IAccount MakeAccount(string name, System.IO.TextReader textIn)
    {
		switch (name)
        {
			case "BabyAccount":
				return new BabyAccount(textIn);
			case "Account":
				return new Account(textIn);
			default:
				return null;
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
		
		// save account
		CustomerAccount robAcc = new CustomerAccount("Rob Well", 9999);

		Console.WriteLine("Save an account");
		robAcc.Save("test.txt");

		// Baby Account
		BabyAccount babyEric = new BabyAccount("Eric", 20, "John");

		// load an account
		Console.WriteLine("Load an account");
		CustomerAccount testLoad = CustomerAccount.Load("test.txt");
		if (testLoad == null)
        {
			Console.WriteLine("File does not exist");
        }

		// Store an account in a bank (09282020)
		Console.WriteLine("Reading accounts from text");
		HashBank testHash = HashBank.Load(new System.IO.StreamReader("testBank.txt"));
		// Print out all accounts
		*/

		// Test validation
		bool reply;
		reply =  new CustomerAccount("Jobs", 9999).ValidateName("");
		Console.WriteLine(reply.ToString());

		CustomerAccount testVal = new CustomerAccount("Rob", 50);
		//		testVal.SetName("");
		// Implement text Editor
		AccountEditTextUI editor = new AccountEditTextUI(testVal);
		editor.EditName();

		Console.WriteLine(testVal.GetName());

	}
}
