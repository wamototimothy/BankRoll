
using Bank;
using System.Xml.Linq;

public class BankAccount
{
    private static int accountNumberSeed = 1234567890; //This class is only accessed by code inside
                                                       //this class.
                                                       //it is static, meaning it's shared by all of this class' objects.
    private readonly decimal _minimumBalance;
    private List<Transaction> allTransactions = new List<Transaction>();

    public string Number { get; set; }
    public string Owner { get; set; }
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
            }
            return balance;
        }

    }

    //opening a new Account.
    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0) { }
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        Owner = name;
        Number = accountNumberSeed.ToString();
        accountNumberSeed++;

        Owner = name;
        _minimumBalance = minimumBalance;
        if (initialBalance > 0)
        MakeDeposit(initialBalance, DateTime.Now, "Initial Balance");
    }
   
    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive");
        }

        var deposit = new Transaction(amount, date, note);
        allTransactions.Add(deposit);
    }
    public void MakeWithdraw(decimal amount, DateTime date, string note)
    {

        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive");
        }
        Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? withdrawal = new(-amount, date, note);
        allTransactions.Add(withdrawal);
        if(overdraftTransaction != null)
            allTransactions.Add(overdraftTransaction);
    }
    protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
        {
            throw new InvalidOperationException("Not sufficient funds for thos withdrawal");
        }
        else
        {
            return default;
        }
    }
    public string GetAccountHistory()
    {
        var compute = new System.Text.StringBuilder();
        decimal balance = 0;
        compute.AppendLine("Date\t\tAmount\tBalance\tNote");
        foreach (var item in allTransactions)
        {
            balance += item.Amount;
            compute.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
        }
        return compute.ToString();
    }
    public virtual void PerformMonthEndTransactions()
    {
    }

    
}