using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intuit.Ipp.Core;
using Intuit.Ipp.Security;
using Intuit.Ipp.DataService;
using Intuit.Ipp.Data;
using System.Configuration;
using Konduskar_QuickBook.Util;

namespace Konduskar_QuickBook
{
    class Program
    {
      

        static void Main(string[] args)
        {
            Logger.WriteLog("Started...");
            Start();
        }

        public static void Start()
        {
            AccountsAPI accountsAPI = new AccountsAPI();

            Boolean toPostMasterData = Convert.ToBoolean(ConfigurationManager.AppSettings["PostMasterData"].ToString());
            if (toPostMasterData)
            {
                //string currenttime = Util.Util.GetServerDateTime().ToString("dd-MMM-yyy HH:mm:ss.fff");
                Logger.WriteLog("Master Data Started...");
                try
                {
                    accountsAPI.Main();
                }
                catch (Exception ex)
                {
                    Logger.WriteLog("Exception in Master Data: " + ex.Message);
                }
                Logger.WriteLog("Master Data Ended...");
            }
            Logger.WriteLog("Started1...");
            Boolean toPostTransactionData = Convert.ToBoolean(ConfigurationManager.AppSettings["PostTransactionData"].ToString());
            if (toPostTransactionData)
            {
                Boolean toPostBookedAgentVouchers = Convert.ToBoolean(ConfigurationManager.AppSettings["PostBookedAgentVouchers"].ToString());
                if (toPostBookedAgentVouchers)
                {
                    Logger.WriteLog("Konduskar Booked Agent Vouchers Started...");
                    try
                    {

                        accountsAPI.PostAgentVouchers();

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog("Konduskar Exception in Booked Agent Vouchers: " + ex.Message);
                    }
                    Logger.WriteLog("Konduskar Agent Vouchers Ended...");

                }


                Boolean toPostBookedAgentVouchersUpdate = Convert.ToBoolean(ConfigurationManager.AppSettings["PostBookedAgentVouchersUpdate"].ToString());
                if (toPostBookedAgentVouchersUpdate)
                {
                    Logger.WriteLog("Konduskar Booked Agent Vouchers Update Started...");
                    try
                    {

                        accountsAPI.UpdateAgentVouchers();

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog("Konduskar Exception in Booked Agent Vouchers Updation: " + ex.Message);
                    }
                    Logger.WriteLog("Konduskar Agent Vouchers Update Ended...");

                }


                Boolean toPostCancelledAgentVouchers = Convert.ToBoolean(ConfigurationManager.AppSettings["PostCancelledAgentVouchers"].ToString());
                if (toPostCancelledAgentVouchers)
                {
                    Logger.WriteLog("Konduskar Cancelled Agent Vouchers Credit Note Started...");
                    try
                    {

                        accountsAPI.PostAgentVouchersCancellation();

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog("Konduskar Exception in Cancelled Agent Vouchers  Credit Note: " + ex.Message);
                    }
                    Logger.WriteLog("Konduskar Agent Vouchers Cancelletion Ended...");

                }





                Boolean toPostBranchBookings = Convert.ToBoolean(ConfigurationManager.AppSettings["PostBranchBookings"].ToString());
                if (toPostBranchBookings)
                {
                    Logger.WriteLog("konduskar Bookings Started...");
                    try
                    {
                      
                        accountsAPI.PostBranchSalesEntry();
                       
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog("Exception in Bookings_konduskar: " + ex.Message);
                    }
                    Logger.WriteLog("konduskar Bookings Ended...");

                }

                Boolean toPostBranchBookingsUpdate = Convert.ToBoolean(ConfigurationManager.AppSettings["PostBranchBookingsUpdate"].ToString());
                if (toPostBranchBookingsUpdate)
                {
                    Logger.WriteLog("konduskar Bookings Updation Started...");
                    try
                    {

                        accountsAPI.UpdateBranchSalesEntry();

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog("Exception in Bookings_konduskar Updation: " + ex.Message);
                    }
                    Logger.WriteLog("konduskar Bookings Updation Ended...");

                }


                Boolean toPostBranchCancellation = Convert.ToBoolean(ConfigurationManager.AppSettings["PostBranchCancellation"].ToString());
                if (toPostBranchCancellation)
                {
                    Logger.WriteLog("konduskar Branch Cancellation Started...");
                    try
                    {

                        accountsAPI.PostBranchRefundReceiptEntry();

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog("Exception in Cancellations_konduskar: " + ex.Message);
                    }
                    Logger.WriteLog("konduskar Cancellation Ended...");

                }

                Boolean toPostBookedFranchiseVouchers = Convert.ToBoolean(ConfigurationManager.AppSettings["PostBookedFranchiseVouchers"].ToString());
                if (toPostBookedFranchiseVouchers)
                {
                    Logger.WriteLog("Konduskar Booked Franchise Vouchers Started...");
                    try
                    {

                        accountsAPI.PostFranchiseVouchers();

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog("Konduskar Exception in Booked Franchise Vouchers: " + ex.Message);
                    }
                    Logger.WriteLog("Konduskar Franchise Vouchers Ended...");

                }

            

                Boolean toCheckQBConnection = Convert.ToBoolean(ConfigurationManager.AppSettings["CheckQBConnection"].ToString());
                if (toCheckQBConnection)
                {
                    Logger.WriteLog("Check Qb Posting...");
                    try
                    {

                        accountsAPI.CheckPostingInQB();
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog("Check Qb Posting: " + ex.Message);
                    }
                    Logger.WriteLog("Check Qb Posting...");
                }



            }





            

        }
    }
}
