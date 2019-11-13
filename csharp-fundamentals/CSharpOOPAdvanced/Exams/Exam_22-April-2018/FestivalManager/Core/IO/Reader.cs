using FestivalManager.Core.IO.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FestivalManager.Core.IO
{
    public class Reader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
