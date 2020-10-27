using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maersk.Sorting.Api.Data
{
    public class SortJobDbContext:DbContext
    {
        public SortJobDbContext(DbContextOptions<SortJobDbContext> dbContextOptions):base(dbContextOptions)
        {

        }
        public DbSet<SortJob> SortJobC { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SortJob>()
                .Property(b => b.Output)
                .HasDefaultValue("");
          
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<SortJob>()
        //        .Property(b => b.Output)
        //        .IsRequired(false)//optinal case
        //        .IsRequired();//required case;
        //    modelBuilder.Entity<SortJob>()
        //       .Property(b => b.Input)
        //       .IsRequired(false)//optinal case
        //       .IsRequired();//required case;


        //}
    }
}
