using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Mpdels;

namespace Models.Data
{
    public class AppDBcontext:DbContext
    {
       public  DbSet<Department> Departments { get; set; }
        public AppDBcontext(DbContextOptions<AppDBcontext>options):base(options)
        {

        }
      
    }
}
