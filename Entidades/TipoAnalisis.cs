using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisisMedico.Entidades
{
    public class TipoAnalisis
    {
        [Key]
        public int IdTipoAnalisis { get; set; }
        public string Descripcion { get; set; }

        public TipoAnalisis()
        {
            IdTipoAnalisis = 0;
            Descripcion = String.Empty;
        }
    }
}
