using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekSixDataBaseTextAssignment.StuffForFiles
{
    internal interface IFile
    {
        //interface with the delimiter for the pipe file, the file path, and extension of the file, learned from assignment 4
        string FilePath { get; set; }
        string? Delimiter { get; set; }
        string Extension { get; set; }
    }
}
