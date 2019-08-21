using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konduskar_QuickBook.Util
{
    public class EntryCounter
    {
        private static int QBCount = 0;
        private static int CRSCount = 0;
        private static EntryCounter entryCounter = new EntryCounter();
        private EntryCounter()
        { }

        public static EntryCounter GetInstance()
        {
            return entryCounter;
        }

        public void IncreaseQBCount(int number)
        {
            QBCount = QBCount + number;
        }

        public int GetQBCount()
        {
            return QBCount;
        }

        public void ResetQBCount()
        {
            QBCount = 0;
        }

        public void IncreaseCRSCount(int number)
        {
            CRSCount = CRSCount + number;
        }

        public int GetCRSCount()
        {
            return CRSCount;
        }

        public void ResetCRSCount()
        {
            CRSCount = 0;
        }

        public void ResetAllCount()
        {
            ResetQBCount();
            ResetCRSCount();
        }

        public Boolean IsQBCountEqualToCRSCount()
        {
            return QBCount == CRSCount ? true : false;
        }
    }
}
