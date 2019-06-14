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
    public partial class rUsuario : Form
    {
        public rUsuario()
        {
            InitializeComponent();
        }
        private void Limpiar()
        {
            IdnumericUpDown.Value = 0;
            NombretextBox.Text = string.Empty;
            EmailtextBox.Text = string.Empty;
            TelefonomaskedTextBox1.Text = string.Empty;
            SexocomboBox.SelectedIndex = 0;
            FechadateTimePicker.Value = DateTime.Now;

        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Usuarios usuario = UsuariosBLL.Buscar((int)IdnumericUpDown.Value);
            return (usuario != null);
        }

        //Llena campos
        private Usuarios LlenaClase()
        {
            Usuarios usuario = new Usuarios();
            usuario.UsuarioId = Convert.ToInt32(IdnumericUpDown.Value);
            usuario.Nombre = NombretextBox.Text;
            usuario.Email = EmailtextBox.Text;
            usuario.Telefono = TelefonomaskedTextBox1.Text;
            usuario.Sexo = SexocomboBox.Text;
            usuario.FechaNacimiento = FechadateTimePicker.Value;

            return usuario;
        }

        private void LlenaCampo(Usuarios usuario)
        {
            IdnumericUpDown.Value = usuario.UsuarioId;
            NombretextBox.Text = usuario.Nombre;
            TelefonomaskedTextBox1.Text = usuario.Telefono;
            EmailtextBox.Text = usuario.Email;
            SexocomboBox.Text = usuario.Sexo;
            FechadateTimePicker.Value = usuario.FechaNacimiento;
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

            if (NombretextBox.Text == String.Empty)
            {
                MyerrorProvider.SetError(NombretextBox, "El campo Nombre no puede estar vacio");
                NombretextBox.Focus();
                paso = false;
            }

            if (EmailtextBox.Text == String.Empty)
            {
                MyerrorProvider.SetError(EmailtextBox, "El campo Email no puede estar vacio");
                EmailtextBox.Focus();
                paso = false;
            }
            if (TelefonomaskedTextBox1.Text == String.Empty)
            {
                MyerrorProvider.SetError(TelefonomaskedTextBox1, "El campo Telefono no puede estar vacio");
                TelefonomaskedTextBox1.Focus();
                paso = false;
            }
            if (SexocomboBox.SelectedIndex == 0)
            {
                MyerrorProvider.SetError(SexocomboBox, "Debes elegir el Sexo del Usuario");
                SexocomboBox.Focus();
                paso = false;
            }
            return paso;
        }
        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id;
            Usuarios usuario = new Usuarios();
            int.TryParse(IdnumericUpDown.Text, out id);

            Limpiar();

            usuario = UsuariosBLL.Buscar(id);

            if (usuario != null)
            {
                LlenaCampo(usuario);
            }
            else
            {
                MessageBox.Show("Persona no encontrada");
            }
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Usuarios usuario;
            bool paso = false;

            if (!Validar())
                return;

            usuario = LlenaClase();
            Limpiar();

            //determinar si es guardar o modificar
            if (IdnumericUpDown.Value == 0)
                paso = UsuariosBLL.Guardar(usuario);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se peude modificar un Usuario que no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = UsuariosBLL.Modificar(usuario);
            }
            //informar el resultado
            if (paso)
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            MyerrorProvider.Clear();
            int id;
            int.TryParse(IdnumericUpDown.Text, out id);

            if (ValidarEliminar())
                return;

            if (UsuariosBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar();
            }
            else
                MyerrorProvider.SetError(IdnumericUpDown, "No se puede eliminar un usuario que no existe");
        }
    }
}

