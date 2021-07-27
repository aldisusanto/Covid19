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
    public partial class FormChooseCountry : Form
    {
        public DataTable countryData { get; set; }
        public string Country { get; set; }
        public DialogResult result { get; set; }
        public FormChooseCountry()
        {
            InitializeComponent();
        }

        private void FormChooseCountry_Load(object sender, EventArgs e)
        {
            dgvCountry.DataSource = countryData;
        }

        private void tb_search_TextChanged(object sender, EventArgs e)
        {
            countryData.DefaultView.RowFilter = string.Format("name LIKE '{0}*'", tb_search.Text);
        }

        private void dgvCountry_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Country = dgvCountry.Rows[e.RowIndex].Cells[0].Value.ToString();
            result = DialogResult.OK;
            this.Close();
        }

       
    }
}
