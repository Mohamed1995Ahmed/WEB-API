using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Mpdels;

namespace Models.Data
{
    public class AppDBcontext:IdentityDbContext<User>
    {
       public  DbSet<Department> Departments { get; set; }
        public AppDBcontext(DbContextOptions<AppDBcontext>options):base(options)
        {

        }
      
    }
}
