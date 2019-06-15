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
    public class AnalisisBLL
    {
        public static bool Guardar(Analisis analisi)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.Analisi.Add(analisi) != null)
                    paso = contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose(); // cerrar la conexion
            }

            return paso;
        }

        //Modificar Analisi
        public static bool Modificar(Analisis analisi)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var Anterior = contexto.Analisi.Find(analisi.AnalisisId);
                foreach (var item in Anterior.analisis)
                {
                    if (!analisi.analisis.Exists(d => d.TipoAnalisisId == item.TipoAnalisisId))
                        contexto.Entry(item).State = EntityState.Deleted;
                }
                contexto.Entry(analisi).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        //Eliminar analisi
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var eliminar = contexto.Analisi.Find(id);
                contexto.Entry(eliminar).State = EntityState.Deleted;

                paso = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;

        }

        //para Buscar los analisi
        public static Analisis Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Analisis analisi = new Analisis();
            try
            {
                analisi = contexto.Analisi.Find(id);
                analisi.analisis.Count();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return analisi;
        }

        //Lista
        public static List<Analisis> Getlist(Expression<Func<Analisis, bool>> expression)
        {
            List<Analisis> lista = new List<Analisis>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Analisi.Where(expression).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
    }
}
