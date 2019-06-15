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
    public partial class rAnalisis : Form
    {
        public List<AnalisisDetalle> Detalle { get; set; }
        public rAnalisis()
        {
            InitializeComponent();
            LlenarComboBox();
            this.Detalle = new List<AnalisisDetalle>();
        }
        private void LlenarComboBox()
        {
            var listado = new List<TipoAnalisis>();
            listado = TipoAnalisisBLL.Getlist(p => true);
            AnalisiscomboBox.DataSource = listado;
            AnalisiscomboBox.DisplayMember = "Descripcion";
            AnalisiscomboBox.ValueMember = "TipoAnalisisId";

            var listado2 = new List<Usuarios>();
            listado2 = UsuariosBLL.Getlist(p => true);
            UsuariocomboBox.DataSource = listado2;
            UsuariocomboBox.DisplayMember = "Nombre";
            UsuariocomboBox.ValueMember = "UsuarioId";

        }

        private void CargarGrid()
        {
            DetalledataGridView.DataSource = null;
            DetalledataGridView.DataSource = this.Detalle;
            //Ocultar columnas
           DetalledataGridView.Columns["Id"].Visible = false;
           DetalledataGridView.Columns["AnalisisId"].Visible = false;
        }

        private void Limpiar()
        {
            IdnumericUpDown.Value = 0;
            FechadateTimePicker.Value = DateTime.Now;
            UsuariocomboBox.SelectedIndex = 0;
            AnalisiscomboBox.SelectedIndex = 0;
        
            MyerrorProvider.Clear();

            this.Detalle = new List<AnalisisDetalle>();
            CargarGrid();

        }
        private bool ExisteEnLaBaseDeDatos()
        {
            Analisis Analisi = AnalisisBLL.Buscar(Convert.ToInt32(IdnumericUpDown.Value));
            return (Analisi != null);
        }



        private Analisis LlenaClase()
        {
            Analisis Analisi = new Analisis();
            Analisi.UsuarioId = (int)(IdnumericUpDown.Value);
            Analisi.Fecha = FechadateTimePicker.Value;
            Analisi.analisis = this.Detalle;

            return Analisi;

        }

        private void LlenaCampo(Analisis Analisi)
        {
            IdnumericUpDown.Value = Analisi.UsuarioId;

            AnalisiscomboBox.SelectedValue = Analisi.AnalisisId;
            UsuariocomboBox.SelectedValue = Analisi.UsuarioId;
            this.Detalle = Analisi.analisis;
            CargarGrid();
            
            //Ocultar columnas
           DetalledataGridView.Columns["Id"].Visible = false;
           DetalledataGridView.Columns["AnalisisId"].Visible = false;

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

        private bool ValidarAgregar()
        {
            bool paso = true;
            MyerrorProvider.Clear();

            if (ResultadotextBox.Text == string.Empty)
            {
                MyerrorProvider.SetError(ResultadotextBox, "Debes Introducir un resultado");
                ResultadotextBox.Focus();
                paso = false;
            }
            return paso;
        }
        private bool Validar()
        {
            bool paso = true;
            MyerrorProvider.Clear();


            if (this.Detalle.Count == 0)
            {
                MyerrorProvider.SetError(DetalledataGridView, "Debes agregar un analisis");
                DetalledataGridView.Focus();
                paso = false;
            }

            return paso;
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id;
            Analisis Analisi = new Analisis();
            int.TryParse(IdnumericUpDown.Text, out id);

            Limpiar();

            Analisi = AnalisisBLL.Buscar(id);

            if (Analisi != null)
            {
                LlenaCampo(Analisi);
            }
            else
            {
                MessageBox.Show("no encontrado");
            }
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Analisis analisi = LlenaClase() ;
            bool paso = false;

            if (!Validar())
                return;



            //determinar si es guardar o modificar
            if (IdnumericUpDown.Value == 0)
            {
                paso = AnalisisBLL.Guardar(analisi);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se peude modificar, no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = AnalisisBLL.Modificar(analisi);
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

        private void Agregarbutton_Click(object sender, EventArgs e)
        {
            TipoAnalisis tipo = new TipoAnalisis();
            if (DetalledataGridView.DataSource != null)
                this.Detalle = (List<AnalisisDetalle>)DetalledataGridView.DataSource;

            this.Detalle.Add(
                new AnalisisDetalle(
                    id: 0,
                    analisisId: (int)IdnumericUpDown.Value,
                    tipoAnalisisId: Convert.ToInt32(AnalisiscomboBox.SelectedValue),
                    resultado: ResultadotextBox.Text
                    )
                );
            CargarGrid();
            ResultadotextBox.Focus();
            ResultadotextBox.Clear();
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {

            int id;
            int.TryParse(IdnumericUpDown.Text, out id);

            if (!ValidarEliminar())
                return;

            if (AnalisisBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar();
            }
            else
            {
                MessageBox.Show("No se peude Eliminar, no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void RemoverFilabutton_Click(object sender, EventArgs e)
        {
            if (DetalledataGridView.Rows.Count > 0 && DetalledataGridView.CurrentRow != null)
            {
                Detalle.RemoveAt(DetalledataGridView.CurrentRow.Index);
                CargarGrid();
            }
        }
    }
}
