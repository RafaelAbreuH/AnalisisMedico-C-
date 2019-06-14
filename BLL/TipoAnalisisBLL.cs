using AnalisisMedico.DAL;
using AnalisisMedico.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AnalisisMedico.BLL
{
    public class TipoAnalisisBLL
    {
        public static bool Guardar(TipoAnalisis tipoanalisis)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.Tipoanalisis.Add(tipoanalisis) != null)
                {
                    contexto.SaveChanges();
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        //Modificar Tipoanalisis
        public static bool Modificar(TipoAnalisis tipoanalisis)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(tipoanalisis).State = EntityState.Modified;
                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        //Eliminar tipoanalisis
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                TipoAnalisis tipoanalisis = contexto.Tipoanalisis.Find(id);

                contexto.Tipoanalisis.Remove(tipoanalisis);
                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;

        }

        //para Buscar los tipoanalisis
        public static TipoAnalisis Buscar(int id)
        {
            Contexto contexto = new Contexto();
            TipoAnalisis tipoanalisis = new TipoAnalisis();
            try
            {
                tipoanalisis = contexto.Tipoanalisis.Find(id);
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return tipoanalisis;
        }

        //Lista
        public static List<TipoAnalisis> Getlist(Expression<Func<TipoAnalisis, bool>> expression)
        {
            List<TipoAnalisis> tipoanalisis = new List<TipoAnalisis>();
            Contexto contexto = new Contexto();
            try
            {
                tipoanalisis = contexto.Tipoanalisis.Where(expression).ToList();
                contexto.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            return tipoanalisis;
        }
    }
}
