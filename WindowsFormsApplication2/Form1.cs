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

        int CantidadDeFilas =0, indice = 0;
        
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

                string selectAll = "SELECT * From Actor";
                dataAdapterActor = new MySqlDataAdapter(selectAll, ObjConexion);
                
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
            dataGridView1.DataSource = null;
            MySqlCommand consultaSql = ObjConexion.CreateCommand();
            consultaSql.CommandText = "SELECT actor_id, first_name, last_name FROM Actor";
            MySqlDataReader reader = consultaSql.ExecuteReader();

            while (reader.Read())
            {
                Actor objActor = new Actor();
                objActor.first_name = reader["first_name"].ToString();
                objActor.last_name = reader["last_name"].ToString();
                objActor.Actor_id = reader["actor_id"].ToString();
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

            dataSetActor = new DataSet();
            
            
            dataAdapterActor.Fill(dataSetActor, "Actor");

            navegar_registros();

            CantidadDeFilas = dataSetActor.Tables["Actor"].Rows.Count;

           
        }

        private void navegar_registros()
        {
            DataRow dFila;

            dFila = dataSetActor.Tables["Actor"].Rows[indice];

            textBox1.Text = dFila.ItemArray.GetValue(0).ToString();
            textBox2.Text = dFila.ItemArray.GetValue(1).ToString();
            textBox3.Text = dFila.ItemArray.GetValue(2).ToString();
            textBox4.Text = dFila.ItemArray.GetValue(3).ToString();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //me fijo de no pasarme del maximo de filas, q como arranca en 0 es 
            // cantidad de filas - 1 
            if (indice != CantidadDeFilas - 1)
            {
                indice++;
                navegar_registros();
            }
            else
            {
                MessageBox.Show("Ultimo registro");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (indice > 0)
            {
                indice--;
                navegar_registros();
            }
            else
            {
                MessageBox.Show("Primer registro");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            indice = 0;
            navegar_registros();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            indice = CantidadDeFilas - 1;
            navegar_registros();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySqlCommandBuilder command_Builder;
            command_Builder = new MySqlCommandBuilder(dataAdapterActor);

            DataRow dFila = dataSetActor.Tables["Actor"].NewRow();

            dFila[0] = textBox1.Text;
            dFila[1] = textBox2.Text;
            dFila[2] = textBox3.Text;
            dFila[3] = textBox4.Text;

            dataSetActor.Tables["Actor"].Rows.Add(dFila);

            CantidadDeFilas++;
            indice = CantidadDeFilas - 1;

            dataAdapterActor.Update(dataSetActor, "Actor");

            MessageBox.Show("Registro añadido con exito");

            button1.PerformClick();
           



        }

        private void boton_modificar_Click(object sender, EventArgs e)
        {
            MySqlCommandBuilder command_Builder;
            command_Builder = new MySqlCommandBuilder(dataAdapterActor);

            DataRow dFila2 = dataSetActor.Tables["Actor"].Rows[indice];

            dFila2[0] = textBox1.Text;
            dFila2[1] = textBox2.Text;
            dFila2[2] = textBox3.Text;
            dFila2[3] = textBox4.Text;

            dataAdapterActor.Update(dataSetActor, "Actor");
            
            button1.PerformClick();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            MySqlCommandBuilder command_Builder;
            command_Builder = new MySqlCommandBuilder(dataAdapterActor);

            dataSetActor.Tables["Actor"].Rows[indice].Delete();
            
            CantidadDeFilas--;
            indice = 0;

            dataAdapterActor.Update(dataSetActor, "Actor");

            navegar_registros();
            
            MessageBox.Show("Registro Eliminado");

            button1.PerformClick();
        }



    }
}
