using System;
using System.Collections.Generic;
using System.Text;

namespace BookCat
{
    public class FileWork
    {
        public static string getTempFileName()
        {
            return System.IO.Path.GetTempFileName();
        }
    }
}
