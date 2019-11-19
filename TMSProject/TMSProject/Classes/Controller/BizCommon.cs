using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSProject.Classes.Controller
{
    public class BizCommon
    {
        public string newOrderID()
        {
            return null;
        }

        public bool fieldsValidation()
        {
            return false;
        }

        public string getInitiated_By()
        {
            return null;
        }

        public string newTripID()
        {
            return null;
        }

        public string newPlanID()
        {
            return null;
        }

        public string newBillingID()
        {
            return null;
        }

        public string newInvoiceID(int seq)
        {
            string value = String.Format("{0:D3}", seq);
            return "INV" + value;
        }
    }
}
