﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ifood_back.Policies
{
    public class OnlyExpensiveMastreRequirement : IAuthorizationRequirement
    {
        
    }
}
