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
using static Bunifu.Dataviz.WinForms.BunifuDatavizAdvanced;

namespace Covid19
{
    public partial class FormOverview : Form
    {
        public FormOverview()
        {
            InitializeComponent();
        }
        DataTable dataContries = new DataTable();
        DataTable dataHistory = new DataTable();
        DataTable dataWorld = new DataTable();
        string httpFeedback;
        string countryName = null;

        DataPoint newCaseDataPoint = new DataPoint(_type.Bunifu_column);
        DataPoint criticalCaseDataPoint = new DataPoint(_type.Bunifu_column);
        DataPoint deathCaseDataPoint = new DataPoint(_type.Bunifu_column);
        DataPoint recoveredCaseDataPoint = new DataPoint(_type.Bunifu_column);


        private void btnChoose_Click(object sender, EventArgs e)
        {
            FormChooseCountry form = new FormChooseCountry();
            form.countryData = dataContries;
            form.Location = new Point(btnChoose.Location.X + 5, btnChoose.Location.Y + 70);
            form.ShowDialog();

            if(form.result == DialogResult.OK)
            {
                labelCountry.Text = form.Country;
                labelCountry2.Text = form.Country;
                countryName = form.Country;
                toatsControl1.Visible = true;

                if (dataHistory.Rows.Count > 0)
                {
                    dataHistory.Columns.Clear();
                }
                if (dataHistory.Rows.Count > 0)
                {
                    dataHistory.Rows.Clear();
                }
                dgvHistoryData.DataSource = null;
                newCaseDataPoint.clear();
                criticalCaseDataPoint.clear();
                recoveredCaseDataPoint.clear();
                deathCaseDataPoint.clear();
                backgroundWorker1.RunWorkerAsync();


            }


        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            if (countryName == null)
            {
                GetAllContries();
                GetDataWorld();
            }
            else
            {
                GetDataCovidHistory(countryName);
            }

            if (backgroundWorker1.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
        }
        private void GetAllContries()
        {
            // CALL API COUNTRY
            var client = new RestClient("https://covid-193.p.rapidapi.com/countries");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", "0cef955fc3msh29e683182351c7fp1dace2jsn56665f1cae86");
            request.AddHeader("x-rapidapi-host", "covid-193.p.rapidapi.com");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content;
                var countries = JsonConvert.DeserializeObject<Country>(content);

                // add data to dataCountries
                dataContries.Columns.Add("name") ;
                foreach (var country in countries.response)
                {
                    dataContries.Rows.Add(country);
                }
            }
            else
            {
                httpFeedback = response.ErrorMessage;
                backgroundWorker1.CancelAsync();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toatsControl1.Visible = false;
            timer1.Stop();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            labelAC.Text = "0";
            labelDC.Text = "0";
            labelCC.Text = "0";
            labelAcPersentase.Text = "0%";
            labelDCPersentase.Text = "0%";
            labelCCPersentase.Text = "0%";
            labelTotalCases.Text = dataWorld.Rows[0]["total cases"].ToString();
            labelNewCases.Text = dataWorld.Rows[0]["new cases"].ToString();
            labelTotalDeath.Text = dataWorld.Rows[0]["total death"].ToString();
            labelNewDeath.Text = dataWorld.Rows[0]["new death"].ToString();
            labelActiveCases.Text = dataWorld.Rows[0]["active cases"].ToString();
            labelRecovered.Text = dataWorld.Rows[0]["total recovered"].ToString();
            if (e.Cancelled)
            {
                timer1.Start();
                MessageBox.Show(httpFeedback);
            }
            else
            {
                Console.WriteLine(dataHistory.Rows.Count);
                if(dataHistory.Rows.Count > 0)
                {
                    dgvHistoryData.DataSource = dataHistory;

                    labelAC.Text = dataHistory.Rows[0]["Active Cases"].ToString();
                    labelCC.Text = dataHistory.Rows[0]["Critical Cases"].ToString();
                    labelDC.Text = dataHistory.Rows[0]["Total Deaths"].ToString();

                    int labelACNumber = int.Parse(labelAC.Text);
                    int labelCCNumber = int.Parse(labelCC.Text);
                    int labelDCNumber = int.Parse(labelDC.Text);
                    int TotalCaseNumber = int.Parse(dataHistory.Rows[0]["Total Cases"].ToString());

                    int persenAC = 100 * labelACNumber / TotalCaseNumber;
                    float persenCC = 100 * labelCCNumber / TotalCaseNumber;
                    int persenDC = 100 * labelDCNumber / TotalCaseNumber;

                    labelAcPersentase.Text = persenAC.ToString()+"%";
                    labelCCPersentase.Text = persenCC.ToString()+"%";
                    labelDCPersentase.Text = persenDC.ToString()+"%";

                    // add datapoint to canvas

                    Canvas statisticCanvas = new Canvas();
                    statisticCanvas.addData(newCaseDataPoint);
                    statisticCanvas.addData(criticalCaseDataPoint);
                    statisticCanvas.addData(deathCaseDataPoint);
                    statisticCanvas.addData(recoveredCaseDataPoint);

                    //set color
                    
                    bunifuData.colorSet.Add(Color.FromArgb(170, 170, 251));
                    bunifuData.colorSet.Add(Color.FromArgb(250, 100, 1));
                    bunifuData.colorSet.Add(Color.FromArgb(108, 17, 74));
                    bunifuData.colorSet.Add(Color.FromArgb(220, 20, 60));

                   

                    //render to dataviz
                    bunifuData.Render(statisticCanvas);
                }
                
                timer1.Start();
                //MessageBox.Show("Data Loaded","Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormOverview_Load(object sender, EventArgs e)
        {
            toatsControl1.Visible = true;
            backgroundWorker1.RunWorkerAsync();
        }

       

        private void GetDataCovidHistory(string country)
        {
            var client = new RestClient($"https://covid-193.p.rapidapi.com/history?country={country}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", "0cef955fc3msh29e683182351c7fp1dace2jsn56665f1cae86");
            request.AddHeader("x-rapidapi-host", "covid-193.p.rapidapi.com");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content;
                var historyData = JsonConvert.DeserializeObject<Rootobject>(content);
                if(historyData != null)
                {
                    // add data to datatable
                    // create columns 
                    DataColumn[] dataColumns = new DataColumn[]
                    {
                        new DataColumn("Record Time"),
                        new DataColumn("New Cases"),
                        new DataColumn("Active Cases"),
                        new DataColumn("Critical Cases"),
                        new DataColumn("Total Cases"),
                        new DataColumn("New Deaths"),
                        new DataColumn("Total Deaths"),
                        new DataColumn("Total Test")
    
                     

                    };
                    // add columns to datatable
                   
                    dataHistory.Columns.AddRange(dataColumns);
                    historyData.response.Reverse();

                    // get7 data
                    DateTime[] last_seven_day = Enumerable.Range(0, 7).Select(i => DateTime.Now.AddDays(-i)).ToArray();
                    // createing dictionary for dataviz 
                    Dictionary<string, int> newCasesDictionary = new Dictionary<string, int> ();
                    Dictionary<string, int> criticalCasesDictionary = new Dictionary<string, int> ();
                    Dictionary<string, int> deathCasesDictionary = new Dictionary<string, int> ();
                    Dictionary<string, int> recoveredCasesDictionary = new Dictionary<string, int> ();
       
                    // add data to datatable
                    if(rb_week.Checked == true)
                    {
                        foreach (var day in last_seven_day)
                        {

                            // data History
                            foreach (var data in historyData.response)
                            {
                                if (data.cases.critical == null)
                                {
                                    data.cases.critical = "0";
                                }
                                if (data.deaths.New == null)
                                {
                                    data.deaths.New = "0";
                                }
                                if (data.cases.New == null)
                                {
                                    data.cases.New = "0";
                                }
                                if (data.cases.recovered == null)
                                {
                                    data.cases.recovered = "0";
                                }

                                if (data.day.Contains($"{day:yyyy-MM-dd}"))
                                {
                                    DateTime date7 = new DateTime(day.Date.Year, day.Date.Month, day.Date.Day);
                                    dataHistory.Rows.Add($"{day:dd-MM-yyyy}" + " " + date7.ToString("ddd") + "", data.cases.New, data.cases.active, data.cases.critical, data.cases.total, data.deaths.New, data.deaths.total, data.tests.total);
                                    
                                    // add data to dataviz
                                    newCasesDictionary.Add(date7.ToString("ddd"), int.Parse(data.cases.New));
                                    criticalCasesDictionary.Add(date7.ToString("ddd"), int.Parse(data.cases.critical, System.Globalization.NumberStyles.AllowThousands));
                                    deathCasesDictionary.Add(date7.ToString("ddd"), int.Parse(data.deaths.New));
                                    recoveredCasesDictionary.Add(date7.ToString("ddd"), int.Parse(data.cases.recovered, System.Globalization.NumberStyles.AllowThousands));

                                    break;
                                }

                            }

                        }

                        //reverse dictionary
                        var reversedNewCases = newCasesDictionary.Reverse();
                        var reversedCriticalCases = criticalCasesDictionary.Reverse();
                        var reversedDeathCases = deathCasesDictionary.Reverse();
                        var reversedRecoveredCases = recoveredCasesDictionary.Reverse();

                        foreach (var newCases in reversedNewCases)
                        {
                            newCaseDataPoint.addLabely(newCases.Key, newCases.Value);
                        }
                        foreach (var criticalCases in reversedCriticalCases)
                        {
                            criticalCaseDataPoint.addLabely(criticalCases.Key, criticalCases.Value);
                        }
                        foreach (var deathCases in reversedDeathCases)
                        {
                            deathCaseDataPoint.addLabely(deathCases.Key, deathCases.Value);
                        }
                        foreach (var recoveredCases in reversedNewCases)
                        {
                            recoveredCaseDataPoint.addLabely(recoveredCases.Key, recoveredCases.Value);
                        }
                    }
                    else
                    {
                        bunifuData.Render(null);
                    }
                  
                }
                else
                {
                    MessageBox.Show("No data","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    backgroundWorker1.CancelAsync();
                }

            }
            else
            {
                httpFeedback = response.StatusDescription;
                backgroundWorker1.CancelAsync();
            }
        }

        private void GetDataWorld()
        {
            var client = new RestClient("https://vaccovid-coronavirus-vaccine-and-treatment-tracker.p.rapidapi.com/api/npm-covid-data/world");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", "0cef955fc3msh29e683182351c7fp1dace2jsn56665f1cae86");
            request.AddHeader("x-rapidapi-host", "vaccovid-coronavirus-vaccine-and-treatment-tracker.p.rapidapi.com");
            IRestResponse response = client.Execute(request);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content;
                var WorldData = JsonConvert.DeserializeObject<List<Data>>(content);
                dataWorld.Columns.Add("total cases");
                dataWorld.Columns.Add("new cases");
                dataWorld.Columns.Add("total death");
                dataWorld.Columns.Add("new death");
                dataWorld.Columns.Add("active cases");
                dataWorld.Columns.Add("total recovered");
                foreach(var data in WorldData)
                {
                    dataWorld.Rows.Add(data.TotalCases, data.NewCases, data.TotalDeaths, data.NewDeaths, data.ActiveCases, data.TotalRecovered);
                }

            }
            else
            {
                httpFeedback = response.ErrorMessage;
                backgroundWorker1.CancelAsync();
            }
        }
    }
}
