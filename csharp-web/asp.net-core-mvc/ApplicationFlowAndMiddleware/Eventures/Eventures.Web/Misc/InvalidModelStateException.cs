using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Web.Misc
{
    [Serializable]
    public class InvalidModelStateException : Exception
    {

        public InvalidModelStateException()
        {
        }

        public InvalidModelStateException(string message) : base(message)
        {

        }
    }
}
