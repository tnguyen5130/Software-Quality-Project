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
    public class Invoice
    {
        public string invoiceID { get; set; }
        public string billingID { get; set; }
        public string contractID { get; set; }
        public string customerID { get; set; }
        public string completeStatus { get; set; }
        public string command { get; set; }

        public string orderID { get; set; }

        public Invoice() { }

        public bool Save()
        {
            bool flag = false;
            if (command == "UPDATE")
            {
                flag = new InvoiceBizDAO().UpdateInvoice(this);
            }
            else
            {
                flag = new InvoiceBizDAO().InsertInvoice(this);
            }

            return flag;
        }

        public void Delete()
        {
            new InvoiceBizDAO().DeleteInvoice(this);
        }

        public Invoice GetById(string billingID, string orderID)
        {
            var invoices = new InvoiceBizDAO().GetInvoices(invoiceID, orderID);
            return invoices[0];
        }

        public List<Invoice> GetInvoices(string billingID, string orderID)
        {
            var invoiceList = new InvoiceBizDAO().GetInvoices(billingID, orderID);
            return invoiceList;
        }

        public string generateBillingID(int seq)
        {
            // Naming INVO + billingID
            return null;
        }
    }
}

