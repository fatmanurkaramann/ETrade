﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Application.Abstraction.Services.Authentication
{
    public interface IInternalAuth
    {
        Task LoginAsync();
    }
}
