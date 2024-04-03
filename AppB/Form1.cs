using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Xml;
using System.Xml.Linq;

namespace AppB
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {

            //define endPoint
            string endPoint = "http://localhost:57696/api/somiod/casa/porta/data/";

            // Create a Uri object from the endpoint URL
            Uri uri = new Uri(endPoint);
            generateRequest(uri, "open", "Data");

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {

            //define endPoint
            string endPoint = "http://localhost:57696/api/somiod/casa/porta/data/";

            // Create a Uri object from the endpoint URL
            Uri uri = new Uri(endPoint);
            generateRequest(uri, "close", "Data");

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (!isExistingApp().Result)
            {
                //define endPoint
                string endPoint = "http://localhost:57696/api/somiod/";

                // Create a Uri object from the endpoint URL
                Uri uri = new Uri(endPoint);

                generateRequest(uri, null, "Application");
            }
        }

        private async void generateRequest(Uri uri, string status, string type)
        {
            //xml payload
            string xml = "";
            string curDate = DateTime.Now.ToString("yyyy-MM-dd");

            if (type == "Data" && status != null)
            {
               xml = generateDataXML(status, "porta", curDate);
            }
            else if(type == "Application")
            {
                xml = generateApplicationXML("Switch", curDate);
            }

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                HttpMethod httpMethod = HttpMethod.Post;
                StringContent stringContent = new StringContent(xml, Encoding.UTF8, "application/xml");

                try
                {
                    HttpResponseMessage response = await httpClient.PostAsync(uri, stringContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException($"HTTP request failed with status code " +
                            $"{response.StatusCode}. Reason: {response.ReasonPhrase}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"An error occurred during the HTTP request: {ex?.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex?.Message}");
                }

            }
        }

        private async Task<bool> isExistingApp()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync("http://localhost:57696/api/somiod/Switch").ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        // Check if the response has content
                        if (response.Content != null)
                        {
                            string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            // Add your specific condition to determine existence based on the content
                            // read the xml
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(content);

                            // get the values
                            string id = doc.GetElementsByTagName("id")[0].InnerText;


                            bool exists = id != "0";

                            return exists;
                        }
                    }
                    else
                    {
                        throw new HttpRequestException($"HTTP request failed with status code " +
                            $"{response.StatusCode}. Reason: {response.ReasonPhrase}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"An error occurred during the HTTP request: {ex?.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex?.Message}");
                }

                // If an exception occurred or the response was not successful, return false
                return false;
            }
        }


        private string generateApplicationXML(string nameText, string creation_dtText)
        {
            XmlDocument doc = new XmlDocument();

            // Create the XML Declaration, and append it to XML document
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);

            XmlElement data = doc.CreateElement("Application");
            doc.AppendChild(data);

            XmlElement name = doc.CreateElement("name");
            name.InnerText = nameText;

            XmlElement creationDate = doc.CreateElement("creation_dt");
            creationDate.InnerText = creation_dtText;


            data.AppendChild(name);
            data.AppendChild(creationDate);

            string xmlOutput = doc.OuterXml;

            return xmlOutput;
        }

        private string generateDataXML(string contentText, string nameText, string creation_dtText)
        {
            XmlDocument doc = new XmlDocument();

            // Create the XML Declaration, and append it to XML document
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);

            XmlElement data = doc.CreateElement("Data");
            doc.AppendChild(data);

            XmlElement name = doc.CreateElement("name");
            name.InnerText = nameText;


            XmlElement creationDate = doc.CreateElement("creation_dt");
            creationDate.InnerText = creation_dtText;

            XmlElement content = doc.CreateElement("content");
            content.InnerText = contentText;


            data.AppendChild(name);
            data.AppendChild(content);
            data.AppendChild(creationDate);

            string xmlOutput = doc.OuterXml;

            return xmlOutput;
        }

    }
}
