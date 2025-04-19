using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace Models.Mpdels
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ManagerName { get; set; }
        public List<Employee>? Employees { get; set; }

       

    }
}
