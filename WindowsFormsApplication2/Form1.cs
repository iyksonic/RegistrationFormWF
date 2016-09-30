using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Net;
using System.Text.RegularExpressions;



namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        MySqlConnection myConnection;
        public Form1()
        {
            InitializeComponent();
            string connStr = "server=localhost;uid=root;database=school";
            myConnection = new MySqlConnection(connStr);
        }

        private void button1Register_Click(object sender, EventArgs e)
        {
            try
            {
                string txtName = textBoxName.Text;
                string txtEmail = textBoxEmail.Text;
                string txtPhone = textBoxPhone.Text;
                if(txtName == " " || txtEmail == "" || txtPhone == "")
                {
                    MessageBox.Show("check your field well");
                }
                
               else if (!Regex.IsMatch(txtPhone, @"[0-9]"))
                {
                 MessageBox.Show("check phone no");
                }
                else if (textBoxPhone.MaxLength > 11)
                {
                    MessageBox.Show("check phone no length");
                }

                else {

                string command = string.Format("INSERT INTO users (name, email, phone_number) " +
                
                    
                    "VALUES ('{0}', '{1}', '{2}')", textBoxName.Text, textBoxEmail.Text, textBoxPhone.Text);
                MySqlCommand cmd = new MySqlCommand(command, myConnection);
                myConnection.Open();
                int result = cmd.ExecuteNonQuery();
                pictureBox1.Image = Properties.Resources.success;
                MessageBox.Show("User insertion successful.", "Success");
                StatusText.Text = "User insertion successful.";
                textBoxEmail.Clear();
                textBoxName.Clear();
                textBoxPhone.Clear();
                }
            }

            catch (Exception ex)
            {
                pictureBox1.Image = Properties.Resources.Stop;
                StatusText.Text = ex.Message;
                MessageBox.Show("An error has occurred! Msg: " + ex.Message, "Error");
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
    }
