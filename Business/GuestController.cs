using PhumlaKamnandiBooking.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhumlaKamnandiBooking.Business
{
    class GuestController
    {
        #region Data Members
        guestDB guestDB;
        Collection<Guest> guests;
        #endregion

        #region Properties
        public Collection<Guest> Allguests
        {
            get
            {
                return guests;
            }
        }
        #endregion

        #region Constructor
        public GuestController()
        {
            //***instantiate the guestB object to communicate with the database
            guestDB = new guestDB();
            guests = guestDB.AllGuests;
        }
        #endregion

        #region Database Communication.
        public void DataMaintenance(Guest aGuest, DB.DBOperation operation)
        {
            int index = 0;
            //perform a given database operation to the dataset in meory; 
            guestDB.DataSetChange(aGuest, operation);
            //perform operations on the collection
            switch (operation)
            {
                case DB.DBOperation.Add:
                    //*** Add the employee to the Collection
                    guests.Add(aGuest);
                    break;
                case DB.DBOperation.Edit:
                    index = FindIndex(aGuest);
                    guests[index] = aGuest;  // replace employee at this index with the updated employee
                    break;
                case DB.DBOperation.Delete:
                    index = FindIndex(aGuest);  // find the index of the specific employee in collection
                    guests.RemoveAt(index);  // remove that employee form the collection
                    break;

            }
        }
        //***Commit the changes to the database
        public bool FinalizeChanges(Guest guest)
        {
            //***call the EmployeeDB method that will commit the changes to the database
            return guestDB.UpdateDataSource(guest);
        }
        #endregion

        #region Searching through a collection
        public Collection<Guest> FindByCredit(Collection<Guest> guests, bool value)
        {
            Collection<Guest> matches = new Collection<Guest>();
            foreach (Guest guest in guests)
            {
                if (guest.getCreditStatus == value)
                {
                    matches.Add(guest);
                }
            }
            return matches;
        }

        //This method receives a guest ID as a parameter; finds the guest object in the collection of guests and then returns this object
        public Guest Find(int ID)
        {
            int index = 0;
            bool found = (guests[index].getGuestID == ID);  //check if it is the first student
            int count = guests.Count;
            while (!(found) && (index < guests.Count - 1))  //if not "this" student and you are not at the end of the list 
            {
                index = index + 1;
                found = (guests[index].getGuestID == ID);   // this will be TRUE if found
            }
            return guests[index];  // this is the one!  
        }

        public int FindIndex(Guest aGuest)
        {
            int counter = 0;
            bool found = false;
            found = (aGuest.getGuestID == guests[counter].getGuestID);   //using a Boolean Expression to initialise found
            while (!(found) & counter < guests.Count - 1)
            {
                counter += 1;
                found = (aGuest.getGuestID == guests[counter].getGuestID);
            }
            if (found)
            {
                return counter;
            }
            else
            {
                return -1;
            }
        }
        #endregion

    }
}
