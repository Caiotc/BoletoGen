﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoGen2.Models.Interface
{
    public interface IUserRepository
    {
      
            User SelectUser(Guid UserId);
       
    }
}
