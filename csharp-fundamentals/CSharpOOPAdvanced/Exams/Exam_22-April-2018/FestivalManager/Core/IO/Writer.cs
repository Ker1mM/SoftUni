using FestivalManager.Core.IO.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FestivalManager.Core.IO
{
    public class Writer : IWriter
    {
        public void Write(string contents)
        {
            Console.Write(contents);
        }

        public void WriteLine(string contents)
        {
            Console.WriteLine(contents);
        }
    }
}
