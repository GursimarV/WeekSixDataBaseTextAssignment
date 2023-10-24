using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekSixDataBaseTextAssignment.StuffForFiles
{
    //TheFiles class inherits from the IFile interface
    internal class TheFiles : IFile
    {
        //Learned from assignment 4 
        public string FilePath { get; set; }
        public string? Delimiter { get; set; } = null;
        public string Extension { get; set; }

        public TheFiles(string filepath, string extension, string delimiter = null)
        {
            FilePath = filepath;
            Delimiter = delimiter;
            Extension = extension;
        }
    }
}
