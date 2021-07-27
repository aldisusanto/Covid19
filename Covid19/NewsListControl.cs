using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Covid19
{
    public partial class NewsListControl : UserControl
    {
        browser form;
        public NewsListControl()
        {
            InitializeComponent();
            form = new browser(this);
        }


        #region Properties
        private string _title ;
        private string _content;
        private string _pubdate;
        private string _image;
        private string _media;
        public string _link;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value; labelTitle.Text = value ;
            }
        }
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value; labelContent.Text = value ;
            }
        }
        public string PubDate
        {
            get
            {
                return _pubdate;
            }
            set
            {
                _pubdate = value; labelPub.Text = value ;
            }
        }
        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value; imageMedia.ImageLocation = value ;
            }
        }
        public string Media
        {
            get
            {
                return _media;
            }
            set
            {
                _media = value; labelMedia.Text = value ;
            }
        }
        public string Link
        {
            get
            {
                return _link;
            }
            set
            {
                _link = value;
            }
        }


        #endregion

        private void btnLink_Click(object sender, EventArgs e)
        {
            form.ShowDialog();
        }
    }
}
