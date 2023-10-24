using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekSixDataBaseTextAssignment.StuffForFiles
{
    internal class FilePathAndType
    {
        //The extensions of the files in the files folder
        public sealed class FileExtensions
        {
            public static string Text => ".txt";
            public static string Pipe => ".txt";
        }

        //The delimiters that affects the pipe file used in assignment 4
        public sealed class FileDelimieters
        {
            public static string Pipe => "|";
        }
    }
}
