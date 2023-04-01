using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Task_Manager
{

        enum Priority
        {
            Critical = 0,
            High,
            Medium,
            Low
        }
        enum Affair
        {
            General = 0,
            Personal
        }
        public partial class MainUser : Form
    {
        //Это пиздец, мужик. Если ты это увидишь, то извини, лучше нихуя тут не трогает. Я это 2 или 3 раза писал с нуля. я в рот ебал
        //эти сроки, но мы конечно сами виноваты. Время 3:38. Дата 27.03. второй раз не сплю и пишу этот ебучий проект...
        //Я даже не прогер!! НО мне нравится. Жаль, я не один в комнате.  Я бы матерился щас на всю комнату.
        //Когда я удалил репозиторий, я чуть не заплакал :DDDD
        public MainUser()
        {
            InitializeComponent();
            tableLayoutPanel1.RowStyles[0].Height = 25;
            tableLayoutPanel1.ColumnStyles[0].Width = 25;
        }
        Point UserPoint = new Point(5, 5);
        string Affair { get; set; }
        private void AddTask(string description, string teg, Priority priority, string affair)
        {
            Panel panel = new Panel() { BackColor = Color.Purple, Location = UserPoint, Size = new Size(panel8.Size.Width - 10, 100),Anchor = (AnchorStyles)(AnchorStyles.Left|AnchorStyles.Right|AnchorStyles.Top) };
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
            Label lblFullDescription = new Label() { Text = "Полностью", AutoSize = true, Location = new Point(5, 50), BackColor = Color.FromArgb(22, 125, 199), Anchor = (AnchorStyles)(AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top) };
            panel.MouseClick += (o, e) =>
            {
                if (panel.Location.X >= panel9.Location.X)
                {
                    panel.Left = panel9.Location.X;
                }
            };
            void Show(object o, EventArgs e) => MessageBox.Show(description, "Полная задача");
            lblFullDescription.Click += (o, e) => Show(o, e); //Анонимные делегаты, хз, как, но работает :))))
            panel8.Controls.Add(panel);
            panel.Controls.Add(lblTask);
            panel.Controls.Add(lblFullDescription);
            panel.Controls.Add(lblTEG);
            panel.Controls.Add(lblAffair);
            UserPoint.Y += 110;

        }
        // ДА РОФЛЮ :))))))
        Point point;
        private void MouseDown_Click(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);

        }

        private void MouseMove_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - point.X;
                this.Top += e.Y - point.Y;
            }
        }
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SettingsUser settingsUser = new SettingsUser();
            settingsUser.Show();
        }

        private void pictureBox1_MouseMove(object sender, EventArgs e)
        {

            MessageBox.Show("Ты милашка", "^_^");

        }
        private SqlConnection sqlConnection = null;
        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TS"].ConnectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand($"INSERT INTO [Problems] " +
                $"(date_of_assignment,date_of_taking,description_half,description_full,type,gryppa,solution,date_of_solution,status) " +
                $"VALUES (N'{DateTime.Now}',N'{null}',N'{richTextBox2.Text}',N'{richTextBox1.Text}',N'{null}',N'{null}',N'{null}',N'{null}',N'{"В ожидании"}')",sqlConnection);
            MessageBox.Show(command.ExecuteNonQuery().ToString());
            AddTask(richTextBox1.Text, richTextBox2.Text, Priority.Critical, "Общие");
        }

        private void panel8_DragOver(object sender, DragEventArgs e)
        {
            if (this.Location.X >= panel9.Location.X)
            {
                this.Left = panel9.Location.X;
            }
        }

        private void panel9_DragEnter(object sender, DragEventArgs e)
        {
            if (this.Location.X <= panel8.Location.X)
            {
                this.Left = panel8.Location.X;
            }
        }
    }
}