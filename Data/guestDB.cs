using PhumlaKamnandiBooking.Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhumlaKamnandiBooking.Data
{
    class guestDB:DB
    {
        #region  Data members        
        private string table1 = "Guest";
        private string sqlLocal1 = "SELECT * FROM Guest";
        private Collection<Guest> guests;
        #endregion

        #region Property Method: Collection
        public Collection<Guest> AllGuests
        {
            get
            {
                return guests;
            }
        }
        #endregion

        #region Utility Methods
        public DataSet GetDataSet()
        {
            return dsMain;
        }
        private void Add2Collection(string table)
        {
            DataRow myRow = null;
            Guest aGuest;

            foreach (DataRow myRow_loopVariable in dsMain.Tables[table].Rows)
            {
                myRow = myRow_loopVariable;
                if (!(myRow.RowState == DataRowState.Deleted))
                {
                    //Instantiate a new guest object
                    aGuest = new Guest();
                    //Obtain each guest attribute from the specific field in the row in the table
                    aGuest.getGuestID = Convert.ToInt32(myRow["GuestID"]);
                    //Do the same for all other attributes
                    aGuest.getFirstName = Convert.ToString(myRow["FisrtName"]).TrimEnd();
                    aGuest.getSurname = Convert.ToString(myRow["surname"]).TrimEnd();
                    aGuest.getAge = Convert.ToInt32(myRow["Age"]);
                    aGuest.getAddress = Convert.ToString(myRow["Address"]).TrimEnd();
                    aGuest.getEmail = Convert.ToString(myRow["Email"]).TrimEnd();
                    aGuest.getDepositPaid = Convert.ToBoolean(myRow["DepositPaid"]);
                    aGuest.getBillPaid = Convert.ToBoolean(myRow["BillPaid"]);
                    aGuest.getCreditStatus = Convert.ToBoolean(myRow["CreditStatus"]);

                    guests.Add(aGuest);
                }
            }
        }
        private void FillRow(DataRow aRow, Guest aGuest, DB.DBOperation operation)
        {
            
            if (operation == DB.DBOperation.Add)
            {
                aRow["GuestID"] = aGuest.getGuestID;  //NOTE square brackets to indicate index of collections of fields in row.
            }

            aRow["FirstName"] = aGuest.getFirstName;
            aRow["Surname"] = aGuest.getSurname;
            aRow["Age"] = aGuest.getAge;
            aRow["Address"] = aGuest.getAddress;
            aRow["Email"] = aGuest.getEmail;
            aRow["DepositPaid"] = aGuest.getDepositPaid;
            aRow["BillPaid"] = aGuest;
            aRow["CreditStatus"] = aGuest.getCreditStatus;
        }

        private int FindRow(Guest aGuest, string table)
        {
            int rowIndex = 0;
            DataRow myRow;
            int returnValue = -1;
            foreach (DataRow myRow_loopVariable in dsMain.Tables[table].Rows)
            {
                myRow = myRow_loopVariable;
                //Ignore rows marked as deleted in dataset
                if (!(myRow.RowState == DataRowState.Deleted))
                {
                    //In c# there is no item property (but we use the 2-dim array) it is automatically known to the compiler when used as below
                    if (aGuest.getGuestID == Convert.ToInt32(dsMain.Tables[table].Rows[rowIndex]["GuestID"]))
                    {
                        returnValue = rowIndex;
                    }
                }
                rowIndex += 1;
            }
            return returnValue;
        }

        #endregion

        #region Database Operations CRUD
        public void DataSetChange(Guest aGuest, DB.DBOperation operation)
        {
            DataRow aRow = null;
            string dataTable = table1;
           
            switch (operation)
            {
                case DB.DBOperation.Add:
                    aRow = dsMain.Tables[dataTable].NewRow();
                    FillRow(aRow, aGuest, operation);
                    dsMain.Tables[dataTable].Rows.Add(aRow);
                    //Add to the dataset
                    break;
                case DB.DBOperation.Edit:
                    // For the Edit section you have to find a row instead of creating a new row.
                    aRow = dsMain.Tables[dataTable].Rows[FindRow(aGuest, dataTable)];
                    //Fill this row for the Edit operation by calling the FillRow method
                    FillRow(aRow, aGuest, operation);
                    break;
                case DB.DBOperation.Delete:
                    aRow = dsMain.Tables[dataTable].Rows[FindRow(aGuest, dataTable)];
                    aRow.Delete();
                    break;
            }
        }
        #endregion

        #region Build Parameters, Create Commands & Update database
        private void Build_INSERT_Parameters(Guest aGuest)
        {
            //Create Parameters to communicate with SQL INSERT...add the input parameter and set its properties.
            SqlParameter param = default(SqlParameter);

            param = new SqlParameter("@GuestID", SqlDbType.Int, 10, "GuestID");
            daMain.InsertCommand.Parameters.Add(param);//Add the parameter to the Parameters collection.

            param = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50, "FirstName");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Surname", SqlDbType.NVarChar, 50, "Surname");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Age", SqlDbType.Int, 10, "Age");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Address", SqlDbType.NVarChar, 50, "Address");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Email", SqlDbType.NVarChar, 50, "Email");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@DepositPaid", SqlDbType.NChar, 10, "DepositPaid");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@BillPaid", SqlDbType.NChar, 10, "BillPaid");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@CreditStatus", SqlDbType.NChar, 10, "CreditStatus");
            daMain.InsertCommand.Parameters.Add(param);

        }

        private void Build_UPDATE_Parameters(Guest aGuest)
        {
            SqlParameter param = default(SqlParameter);

            param = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50, "FirstName");
            param.SourceVersion = DataRowVersion.Current;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Surname", SqlDbType.NVarChar, 50, "Surname");
            param.SourceVersion = DataRowVersion.Current;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Age", SqlDbType.Int, 10, "Role");
            param.SourceVersion = DataRowVersion.Current;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Address", SqlDbType.NVarChar, 50, "Address");
            param.SourceVersion = DataRowVersion.Current;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Email", SqlDbType.NVarChar, 50, "Email");
            param.SourceVersion = DataRowVersion.Current;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@DepositPaid", SqlDbType.NChar, 10, "DepositPaid");
            param.SourceVersion = DataRowVersion.Current;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@BillPaid", SqlDbType.NChar, 10, "BillPaid");
            param.SourceVersion = DataRowVersion.Current;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@CreditStatus", SqlDbType.NChar, 10, "CreditStatus");
            param.SourceVersion = DataRowVersion.Current;
            daMain.UpdateCommand.Parameters.Add(param);


            param = new SqlParameter("@Original_ID", SqlDbType.NVarChar, 15, "GuestID");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);
        }

        private void Create_INSERT_Command(Guest aGuest)
        {
            //Create the command that must be used to insert values into the Guest table..

            daMain.InsertCommand = new SqlCommand("INSERT into Guest (GuestID, FirstName, Surname, Age, Address, Email, DepositPaid, BillPaid, CreditStatus) VALUES (@GuestID, @FirstName, @Surname, @Age, @Address, @Email, @DepositPaid, @BillPaid, @CreditStatus)", cnMain);

            Build_INSERT_Parameters(aGuest);
        }
        private void Build_DELETE_Parameters()
        {
            //--Create Parameters to communicate with SQL DELETE
            SqlParameter param;
            param = new SqlParameter("@GuestID", SqlDbType.Int, 10, "GuestID");
            param.SourceVersion = DataRowVersion.Original;
            daMain.DeleteCommand.Parameters.Add(param);
        }
        private void Create_UPDATE_Command(Guest aGuest)
        {
            daMain.UpdateCommand = new SqlCommand("UPDATE Guest SET FirstName =@FirstName, Surname =@Surname, Age =@Age, Address =@Address, Email =@Email, DepositPaid =@DepositPaid, BillPaid =@BillPaid, CreditStatus =@CreditStatus " + "WHERE GuestID = @Original_ID", cnMain);

            Build_UPDATE_Parameters(aGuest);
        }
        private string Create_DELETE_Command(Guest aGuest)
        {
            string errorString = null;
            //Create the command that must be used to delete values from the the appropriate table
            daMain.DeleteCommand = new SqlCommand("DELETE FROM HeadWaiter WHERE GestID = @GuestID", cnMain);
            try
            {
                Build_DELETE_Parameters();
            }
            catch (Exception errObj)
            {
                errorString = errObj.Message + "  " + errObj.StackTrace;
            }
            return errorString;
        }

        public bool UpdateDataSource(Guest aGuest)
        {
            bool success = true;
            Create_INSERT_Command(aGuest);
            Create_UPDATE_Command(aGuest);

            success = UpdateDataSource(sqlLocal1, table1);

            return success;
        }

        #endregion
    }
}
