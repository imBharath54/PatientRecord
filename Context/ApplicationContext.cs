using Microsoft.EntityFrameworkCore;
using PatientRecordMicroService.Models;
using System.Collections.Generic;

namespace PatientRecordMicroService.Context
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        // Define the DbSet for Record
        public DbSet<Record> Records { get; set; }
    }
}
