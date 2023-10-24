using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekSixDataBaseTextAssignment.StuffForFiles;

namespace WeekSixDataBaseTextAssignment.DataBaseCode
{
    internal class ProduceAccess
    {
        private string sqlConnectString = string.Empty;
        private DataBaseConnectSingleton connect;
        public ProduceAccess()
        {
            connect = DataBaseConnectSingleton.Instance();
            sqlConnectString = connect.PrepareDBConnect();
        }

        public List<Produce> SelectAllProduce()
        {
            List<Produce> output = new List<Produce>();

            //This opens the connection to the table and creates a string using a SQL command
            //Learned from Week 6 Powerpoint Prenstation
            using (SqlConnection conn = new SqlConnection(sqlConnectString))
            {
                conn.Open();
                string inlineSQL = @"SELECT * FROM Produce";
                
                //With the using statement, it creates a SQL Command object and will go through as a string and object
                using (var command = new SqlCommand(inlineSQL, conn))
                {
                    //This allows to read the data from the table which are strings, decimals, and DateTime
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var value = new Produce((string)reader.GetValue(1), (string)reader.GetValue(2), (decimal)reader.GetValue(3), (string)reader.GetValue(4), (DateTime)reader.GetValue(5));
                        
                        output.Add(value);
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return output;
        }
        //Learned from Week 6 Powerpoint Prenstation
        public void InsertProduce(Produce produce)
        {
            using (SqlConnection conn = new SqlConnection(sqlConnectString))
            {
                conn.Open();

                string inlineSQL = @$"INSERT [dbo].[Produce] ([Name], [Location], [Price], [UoM], [Sell_by_Date]) Values('{produce.Name}', '{produce.Location}', {produce.Price}, '{produce.UoM}', '{produce.SellByDate.ToString("MM-dd-yyyy")}')";
                using (var command = new SqlCommand(inlineSQL, conn))
                {
                    var query = command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        //I used code reference from the InsertProduce, to make a code so that data can change the location from 'F' to 'Z'
        public void ChangeLocation()
        {
            using (SqlConnection conn = new SqlConnection(sqlConnectString))
            {
                conn.Open();
                //Learned about the UPDATE function from here: https://www.geeksforgeeks.org/sql-update-statement/
                //Learned about the REPLACE function from here: https://www.sqlteam.com/articles/using-replace-in-an-update-statement
                string inlineSQL = @$"UPDATE Produce Set Location = REPLACE(Location, 'F', 'Z')";
                using (var command = new SqlCommand(inlineSQL, conn))
                {
                    var query = command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        //I used code reference from the InsertProduce, to make a code so that it can delete any Produce that are past the Sell_by_Date
        public void DeleteProduceAfterSellBy()
        {
            using (SqlConnection conn = new SqlConnection(sqlConnectString))
            {
                conn.Open();

                //Learned about the DELETE function from here: https://www.w3schools.com/sql/sql_delete.asp
                //Learned about the GETDATE function from here: https://www.mssqltips.com/sqlservertutorial/9386/sql-getdate-function
                string inlineSQL = @$"DELETE FROM PROG260FA23.dbo.Produce WHERE Sell_by_Date < cast(GETDATE() as date)";
                using (var command = new SqlCommand(inlineSQL, conn))
                {
                    var query = command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        //I used code reference from the InsertProduce, to make a code so that it can change the prices of any Produce left from the DeleteProduceAfterSellBy
        public void ChangeAllPrices()
        {
            using (SqlConnection conn = new SqlConnection(sqlConnectString))
            {
                conn.Open();

                //Learned about the UPDATE function from here: https://www.geeksforgeeks.org/sql-update-statement/
                string inlineSQL = @$"UPDATE [PROG260FA23].[dbo].[Produce] SET Price = Price + 1";
                using (var command = new SqlCommand(inlineSQL, conn))
                {
                    var query = command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
