using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_Manager
{
    public partial class SettingsUser : Form
    {
        public SettingsUser()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.
                Show("Здесь будет возможность поменять аватарку, ник, ..., ..., ..., ... :)",
                "Справка", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);//выбрать язык, угрожать знанием апи, поменять тему, мемы про карчивского :)
            if (dl == DialogResult.Cancel)
            {
                MessageBox.Show("Сам ты Cancel", ":)");// Возможно, стоит удалить)
            }
            else
            {
                MessageBox.Show("Это реально готовые идеи, в комментариях они прописаны ; )", "С-Серьезность");
            }
        }
    }
}