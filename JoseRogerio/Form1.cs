using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoseRogerio
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.Text = "ACME FLIGHT MANAGER";

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            BtnSalvar.Enabled = false;
            BtnCancelar.Enabled = false;
            btnAlterar.Visible = false;
            dgDados.DataSource = Database.GetVoos();
            dgDados.Columns["data_voo"].HeaderText = "Data do Vôo";
            dgDados.Columns["captura"].HeaderText = "Captura";
            dgDados.Columns["nivel_dor"].HeaderText = "Nível de dor";
            dgDados.Columns["id_voo"].Visible = false;
            dgDados.Columns["custo"].Visible = false;
            dgDados.Columns["id_voo"].Visible = false;
            dgDados.Columns["distancia"].Visible = false;

        }      
        private void BtnSalvar_Click(object sender, EventArgs e)
        {

            if (!Validacao())
            {
                MessageBox.Show("Informe os dados");
                return;
            }
            try
            {
                Voo voo = new Voo();
                voo.IdVoo = generateID();               
                voo.DataVoo = Convert.ToDateTime(TxtData.Text);
                voo.Custo = Convert.ToInt32(txtCusto.Text);
                voo.Distancia = Convert.ToInt32(txtDistancia.Text);
                voo.NivelDor = Convert.ToInt32(txtDor.Text);
                if (rdSim.Checked == true)
                {
                    voo.Captura = "Sim";
                }
                else
                {
                    voo.Captura = "Não";
                }
                Database.Adicionar(voo);
                MessageBox.Show("informações armazenadas com sucesso");              
                LimparDados();
                BtnSalvar.Enabled = false;
                BtnCancelar.Enabled = false;
                dgDados.DataSource = Database.GetVoos();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            if (!Validacao())
            {
                MessageBox.Show("Informe os dados");
                return;
            }

            try
            {
                Voo voo = new Voo();
                voo.IdVoo = id;
                voo.DataVoo = Convert.ToDateTime(TxtData.Text);
                voo.Custo = Convert.ToInt32(txtCusto.Text);
                voo.Distancia = Convert.ToInt32(txtDistancia.Text);
                voo.NivelDor = Convert.ToInt32(txtDor.Text);
                if (rdSim.Checked == true)
                {
                    voo.Captura = "Sim";
                }
                else
                {
                    voo.Captura = "Não";
                }
                Database.Update(voo);
                MessageBox.Show("Informações alteradas com sucesso");
                LimparDados();
                BtnSalvar.Enabled = false;
                BtnCancelar.Enabled = false;
                dgDados.DataSource = Database.GetVoos();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }
        private void dgDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgDados.Rows[e.RowIndex];
                txtId.Text = row.Cells["id_voo"].Value.ToString();
                TxtData.Text = row.Cells["data_voo"].Value.ToString();
                txtCusto.Text = row.Cells["custo"].Value.ToString();
                txtDistancia.Text = row.Cells["distancia"].Value.ToString();
                if (row.Cells["captura"].Value.Equals("Sim"))
                {
                    rdSim.Checked = true;

                }
                else
                {
                    rdNao.Checked = true;

                }
                txtDor.Text = row.Cells["nivel_dor"].Value.ToString();
                BtnCancelar.Enabled = true;
                BtnSalvar.Visible = false;
                btnAlterar.Visible = true;
            }
         
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);

            Database.Delete(id);
            MessageBox.Show("informações excluidas com sucesso");

            dgDados.DataSource = Database.GetVoos();

        }
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimparDados();
            dgDados.DataSource = Database.GetVoos();
            btnAlterar.Visible = false;
            BtnSalvar.Visible = true;
            BtnSalvar.Enabled = false;
            BtnCancelar.Enabled = false;

        }
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            LimparDados();
            BtnCancelar.Enabled = true;
            BtnSalvar.Enabled = true;
            BtnSalvar.Visible = true;
            btnAlterar.Visible = false;

        }
        public void LimparDados()
        {

            TxtData.Text = "";
            txtCusto.Text = "";
            txtDistancia.Text = "";
            txtDor.Text = "";

        }
        public string generateID()
        {
            return Guid.NewGuid().ToString("N");
        }
        private bool Validacao()
        {
            if (string.IsNullOrEmpty(TxtData.Text) && string.IsNullOrEmpty(txtCusto.Text) && string.IsNullOrEmpty(txtDistancia.Text) &&
                string.IsNullOrEmpty(txtDor.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
    }


