/*
 Made by: Gursimar Virdi
 Date: 10/19/2023
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WeekSixDataBaseTextAssignment
{
    internal class Program
    {
        //Direct Path to the Files folder, I used this code from Assignment 4/5
        private const string foldername = "Files";
        public static string directPath => Path.Combine(Directory.GetCurrentDirectory(), foldername);
        static void Main(string[] args)
        {
            FileParse parser = new FileParse();

            parser.ParsingFiles(directPath);
        }
    }
}