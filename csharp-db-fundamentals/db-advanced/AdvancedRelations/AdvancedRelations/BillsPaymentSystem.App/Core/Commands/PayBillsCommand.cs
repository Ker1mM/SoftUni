using BillsPaymentSystem.App.Core.Contracts;
using BillsPaymentSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsPaymentSystem.App.Core.Commands
{
    public class PayBillsCommand : ICommand
    {

        private BillsPaymentSystemContext context;

        public PayBillsCommand(BillsPaymentSystemContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            var withdraw = new WithdrawCommand(context);

            withdraw.Execute(args);

            return "All bills are paid!";
        }
    }
}
