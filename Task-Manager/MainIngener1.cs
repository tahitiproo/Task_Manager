﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;

namespace Task_Manager
{
    
    public partial class MainIngener1 : Form
    {
        //Это пиздец, мужик. Если ты это увидишь, то извини, лучше нихуя тут не трогает. Я это 2 или 3 раза писал с нуля. я в рот ебал
        //эти сроки, но мы конечно сами виноваты. Время 3:38. Дата 27.03. второй раз не сплю и пишу этот ебучий проект...
        //Я даже не прогер!! НО мне нравится. Жаль, я не один в комнате.  Я бы матерился щас на всю комнату.
        //Когда я удалил репозиторий, я чуть не заплакал :DDDD

        // обращение к БД готово

        //время 1:32, дата 30.03 я просто в ахуе с работы, но вроде норм, жду макеты никит, я ебал это в соло делать
        public MainIngener1()
        {
            InitializeComponent();
            tableLayoutPanel1.RowStyles[0].Height = 25;
            tableLayoutPanel1.ColumnStyles[0].Width = 25;
        }
        Point UserPoint = new Point(5, 5);
        string Affair { get; set; }
        private void AddTask(string description, string teg, Priority priority, string affair, Panel _Panel_)
        {
            Panel panel = new Panel() { BackColor = Color.Purple, Location = UserPoint, Size = new Size(panel8.Size.Width - 10, 100), Anchor = (AnchorStyles)(AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top) };
            Label lblTask = new Label() { Text = description, Size = new Size(75, 40), Location = new Point(5, 5), BackColor = Color.FromArgb(22, 199, 199), Anchor = (AnchorStyles)(AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top) };
            Label lblTEG = new Label() { Text = teg, BackColor = TakeColorTeg(priority), Location = new Point(panel.Width - 100, panel.Height - 25), Size = new Size(100, 20), Anchor = (AnchorStyles)(AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top) };
            Label lblAffair = new Label()
            {
                Text = affair,
                Location = new Point(panel.Width - 70, 5),
                BackColor = Color.Gray,
                Size = new Size(60, 15),
                Anchor = (AnchorStyles)(AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top)
            };
            Label lblFullDescription = new Label() { Text = "Полностью", AutoSize = true, Location = new Point(5, 50), BackColor = Color.FromArgb(22, 125, 199) };
            lblTEG.MouseClick += (o, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        SqlCommand command = new SqlCommand($"UPDATE Problems set date_of_taking = N'{DateTime.Now}'," +
                            "type = 'problem',status = N'Принято инженером 1 линии'", sqlConnection);
                        MessageBox.Show(command.ExecuteNonQuery().ToString());
                    }
                    panel.BackColor = Color.Blue;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        SqlCommand command = new SqlCommand($"UPDATE Problems set date_of_taking = N'{DateTime.Now}'," +
                            "type = 'problem',status = N'Принято инженером 1 линии'", sqlConnection);
                        MessageBox.Show(command.ExecuteNonQuery().ToString());
                    }
                    panel.BackColor = Color.Pink;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        SqlCommand command = new SqlCommand($"UPDATE Problems set date_of_taking = N'{DateTime.Now}'," +
                            "type = 'problem',status = N'Принято инженером 1 линии'", sqlConnection);
                        MessageBox.Show(command.ExecuteNonQuery().ToString());
                    }
                    panel.BackColor = Color.Purple;
                }
            };
            panel.MouseClick += (o, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    
                    
                    if (panel8.Controls.Contains(panel)) //добавляю на линию1
                    {
                        
                        panel9.Controls.Add(panel);
                        panel8.Controls.Remove(panel);
                        if (sqlConnection.State == ConnectionState.Open)
                        {
                            SqlCommand command = new SqlCommand($"UPDATE Problems set date_of_taking = N'{DateTime.Now}'," +
                                "type = 'problem',status = N'Принято инженером 1 линии'", sqlConnection);
                            MessageBox.Show(command.ExecuteNonQuery().ToString());
                        }
                    }
                    else if (panel9.Controls.Contains(panel))
                    {
                        splitContainer1.Panel1.Controls.Add(panel);//добавляю на линию2 проблемс
                        panel9.Controls.Remove(panel);
                        if (sqlConnection.State == ConnectionState.Open)
                        {
                            SqlCommand command = new SqlCommand($"UPDATE Problems set date_of_taking = N'{DateTime.Now}'," +
                                "gryppa = 0,status = N'Принято инженером 2 линии'", sqlConnection);
                            MessageBox.Show(command.ExecuteNonQuery().ToString());
                        }
                        
                    }

                }
                else if (e.Button == MouseButtons.Right)//добавляю на линию2 инциденты
                {
                    if (panel9.Controls.Contains(panel))
                    {
                        splitContainer1.Panel2.Controls.Add(panel);
                        panel9.Controls.Remove(panel);
                        if (sqlConnection.State == ConnectionState.Open)
                        {
                            
                            SqlCommand command = new SqlCommand($"UPDATE Problems set date_of_taking = N'{DateTime.Now}'," +
                                "gryppa = 1,status = N'Принято инженером 2 линии'", sqlConnection);
                            MessageBox.Show(command.ExecuteNonQuery().ToString());
                        }
                    }
                }
                else if (e.Button == MouseButtons.Middle)// удаляю с линии 2 на линию 1
                {
                    if (splitContainer1.Panel1.Controls.Contains(panel))
                    {
                        panel9.Controls.Add(panel);
                        splitContainer1.Panel1.Controls.Remove(panel);

                    }
                    else if (splitContainer1.Panel2.Controls.Contains(panel))
                    {
                        panel9.Controls.Add(panel);
                        splitContainer1.Panel2.Controls.Remove(panel);

                    }
                };
            };
                void Show(object o, EventArgs e) => MessageBox.Show(description, "Полная задача");
                lblFullDescription.Click += (o, e) => Show(o, e); //Анонимные делегаты, хз, как, но работает :))))
                _Panel_.Controls.Add(panel);
                panel.Controls.Add(lblTask);
                panel.Controls.Add(lblFullDescription);
                panel.Controls.Add(lblTEG);
                panel.Controls.Add(lblAffair);
                UserPoint.Y += 110;
        }
        private void MainIngener1_Load(object sender, EventArgs e)
        {
            string[] xran = new string[2];
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TS"].ConnectionString);
            sqlConnection.Open();
            if (sqlConnection.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand($"SELECT description_full,description_half FROM Problems", sqlConnection);
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
                    }
                }
                sqlConnection.Open();
                AddTask(xran[0], xran[1], Priority.Critical, "Общие", panel8);
            }

        }
        private SqlConnection sqlConnection = null;
        private Color TakeColorTeg(Priority priority)
        {
            if (priority == Priority.Critical) { return Color.Red; }
            if (priority == Priority.High) { return Color.Green; }
            if (priority == Priority.Medium) { return Color.Blue; }
            return Color.Black;
        }
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {

            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabControl1.TabPages[e.Index];
            //_tabPage.BackColor = Color.Black; так можно задать все tabpage одного цвета, но в конструкторе этого не видно
            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.White);
                g.FillRectangle(Brushes.Black, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Arial", (float)10.0, FontStyle.Regular, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));

        }
        private void pictureBox1_MouseMove(object sender, EventArgs e)
        {

            MessageBox.Show("Ты милашка", "^-^");

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            SettingsUser settingsUser = new SettingsUser();
            settingsUser.Show();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;
                Excel.Range oRng;

                try
                {
                    //Start Excel and get Application object.
                    oXL = new Excel.Application();
                    oXL.Visible = true;

                    //Get a new workbook.
                    oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                    oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                    //Add table headers going cell by cell.
                    oSheet.Cells[1, 1] = "Дата поступления";
                    oSheet.Cells[1, 2] = "Описание";
                    oSheet.Cells[1, 3] = "Решение";
                    oSheet.Cells[1, 4] = "Дата принятия";
                    oSheet.Cells[1, 5] = "Дата решения";

                    //Format A1:D1 as bold, vertical alignment = center.
                    oSheet.get_Range("A1", "E1").Font.Bold = true;
                    oSheet.get_Range("A1", "E1").VerticalAlignment =
                    Excel.XlVAlign.xlVAlignCenter;

                    // Create an array to multiple values at once.
                    string[,] saNames = new string[100, 5];
                    // первая цифра - строка, вторая - столбец
                    SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TS"].ConnectionString);
                    sqlConnection.Open();
                    int str = 0;
                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        SqlCommand command = new SqlCommand($"SELECT date_of_assignment,description_full,solution,date_of_taking,date_of_solution FROM Problems ORDER BY date_of_assignment", sqlConnection);
                        using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            //цикл по всем столбцам полученной в результате запроса таблицы
                            //читаем данные из таблицы
                            while (dr.Read())
                            {
                                //Вывод значений
                                saNames[str, 0] = dr.GetValue(0).ToString().Trim();
                                saNames[str, 1] = dr.GetValue(1).ToString().Trim();
                                saNames[str, 2] = dr.GetValue(2).ToString().Trim();
                                saNames[str, 3] = dr.GetValue(3).ToString().Trim();
                                saNames[str, 4] = dr.GetValue(4).ToString().Trim();
                                str++;
                            }
                        }

                    }
                    //Fill A2:B6 with an array of values (First and Last Names).
                    oSheet.get_Range("A2", "E102").Value2 = saNames;

                    //Fill C2:C6 with a relative formula (=A2 & " " & B2).
                    //////oRng = oSheet.get_Range("C2", "C6");
                    //////oRng.Formula = "=A2 & \" \" & B2";

                    //Fill D2:D6 with a formula(=RAND()*100000) and apply format.
                    //oRng = oSheet.get_Range("D2", "D6");
                    //oRng.Formula = "=RAND()*100000";
                    //oRng.NumberFormat = "$0.00";

                    //AutoFit columns A:D.
                    oRng = oSheet.get_Range("A1", "E1");
                    oRng.EntireColumn.AutoFit();

                    //Manipulate a variable number of columns for Quarterly Sales Data.
                    //DisplayQuarterlySales(oSheet); эта херь рисует графики в экселе, юзлесс тема.
                    //нам не нужно, поэтому пропускаю

                    //Make sure Excel is visible and give the user control
                    //of Microsoft Excel's lifetime.
                    oXL.Visible = true;
                    oXL.UserControl = true;
                }
                catch (Exception theException)
                {
                    String errorMessage;
                    errorMessage = "Error: ";
                    errorMessage = String.Concat(errorMessage, theException.Message);
                    errorMessage = String.Concat(errorMessage, " Line: ");
                    errorMessage = String.Concat(errorMessage, theException.Source);

                    MessageBox.Show(errorMessage, "Error");
                }
            }
        }
    }
}