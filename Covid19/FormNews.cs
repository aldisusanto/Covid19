using Newtonsoft.Json;
using RestSharp;
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
    public partial class FormNews : Form
    {
        public string httpFeedback;
        DataTable dataNews = new DataTable();
        public FormNews()
        {
            InitializeComponent();
            
        }

        private void FormNews_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            GetData();
        }

        private void GetData()
        {
            var client = new RestClient("https://vaccovid-coronavirus-vaccine-and-treatment-tracker.p.rapidapi.com/api/news/get-coronavirus-news/0");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", "0cef955fc3msh29e683182351c7fp1dace2jsn56665f1cae86");
            request.AddHeader("x-rapidapi-host", "vaccovid-coronavirus-vaccine-and-treatment-tracker.p.rapidapi.com");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content;
                var NewsCovid = JsonConvert.DeserializeObject<Covid>(content);
                
                dataNews.Columns.Add("title");
                dataNews.Columns.Add("media");
                dataNews.Columns.Add("content");
                dataNews.Columns.Add("image");
                dataNews.Columns.Add("publish");
                dataNews.Columns.Add("link");
               
                foreach (var data in NewsCovid.news)
                {
                    dataNews.Rows.Add(data.title, data.reference, data.content, data.urlToImage, data.pubDate, data.link);
                       
                }

                NewsListControl[] newsListControl = new NewsListControl[dataNews.Rows.Count];
               

                for (int i = 0; i < newsListControl.Length; i++)
                {
                    string[] title = new string[] 
                    {
                        dataNews.Rows[0]["title"].ToString(),  
                        dataNews.Rows[1]["title"].ToString(),  
                        dataNews.Rows[2]["title"].ToString(),  
                        dataNews.Rows[3]["title"].ToString(),  
                        dataNews.Rows[4]["title"].ToString(),  
                        dataNews.Rows[5]["title"].ToString(),  
                        dataNews.Rows[6]["title"].ToString(),  
                        dataNews.Rows[7]["title"].ToString(),  
                        dataNews.Rows[8]["title"].ToString(),  
                        dataNews.Rows[9]["title"].ToString(),  
                    };

                    string[] media = new string[]
                    {

                        dataNews.Rows[0]["media"].ToString(),
                        dataNews.Rows[1]["media"].ToString(),
                        dataNews.Rows[2]["media"].ToString(),
                        dataNews.Rows[3]["media"].ToString(),
                        dataNews.Rows[4]["media"].ToString(),
                        dataNews.Rows[5]["media"].ToString(),
                        dataNews.Rows[6]["media"].ToString(),
                        dataNews.Rows[7]["media"].ToString(),
                        dataNews.Rows[8]["media"].ToString(),
                        dataNews.Rows[9]["media"].ToString(),
                    };

                    string[] Content = new string[]
                    {
                        dataNews.Rows[0]["content"].ToString(),
                        dataNews.Rows[1]["content"].ToString(),
                        dataNews.Rows[2]["content"].ToString(),
                        dataNews.Rows[3]["content"].ToString(),
                        dataNews.Rows[4]["content"].ToString(),
                        dataNews.Rows[5]["content"].ToString(),
                        dataNews.Rows[6]["content"].ToString(),
                        dataNews.Rows[7]["content"].ToString(),
                        dataNews.Rows[8]["content"].ToString(),
                        dataNews.Rows[9]["content"].ToString(),
                    };
                    string[] image = new string[]
                    {
                        dataNews.Rows[0]["image"].ToString(),
                        dataNews.Rows[1]["image"].ToString(),
                        dataNews.Rows[2]["image"].ToString(),
                        dataNews.Rows[3]["image"].ToString(),
                        dataNews.Rows[4]["image"].ToString(),
                        dataNews.Rows[5]["image"].ToString(),
                        dataNews.Rows[6]["image"].ToString(),
                        dataNews.Rows[7]["image"].ToString(),
                        dataNews.Rows[8]["image"].ToString(),
                        dataNews.Rows[9]["image"].ToString(),
                    };
                    string[] publish = new string[]
                    {
                        dataNews.Rows[0]["publish"].ToString(),
                        dataNews.Rows[1]["publish"].ToString(),
                        dataNews.Rows[2]["publish"].ToString(),
                        dataNews.Rows[3]["publish"].ToString(),
                        dataNews.Rows[4]["publish"].ToString(),
                        dataNews.Rows[5]["publish"].ToString(),
                        dataNews.Rows[6]["publish"].ToString(),
                        dataNews.Rows[7]["publish"].ToString(),
                        dataNews.Rows[8]["publish"].ToString(),
                        dataNews.Rows[9]["publish"].ToString(),
                    };
                    string[] link = new string[]
                    {
                        dataNews.Rows[0]["link"].ToString(),
                        dataNews.Rows[1]["link"].ToString(),
                        dataNews.Rows[2]["link"].ToString(),
                        dataNews.Rows[3]["link"].ToString(),
                        dataNews.Rows[4]["link"].ToString(),
                        dataNews.Rows[5]["link"].ToString(),
                        dataNews.Rows[6]["link"].ToString(),
                        dataNews.Rows[7]["link"].ToString(),
                        dataNews.Rows[8]["link"].ToString(),
                        dataNews.Rows[9]["link"].ToString(),
                    };
                    newsListControl[i] = new NewsListControl();
                    newsListControl[i].Title = title[i];
                    newsListControl[i].Media = media[i];
                    newsListControl[i].Content = Content[i];
                    newsListControl[i].Image = image[i];
                    newsListControl[i].PubDate = publish[i];
                    newsListControl[i].Link = link[i];
                    


                    if (flowLayoutNews.Controls.Count < 0)
                    {
                        flowLayoutNews.Controls.Clear();
                    }
                    else
                    {
                        flowLayoutNews.Controls.Add(newsListControl[i]);
                    }
                }
               
            }
            else
            {

                MessageBox.Show("No data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                backgroundWorker1.CancelAsync();
            }
        }
      

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            if (backgroundWorker1.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
        }


        private void backgroundWorker1_RunWorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}
