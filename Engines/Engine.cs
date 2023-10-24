using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeekSixDataBaseTextAssignment.StuffForFiles;
using WeekSixDataBaseTextAssignment.DataBaseCode;

namespace WeekSixDataBaseTextAssignment.Engines
{
    //Help from Leo's InClassDemo with reminding to make this an abstract class since the abstract class can be used by other classes like DeLimitEngine
    internal abstract class Engine
    {
        public ProduceAccess produceAccess { get; set; }
        public Engine() 
        {
            produceAccess = new ProduceAccess();
        }

        internal virtual List<ErrorCheck> ProcessFile(IFile file)
        {
            //To help check for any errors in the code and was used from Assignment #5
            List<ErrorCheck> ShowError = new List<ErrorCheck>();

            string resultFile = CreateResultFile(file);

            try
            {
                List<Produce> produce = ParseTheProduce(file);

                List<Produce> output = SQLChanges(produce);

                //Uses StreamWriter to write out the processed files into a new file, used from assignment 3 and 4
                using (StreamWriter sourceWrite = new StreamWriter(resultFile))
                {
                    sourceWrite.WriteLine("Name,Location,Price,UoM,Sell_by_Date");
                    foreach (var item in output)
                    {
                        sourceWrite.WriteLine(item.ToString());
                    }
                }
            }
            //To catch errors that are in the code, learned from in class demo in week 5
            catch (IOException ioe)
            {
                ShowError.Add(new ErrorCheck(ioe.Message, ioe.Source ?? "Unknown"));
            }
            catch (Exception ex)
            {
                ShowError.Add(new ErrorCheck(ex.Message, ex.Source ?? "Unknown"));
            }

            return ShowError;
        }

        //This creates the Produce_out file 
        internal string CreateResultFile(IFile file)
        {
            //Learned and reused from assignment #5
            string resultFilePath = file.FilePath.Replace(file.Extension, $"_out{FilePathAndType.FileExtensions.Text}");

            if (File.Exists(resultFilePath))
            {
                File.Delete(resultFilePath);
            }

            return resultFilePath;
        }

        internal virtual List<Produce> ParseTheProduce(IFile file)
        {
            List<Produce> Produce = new List<Produce>();

            //Uses StreamReader to read the file, used from assignment 3 and 4
            using (StreamReader sourceRead = new StreamReader(file.FilePath))
            {
                var line = sourceRead.ReadLine();
                if (line.StartsWith("Name,"))
                {
                    line = sourceRead.ReadLine();
                }

                while (line != null)
                {
                    //Splitting the the text in the file with line.Split with the character '|'
                    var prop = line.Split('|');

                    string name = prop[0];
                    string location = prop[1];
                    decimal price = Convert.ToDecimal(prop[2]);
                    string uom = prop[3];

                    //Splitting the DateTime in the file with another kind of line.Split with the string "-"  
                    //Learned about Parsing from Assignment 4/5 and help from: https://www.geeksforgeeks.org/int32-parsestring-method-in-c-sharp-with-examples/
                    var split = prop[4].Split("-");
                    DateTime sellBy = new DateTime(Int32.Parse(split[2]), Int32.Parse(split[0]), Int32.Parse(split[1]));

                    //Adds the props after it gets Parsed 
                    Produce.Add(new Produce(name, location, price, uom, sellBy));

                    line = sourceRead.ReadLine();
                }
            }
            return Produce;
        }

        //Runs every SQL commands that are called to maintain the Produce table
        //The List is the List of Produce objects named produce
        internal List<Produce> SQLChanges(List<Produce> produce)
        {
            ProduceAccess produceAccess = new ProduceAccess();
            //Insert items from Produce.txt into DB
            foreach (Produce product in produce)
            {
                produceAccess.InsertProduce(product);
            }

            //Changes the location that end with F to Z
            produceAccess.ChangeLocation();

            //Delete any item that is passed the sell by date
            produceAccess.DeleteProduceAfterSellBy();

            //Increases the prices of the produces by $1
            produceAccess.ChangeAllPrices();

            //Returns the list after all the SQL Commands have been done
            return produceAccess.SelectAllProduce();
        }
    }
}
