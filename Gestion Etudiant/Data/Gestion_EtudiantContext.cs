using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gestion_Etudiant.Models;

namespace Gestion_Etudiant.Data
{
    public class Gestion_EtudiantContext : DbContext
    {
        public Gestion_EtudiantContext (DbContextOptions<Gestion_EtudiantContext> options)
            : base(options)
        {
        }

        public DbSet<Gestion_Etudiant.Models.StudentModel> StudentModel { get; set; }
    }
}
