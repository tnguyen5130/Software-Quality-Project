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

        public Invoice() { }

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

        public void Delete()
        {
            new InvoiceBizDAO().DeleteInvoice(this);
        }

        public Invoice GetById(string invoiceID)
        {
            var invoices = new InvoiceBizDAO().GetInvoices(invoiceID);
            return invoices[0];
        }

        public List<Invoice> GetInvoices(string pattern)
        {
            var invoiceList = new InvoiceBizDAO().GetInvoices(pattern);
            return invoiceList;
        }

        public string generateBillingID(int seq)
        {
            // Naming INVO + billingID
            return null;
        }
    }
}
