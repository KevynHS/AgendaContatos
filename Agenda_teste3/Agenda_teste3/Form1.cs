using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Agenda_teste3
{
    public partial class Form1 : Form
    {
        SqlConnection conexao = new SqlConnection(@"Data Source=localhost;Initial Catalog=Contatos;User ID=sa;Password=kevyn091198");
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter cons;
        
        public Form1()
        {
            InitializeComponent();
            Dados();
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && txtEndereco.Text != "" && txtEmail.Text != "" && txtTelefone.Text != "")
            {
                try
                {
                    conexao.Open();
                    cmd = new SqlCommand("INSERT INTO Contacts (name, email, phone, address) VALUES(@nome, @email, @telefone, @endereco)",conexao);
                    cmd.Parameters.AddWithValue("@nome",txtNome.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                    cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deu boa! Registrou ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu o seguinte erro:{ex.Message}");
                }
                finally
                {
                    conexao.Close();
                    Limpar();
                }
            }
            else
            {
                MessageBox.Show("Dados faltantes!! Confira os campos em branco !!");
            }
        }
        private void Dados()
        {
            try
            {
                conexao.Open();
                DataTable dt = new DataTable();
                cons = new SqlDataAdapter("SELECT * FROM Contacts", conexao);
                cons.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro de compilação:{ex.Message}");
            }
            conexao.Close();
        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtEmail.Text = "";
            txtTelefone.Text = "";
        }
        private void Limpar()
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtEmail.Text = "";
            txtTelefone.Text = "";
        }
    }
}
