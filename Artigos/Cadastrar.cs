using System;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace Artigos
{
    public partial class Cadastrar : Form
    {
        public bool logado = false;
        private Conexao conn;
        private SqlConnection ConnectOpen;

        public Cadastrar()
        {
            InitializeComponent();
            conn = new Conexao();
            ConnectOpen = conn.ConnectToDatabase();
        }

   

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //incluir o using System.Text
            StringBuilder sql = new StringBuilder();
            sql.Append("Insert into usuarios(Usuario, senha, perfil) ");
            sql.Append("Values (@usuario, @senha, @perfil)");
            SqlCommand command = null;

            int perfilSelecionado = 0;
            switch (cmbPerfil.Text)
            {
                case "Autores":
                    perfilSelecionado = 1;
                    break;
                case "Revisores":
                    perfilSelecionado = 2;
                    break;
                case "Gerente":
                    perfilSelecionado = 3;
                    break;
                default:
                    perfilSelecionado = 1;
                    break;
            }

            try
            {
                command = new SqlCommand(sql.ToString(), ConnectOpen);
                command.Parameters.Add(new SqlParameter("@usuario", txtUsuario.Text));
                command.Parameters.Add(new SqlParameter("@senha", txtSenha.Text));
                command.Parameters.Add(new SqlParameter("@perfil", perfilSelecionado));
                command.ExecuteNonQuery();

                MessageBox.Show("Cadastrado com sucesso!");
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar" + ex);
                throw;
            }
        }
        private void Cadastrar_Load(object sender, EventArgs e)
        {
            if(Login.perfilUsuario == 3)
            {
                lblPerfil.Visible = true;
                cmbPerfil.Visible = true;
                btnListar.Visible = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            var listarUsu = new ListarUsuario();
            listarUsu.ShowDialog();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("Delete from usuarios(Usuario, senha, perfil) ");
            sql.Append("Values (@usuario, @senha, @perfil)");
            SqlCommand command = null;

            try
            {
                command = new SqlCommand(sql.ToString(), ConnectOpen);
                command.Parameters.Remove(new SqlParameter("@usuario", txtUsuario.Text));
                command.ExecuteNonQuery();
                MessageBox.Show("Excluído com sucesso!");
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir!" + ex);
                throw;
            }
        }
    }
}
