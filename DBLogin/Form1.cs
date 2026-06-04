using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace DBLogin
{
    public partial class Form1 : Form
    {
        string db_path = "User.db";
        string connection_str;
        SQLiteConnection conn;
        string sql;
        SQLiteCommand cmd;

        public Form1()
        {
            InitializeComponent();
            connection_str = $"Data Source={db_path};" + "Version=3;";
            if (!File.Exists(db_path))
            {
                SQLiteConnection.CreateFile(db_path);
                using (conn = new SQLiteConnection(connection_str))
                {
                    conn.Open();

                    sql = @"CREATE TABLE IF NOT EXISTS Users(
                            Id Integer Primary Key Autoincrement,
                            Name TEXT NOT NULL,
                            Password TEXT NOT NULL);
                            ";
                    cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (conn = new SQLiteConnection(connection_str))
            {
                conn.Open();
                sql = @"Insert into Users(Name, Password)
                    Values(@Name, @Password);
                    ";
                cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Password", textBox2.Text);
                cmd.ExecuteNonQuery();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            using (conn = new SQLiteConnection(connection_str))
            {
                sql = @"Select Id, Name, Password From Users Order by ID";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, conn))
                {
                    adapter.Fill(dt);
                }
            }
            dataGridView1.DataSource = dt;
        }
    }
}
