using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections;
using System.Security.Cryptography;

namespace Task_Manager
{
    public partial class Registation : Form
    {
        public Registation()
        {
            InitializeComponent();
        }
        private void FormClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Point point;
        private void MouseDown_Click(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);     

        }

        private void MouseMove_Click(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - point.X;
                this.Top += e.Y - point.Y;
            }
        }

        private void Registation_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TS"].ConnectionString);
            sqlConnection.Open();
            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Подключение установлено");
            }
        }
        private SqlConnection sqlConnection = null;

        private void button1_Click_1(object sender, EventArgs e)
        {

            string source1 = richTextBox1.Text;
            string source2 = String.Concat(richTextBox2.Text);

            SHA256 sha256Hash = SHA256.Create();

            //From String to byte array
            byte[] sourceBytes1 = Encoding.UTF8.GetBytes(source1);
            byte[] sourceBytes2 = Encoding.UTF8.GetBytes(source2);
            byte[] hashBytes1 = sha256Hash.ComputeHash(sourceBytes1);
            byte[] hashBytes2 = sha256Hash.ComputeHash(sourceBytes2);
            string hash1 = BitConverter.ToString(hashBytes1).Replace("-", String.Empty);
            string hash2 = BitConverter.ToString(hashBytes2).Replace("-", String.Empty);
            int i;
            if (Equals(richTextBox2.Text, richTextBox3.Text))
            {
                if (Equals(comboBox1.Text, "user"))
                {
                    i = 0;
                }
                else if (Equals(comboBox1.Text, "engineer1"))
                {
                    i = 1;
                }
                else
                {
                    i = 2;
                }
                SqlCommand command = new SqlCommand($"INSERT INTO [Users] (login,password,role,mail,number) VALUES (N'{hash1}',N'{hash2}',{i},N'{richTextBox4.Text}',N'{richTextBox5.Text}')", sqlConnection);

                MessageBox.Show(command.ExecuteNonQuery().ToString());

                Registation.ActiveForm.Hide();
                LogIn MyForm2 = new LogIn();
                MyForm2.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("Поля Введите пароль и Повторите пароль должны совпадать");
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
