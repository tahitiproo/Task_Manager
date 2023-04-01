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
using System.Security.Cryptography;

namespace Task_Manager
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }
        Point point;
        

        private void FormClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - point.X;
                this.Top += e.Y - point.Y;
            }
        }
        private SqlConnection sqlConnection = null;
        private void LogIn_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TS"].ConnectionString);
            sqlConnection.Open();
            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Подключение установлено");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            LogIn.ActiveForm.Hide();
            Registation MyForm2 = new Registation();
            MyForm2.ShowDialog();
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string source1 = textBox1.Text;
            string source2 = String.Concat(textBox2.Text);

            SHA256 sha256Hash = SHA256.Create();

            //From String to byte array
            byte[] sourceBytes1 = Encoding.UTF8.GetBytes(source1);
            byte[] sourceBytes2 = Encoding.UTF8.GetBytes(source2);
            byte[] hashBytes1 = sha256Hash.ComputeHash(sourceBytes1);
            byte[] hashBytes2 = sha256Hash.ComputeHash(sourceBytes2);
            string hash1 = BitConverter.ToString(hashBytes1).Replace("-", String.Empty);
            string hash2 = BitConverter.ToString(hashBytes2).Replace("-", String.Empty);
            string[] xran = new string[3];
            try
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand($"SELECT login,password,role FROM Users WHERE login=N'{hash1}' AND password=N'{hash2}'", sqlConnection);
                    MessageBox.Show(command.ExecuteNonQuery().ToString());
                    using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        //цикл по всем столбцам полученной в результате запроса таблицы
                        for (int i = 0; i < dr.FieldCount; i++)
                        //Вывод названия столбцов
                        {

                            xran[i] = "" + dr.GetName(i).ToString().Trim();
                        }

                        //читаем данные из таблицы
                        while (dr.Read())
                        {
                            //Вывод значений
                            xran[0] = dr.GetValue(0).ToString().Trim();
                            xran[1] = dr.GetValue(1).ToString().Trim();
                            xran[2] = dr.GetValue(2).ToString().Trim();
                            if ((int.Parse(xran[2]) == 0)&&(hash1 == xran[0] && hash2 == xran[1]))
                            {
                                LogIn.ActiveForm.Hide();
                                MainUser MyForm2 = new MainUser();
                                MyForm2.ShowDialog();
                                Close();
                            }
                            else if ((int.Parse(xran[2]) == 1) && (hash1 == xran[0] && hash2 == xran[1]))
                            {
                                LogIn.ActiveForm.Hide();
                                MainIngener1 MyForm2 = new MainIngener1();
                                MyForm2.ShowDialog();
                                Close();
                            }
                            else if ((int.Parse(xran[2]) == 2) && (hash1 == xran[0] && hash2 == xran[1]))
                            {
                                LogIn.ActiveForm.Hide();
                                MainIngener2 MyForm2 = new MainIngener2();
                                MyForm2.ShowDialog();
                                Close();
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
        private void textBox1_MouseClick(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
        private void textBox2_MouseEnter(object sender, EventArgs e)
        {
            textBox2.Clear();
        }
        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Clear();
        }
        public void RemoveText(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter text here...")
            {
                textBox1.Text = "";
            }
        }

        public void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                textBox1.Text = "Enter text here...";
        }
    }
}
