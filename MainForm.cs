using AnalisisMedico.UI.Registro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalisisMedico
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void UsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rUsuario ver = new rUsuario();
            ver.MdiParent = this;
            ver.Show();
        }

        private void TiposDeAnalisisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rTipoAnalisis ver = new rTipoAnalisis();
            ver.MdiParent = this;
            ver.Show();
        }

        private void AnalisisMedicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rAnalisis ver = new rAnalisis();
            ver.MdiParent = this;
            ver.Show();
        }
    }
}
