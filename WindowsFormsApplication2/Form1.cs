using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        MySqlConnection ObjConexion = new MySqlConnection();
        String connectionString; 
        List<Actor> listaActores = new List<Actor>();

        DataSet dataSetActor;
        MySql.Data.MySqlClient.MySqlDataAdapter dataAdapterActor;

        public Form1()
        {
            InitializeComponent();
            IniciarConexion();
        }
       
        private void IniciarConexion()
        {
            try
            {
                connectionString = "Server=127.0.0.1; Database=sakila; Uid=root; Pwd=mandrake;";
                ObjConexion.ConnectionString = connectionString;
                ObjConexion.Open();
                
                MessageBox.Show("La conexion se realizo con exito!");


            }

            catch 
            {
                MessageBox.Show("La conexion fallo intente nuevamente");
            
            }
        }

        private void boton_Consultar(object sender, EventArgs e)
        {
            MySqlCommand consultaSql = ObjConexion.CreateCommand();
            consultaSql.CommandText = "SELECT first_name, last_name FROM Actor";
            MySqlDataReader reader = consultaSql.ExecuteReader();

            while (reader.Read())
            {
                Actor objActor = new Actor();
                objActor.first_name = reader["first_name"].ToString();
                objActor.last_name = reader["last_name"].ToString();
                listaActores.Add(objActor);
                
            }
           
            dataGridView1.DataSource = listaActores;

            reader.Dispose();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ObjConexion.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           



           
        }




    }
}
