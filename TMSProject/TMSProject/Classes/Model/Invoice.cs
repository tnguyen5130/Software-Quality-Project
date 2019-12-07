//* FILE			: Invoice.cs
//* PROJECT			: SENG2020-19F-Sec1-Software Quallity - Group Project 
//* PROGRAMMER		: Nhung Luong, Yonchul Choi, Trung Nguyen, Adullar - Project Slinger
//* FIRST VERSON	: Nov 11, 2019
//* DESCRIPTION		: The file defines a class  : Invoice for the TMS system






using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSProject.Classes.Controller;

namespace TMSProject.Classes.Model
{
    /// \class Invoice
    /// \brief This class contains the Invoice Info
    /// \author : <i>Yonchul Choi <i>
    public class Invoice
    {
        /// <summary>
        /// A string to get invoiceID 
        /// A string to get billingID
        /// A string to get contractID
        /// A string to get customerID
        /// A string to get completeStatus
        /// </summary>
        public string invoiceID { get; set; }
        public string billingID { get; set; }
        public string contractID { get; set; }
        public string customerID { get; set; }
        public string completeStatus { get; set; }

        public Invoice() { }


        /// \brief This method Save
        /// \details <b>Details</b>
        /// This method will save invoice Info
        /// \return  void
        public void Save()
        {
            if (invoiceID != "")
            {
                new InvoiceBizDAO().UpdateInvoice(this);
            }
            else
            {
                new InvoiceBizDAO().InsertInvoice(this);
            }
        }


        /// \brief This method Delete
        /// \details <b>Details</b>
        /// This method will save invoice Info
        /// \return  void
        public void Delete()
        {
            new InvoiceBizDAO().DeleteInvoice(this);
        }

        public Invoice GetById(string invoiceID)
        {
            var invoices = new InvoiceBizDAO().GetInvoices(invoiceID);
            return invoices[0];
        }


        /// \brief This method Save
        /// \details <b>Details</b>
        /// This method will save employee Info
        /// \return  void
        public List<Invoice> GetInvoices(string pattern)
        {
            var invoiceList = new InvoiceBizDAO().GetInvoices(pattern);
            return invoiceList;
        }


        /// \brief This method generateBillingID
        /// \details <b>Details</b>
        /// This method will save generate Billing ID Info
        /// \return  void
        public string generateBillingID(int seq)
        {
            // Naming INVO + billingID
            return null;
        }
    }
}
