using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Covid19
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            activeBorder.Location = new Point(3, btnOverview.Location.Y);
            Container(new FormOverview());
        }

        private void btnSymptoms_Click(object sender, EventArgs e)
        {
            activeBorder.Location = new Point(3, btnSymptoms.Location.Y);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            activeBorder.Location = new Point(3, btnTest.Location.Y);
        }

        private void btnJournal_Click(object sender, EventArgs e)
        {
            activeBorder.Location = new Point(3, btnJournal.Location.Y);
        }
        
        private void Container(object _form)
        {
            if (panelContainer.Controls.Count > 0) panelContainer.Controls.Clear();
            Form form = _form as Form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(form);
            panelContainer.Tag = form;
            form.Show();
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
            Container(new FormOverview());
        }

        private void btnNews_Click(object sender, EventArgs e)
        {
            activeBorder.Location = new Point(3, btnNews.Location.Y);
            Container(new FormNews());
        }
    }
}
