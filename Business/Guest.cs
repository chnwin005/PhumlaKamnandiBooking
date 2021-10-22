using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhumlaKamnandiBooking.Business
{
    class Guest:Person
    {
        #region Class Info
        // Derived from the person class. 
        // This will represent guests of the Puhmla Kamnandi Hotels which may have a booking or not.
        // The guest will have a guestID representing the Primary key of the key when it comes to booking and as an identifier for the guest
        #endregion

        #region Data Members
        private int guestID;
        private string address, email;
        private Boolean billPaid, depositPaid, creditStatus;
        #endregion

        #region Property Methods
        public int getGuestID
        {
            get { return guestID; }
            set { guestID = value; }
        }
        public string getAddress
        {
            get { return address; }
            set { address = value; }
        }
        public string getEmail
        {
            get { return email; }
            set { email = value; }
        }

        public Boolean getDepositPaid
        {
            get { return depositPaid; }
            set { depositPaid = value; }
        }
        public Boolean getBillPaid
        {
            get { return billPaid; }
            set { billPaid = value; }
        }
        public Boolean getCreditStatus
        {
            get { return creditStatus; }
            set { creditStatus = value; }
        }
        #endregion

        #region Constructor
        public Guest()
        {
           
        }
        #endregion
    }
}
