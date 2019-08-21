using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konduskar_QuickBook.Entity
{
    public class SaleEntryDetail
    {
        public int SaleEntryDetailId { get; set; }
        public int AccSysId { get; set; }
        public string LedgerName { get; set; }
        public int IsDebit { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int AccSysDivisionId { get; set; }
        public string DivisionName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassIdQB { get; set; }
        public string Payeeid { get; set; }
        public string PayeeName { get; set; }
        public string PayeeType { get; set; }
    }
}
