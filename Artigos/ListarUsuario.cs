﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Artigos
{
    public partial class ListarUsuario : Form
    {
        

        public ListarUsuario()
        {
            InitializeComponent();
        }

        private void ListarUsuario_Load(object sender, EventArgs e)
        {
            var conn = Login.ConnectOpen;
            //Buscar todos usuários cadastrados
            string sql = "Select * from usuarios ";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            
            if(dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }



        }
    }
}
