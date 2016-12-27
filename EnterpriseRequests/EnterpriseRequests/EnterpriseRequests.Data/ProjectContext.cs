using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerEnabledDbContext;

namespace EnterpriseRequests.Data
{
    public class ProjectContext : TrackerContext
    {
        public ProjectContext()
            : base("Name=ProjectContext")
        {

        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(ProjectContext).Assembly);
        }
    }
}
