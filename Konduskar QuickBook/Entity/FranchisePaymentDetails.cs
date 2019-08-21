using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konduskar_QuickBook
{

    public enum PaymentType1
    {
        Cash,
        Check
    };
    class FranchisePaymentDetails
    {

            public int TransactionID { get; set; }
            public int inVoiceID { get; set; }
            public decimal ActualAmount { get; set; }
            public decimal TotalAmount { get; set; }
            public DateTime PaymentDate { get; set; }
            public string FranchiseName { get; set; }
            public int QuickBookCustomerID { get; set; }
            public string PaymentType { get; set; }
            public int PaymentTypeID { get; set; }
            public string InstrumentNo { get; set; }
            public string DepositeToName { get; set; }
            public int DepositeToId { get; set; }
            public string TxnDate { get; set; }
            public int franchiseVoucherReceiptsID { get; set; }
            public int isHO { get; set; }
            public string UserLedgerName { get; set; }
            public int UserLedgerId { get; set; }
            public string PaymentDrLedgerName { get; set; }
            public int PaymentDrLedgerID { get; set; }
            public string PaymentCrLedgerName { get; set; }
            public int PaymentCrLedgerID { get; set; }
            public int BranchDivisionID { get; set; }
            public int DepositeToDivisionID { get; set; }
            public int BranchDivisionIDJE { get; set; }
            public string BranchDivisionNameJE { get; set; }
            public int BranchDivisionIDJE2 { get; set; }
            public string BranchDivisionNameJE2 { get; set; }
            public int CompanyID { get; set; }
        
    }
}
