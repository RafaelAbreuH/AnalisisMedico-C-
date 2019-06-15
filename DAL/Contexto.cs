using AnalisisMedico.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisisMedico.DAL
{
    public class Contexto: DbContext
    {
        public DbSet<Usuarios> Usuario { get; set; }
        public DbSet<TipoAnalisis> Tipoanalisis { get; set; }
        public DbSet<Analisis> Analisi { get; set; }

        public Contexto() : base("ConStr")
        { }
    }
}
