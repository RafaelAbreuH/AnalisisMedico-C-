using AnalisisMedico.BLL;
using AnalisisMedico.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalisisMedico.UI.Registro
{
    public partial class rTipoAnalisis : Form
    {
        public rTipoAnalisis()
        {
            InitializeComponent();
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            TipoAnalisis tipoanalisis = TipoAnalisisBLL.Buscar(Convert.ToInt32(IdnumericUpDown.Value));
            return (tipoanalisis != null);
        }

        private TipoAnalisis LlenaClase()
        {
            TipoAnalisis tipoanalisis = new TipoAnalisis();
            tipoanalisis.TipoAnalisisId = (int)(IdnumericUpDown.Value);
            tipoanalisis.Descripcion = DescripciontextBox.Text;

            return tipoanalisis;

        }

        private void LlenaCampo(TipoAnalisis tipoanalisis)
        {
            IdnumericUpDown.Value = tipoanalisis.TipoAnalisisId;
            DescripciontextBox.Text = tipoanalisis.Descripcion;

        }
        private bool ValidarEliminar()
        {
            bool paso = true;
            MyerrorProvider.Clear();

            if (IdnumericUpDown.Value == 0)
            {
                MyerrorProvider.SetError(IdnumericUpDown, "Debes de introducir un ID");
                IdnumericUpDown.Focus();
                paso = false;
            }
            return paso;
        }
        private bool Validar()
        {
            bool paso = true;
            MyerrorProvider.Clear();

            if (DescripciontextBox.Text == string.Empty)
            {
                MyerrorProvider.SetError(DescripciontextBox, " Debes poner una descripcion");
                DescripciontextBox.Focus();
                paso = false;
            }

            return paso;
        }

        private void Limpiar()
        {
            IdnumericUpDown.Value = 0;
            DescripciontextBox.Text = string.Empty;
            MyerrorProvider.Clear();

        }
        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id;
            TipoAnalisis tipoanalisis = new TipoAnalisis();
            int.TryParse(IdnumericUpDown.Text, out id);

            Limpiar();

            tipoanalisis = TipoAnalisisBLL.Buscar(id);

            if (tipoanalisis != null)
            {
                LlenaCampo(tipoanalisis);
            }
            else
            {
                MessageBox.Show("Anaisis no encontrado");
            }
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            TipoAnalisis tipoanalisis;
            bool paso = false;

            if (!Validar())
                return;


            tipoanalisis = LlenaClase();


            //determinar si es guardar o modificar
            if (IdnumericUpDown.Value == 0)
            {
                paso = TipoAnalisisBLL.Guardar(tipoanalisis);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se peude modificar un Analisis que no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = TipoAnalisisBLL.Modificar(tipoanalisis);
            }

            //informar el resultado
            if (paso)
            {
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
            }
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            int id;
            int.TryParse(IdnumericUpDown.Text, out id);

            if (!ValidarEliminar())
                return;

            if (TipoAnalisisBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar();
            }
            else
            {
                MessageBox.Show("No se peude Eliminar un analisis que no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
