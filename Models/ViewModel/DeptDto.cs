﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class DeptDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Names { get; set; }
    }
}
