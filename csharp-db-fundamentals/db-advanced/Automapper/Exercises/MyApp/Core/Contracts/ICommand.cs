﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Core.Contracts
{
    public interface ICommand
    {
        string Execute(string[] inputArgs);
    }
}
