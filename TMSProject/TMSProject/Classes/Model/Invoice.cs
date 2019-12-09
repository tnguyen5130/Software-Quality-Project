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
        public string invoiceDate { get; set; }
        public string completeStatus { get; set; }
        public string command { get; set; }

        //extended properties
        public string orderID { get; set; }
        public string customerName { get; set; }
        public string orderDate { get; set; }
        public string customerCity { get; set; }
        public string telno { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        public string customerCompany { get; set; }
        public string customerProvince { get; set; }
        public string startCityName { get; set; }
        public string endCityName { get; set; }
        public int jobType { get; set; }


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

        public List<Invoice> ViewInvoices(string billingID, string orderID)
        {
            var invoiceList = new InvoiceBizDAO().ViewInvoices(billingID, orderID);
            return invoiceList;
        }

        public List<Invoice> GetInvoiceID(string invoiceID)
        {
            var invoiceList = new InvoiceBizDAO().GetInvoiceID(invoiceID);
            return invoiceList;
        }

        public string generateBillingID(int seq)
        {
            // Naming INVO + billingID
            return null;
        }
    }
}

