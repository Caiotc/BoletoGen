﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoletoGen2.Models
{
    public class Permission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Type { get; set; }
    }
}