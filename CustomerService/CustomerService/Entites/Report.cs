﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Entites
{
    public class Report
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime GeneratedDate { get; set; }
        public string Content { get; set; }
    }
}
