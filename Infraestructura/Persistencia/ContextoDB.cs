using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Persistencia
{
    public class ContextoDB : DbContext
    {
        public ContextoDB(DbContextOptions<ContextoDB> options) : base(options)
        {
        }

        //Definiciones de DbSets para las entidades
        public DbSet<Persona> Personas { get; set; }

    }
}
