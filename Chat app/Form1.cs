﻿using Npgsql;
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
           // listBox1.Items.Clear();
            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM chats";
            var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader rdr = cmd.ExecuteReader();

            string txt = "";
            while (rdr.Read())
            {
                if (!listBox1.Items.Contains(rdr.GetString(0) + " - " + rdr.GetString(1) + " :  " + rdr.GetString(2)))
                {
                    listBox1.Items.Add(rdr.GetString(0) + " - " + rdr.GetString(1) + " :  " + rdr.GetString(2));
                }
                

            }

            con.Close();
            listBox1.SelectedIndex = listBox1.Items.Count - 1;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.ContainerControl |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.SupportsTransparentBackColor
                          , true);

            timer1.Start();
           // load_chats();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            load_chats();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
                textBox1.Clear();
            }
        }
    }
}
