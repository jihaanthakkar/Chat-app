using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string cs = "Host=192.170.82.250;Username=postgres;Password=1993;Database=jihaan_chat_app;";


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //code for connection

          
           // MessageBox.Show(txt);
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            //using Npgsql;

          //  var cs = "Host=localhost;Username=postgres;Password=s$cret;Database=testdb";

            var con = new NpgsqlConnection(cs);
            con.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "INSERT INTO chats VALUES('"+textBox2.Text+"','"+ comboBox1.Text + "','"+textBox1.Text+"')";
            cmd.ExecuteNonQuery();
            con.Close();
            load_chats();
        }

        void load_chats()
        {
            listBox1.Items.Clear();
            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM chats";
            var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader rdr = cmd.ExecuteReader();

            string txt = "";
            while (rdr.Read())
            {

                listBox1.Items.Add(rdr.GetString(0) + " - " + rdr.GetString(1) + " :  " + rdr.GetString(2));

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load_chats();
        }
    }
}
