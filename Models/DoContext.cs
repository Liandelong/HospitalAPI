using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Models
{
     public class DoContext:DbContext
    {
        public DoContext(DbContextOptions<DoContext> options):base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Hospitals> Hospitals { get; set; }
        public DbSet<HealthNews> HealthNews { get; set; }
        public DbSet<NewsKinds> NewsKinds { get; set; }
        public DbSet<DiseaseKnowledge> DiseaseKnowledges { get; set; }
        public DbSet<DiseaseKinds> DiseaseKinds { get; set; }
        public DbSet<Problems> Problems { get; set; }
        public DbSet<Answers> Answers { get; set; }
    }
}
