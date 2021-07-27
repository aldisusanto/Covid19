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
    public partial class browser : Form
    {
        private readonly NewsListControl _parent;
        public browser(NewsListControl parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void browser_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(_parent._link);
        }
    }
}
