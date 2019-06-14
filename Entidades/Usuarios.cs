using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisisMedico.Entidades
{
    public class Usuarios
    {
         
        [Key]
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public Usuarios()
        {
            UsuarioId = 0;
            Nombre = String.Empty;
            Telefono = String.Empty;
            Sexo = string.Empty;
            Email = String.Empty;
            FechaNacimiento = DateTime.Now;
        }
    }
}
