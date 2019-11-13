using System;
using System.Collections.Generic;
using System.Text;

namespace DBAppsIntroduction
{
    public static class Configuration
    {
        public const string ConnectionString = @"Server=.\SQLEXPRESS; " +
                 "Database=MinionsDB; " +
                "Integrated Security=true";
    }
}
