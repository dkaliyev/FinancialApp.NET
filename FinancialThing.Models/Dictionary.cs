﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class Dictionary: Entity
    {
        public virtual string DisplayName { get; set; }
        public virtual string Code { get; set; }
        public virtual string ParentCode { get; set; }
        public virtual string SiteName { get; set; }
        public virtual string Order { get; set; }
    }
}
