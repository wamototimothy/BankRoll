using Bank;

var account = new BankAccount("Wamoto", 1000);
Console.WriteLine($"Account Number: {account.Number} was created for {account.Owner} with {account.Balance} Initial Balance.");

account.MakeWithdraw(500, DateTime.Now, "Rent Payment");
account.MakeDeposit(100, DateTime.Now, "Been Paid YO!");
Console.WriteLine(account.Balance);

Console.WriteLine(account.GetAccountHistory());

var giftCard = new GiftCardAccount("gift card", 100, 50);
giftCard.MakeWithdraw(20, DateTime.Now, "get expensive coffee");
giftCard.MakeWithdraw(50, DateTime.Now, "buy groceries");
giftCard.PerformMonthEndTransactions();
// can make additional deposits:
giftCard.MakeDeposit(27.50m, DateTime.Now, "add some additional spending money");
Console.WriteLine(giftCard.GetAccountHistory());

var savings = new InterestEarningAccount("savings account", 10000);
savings.MakeDeposit(750, DateTime.Now, "save some money");
savings.MakeDeposit(1250, DateTime.Now, "Add more savings");
savings.MakeWithdraw(250, DateTime.Now, "Needed to pay monthly bills");
savings.PerformMonthEndTransactions();
Console.WriteLine(savings.GetAccountHistory());

var lineOfCredit = new LineOfCreditAccount("Line of Credit", 0, 20);
lineOfCredit.MakeWithdraw(2000m, DateTime.Now, "Take out monthly advance");
lineOfCredit.MakeDeposit(50m, DateTime.Now, "Pay back small amount");
lineOfCredit.MakeWithdraw(5000m, DateTime.Now, "Emergencyfunds for repairs");
lineOfCredit.MakeDeposit(150m, DateTime.Now, "Partial restoration of repairs");
lineOfCredit.PerformMonthEndTransactions();
Console.WriteLine(lineOfCredit.GetAccountHistory());

try
{
    account.MakeWithdraw(750, DateTime.Now, "Attempt to overdraw");
}
catch(InvalidOperationException e)
{
    Console.WriteLine("Exception caught trying to overdraw");
    Console.WriteLine(e.ToString());
}

BankAccount invalidAccount;
try
{
    invalidAccount = new BankAccount("Invalid", -55);
}
catch(ArgumentOutOfRangeException e)
{
    Console.WriteLine("Exception Caught");
    Console.WriteLine(e.ToString());
    return;
}