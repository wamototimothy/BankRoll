using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class LineOfCreditAccount : BankAccount
    {
        public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) : base(name, initialBalance, -creditLimit)
        {
        }
        public override void PerformMonthEndTransactions()
        {
            if (Balance<0)
            {
                decimal interest = -Balance * 0.07m;
                MakeWithdraw(interest, DateTime.Now, "Charge Monthly Interest");
            }
            base.PerformMonthEndTransactions();
        }
        protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) =>
            isOverdrawn ? new Transaction(-20, DateTime.Now, "Apply Overdraft fee")
            : default;
        
          
        
    }

}
