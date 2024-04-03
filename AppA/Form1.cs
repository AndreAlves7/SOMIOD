using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using uPLibrary.Networking.M2Mqtt;
using System.Net.Http;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace AppA
{
    public partial class Form1 : Form
    {

        MqttClient mClient = new MqttClient(IPAddress.Parse("127.0.0.1")); //OR use the broker hostname


        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
           //esconder a pictureBox1
           pictureBox1.Visible = false;

            string curDate = DateTime.Now.ToString("yyyy-MM-dd");

            string appName = "casa";
            string containerName = "porta";
            string subscriptionName = "sub1";



            //antes de criar a app, verificar se já existe
            var client4 = new HttpClient();
            var result4 = await client4.GetAsync($"http://localhost:57696/api/somiod/{appName}/");
            var resultContent4 = await result4.Content.ReadAsStringAsync();

            //verificar o xml
            //
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(resultContent4);

            // get the id
            string id = doc.GetElementsByTagName("id")[0].InnerText;

            if (id != "0")
            {
                MessageBox.Show("Application already exists, no need to create it again :)");
            }
            else
            {

                //fazer pedido ao somiod para criar a application
                var client = new HttpClient();
                var content = generateApplicationXML(appName, curDate);
                var result = await client.PostAsync("http://localhost:57696/api/somiod/", new StringContent(content, Encoding.UTF8, "application/xml"));
                var resultContent = await result.Content.ReadAsStringAsync();
                MessageBox.Show(resultContent);


                //fazer pedido ao somiod para criar a o container
                var client2 = new System.Net.Http.HttpClient();
                var content2 = generateContainerXML(containerName, curDate);
                var result2 = await client2.PostAsync($"http://localhost:57696/api/somiod/{appName}/", new StringContent(content2, Encoding.UTF8, "application/xml"));
                var resultContent2 = await result2.Content.ReadAsStringAsync();
                MessageBox.Show(resultContent2);


                //fazer pedido ao somiod para criar a subscription
                var client3 = new HttpClient();
                var content3 = generateSubscriptionXML(subscriptionName, curDate, "mqtt://127.0.0.1:1883", "1"); //1-creation 2-deletion
                var result3 = await client3.PostAsync($"http://localhost:57696/api/somiod/{appName}/{containerName}/sub/", new StringContent(content3, Encoding.UTF8, "application/xml"));
                var resultContent3 = await result3.Content.ReadAsStringAsync();
                MessageBox.Show(resultContent3);


             }

                mClient.Connect(Guid.NewGuid().ToString());
            if (mClient.IsConnected)
            {
                MessageBox.Show("Connected successfully!");

                string[] topicToSubscribe = { $"api/somiod/{appName}/{containerName}"};

                //subscribe to the topi
                mClient.Subscribe(topicToSubscribe, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });


                //event handler for receiving messages
                mClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            }
            else
            {
                MessageBox.Show("Error connecting to MQTT broker...");
            }
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //handle message received
            string message = Encoding.Default.GetString(e.Message);
            //MessageBox.Show(message);

            // read the xml
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(message);

            // get the values
            string value = doc.GetElementsByTagName("value")[0].InnerText;

            // Atualizar a UI no thread correto
            this.Invoke((MethodInvoker)delegate
            {
                if (value == "open")
                {
                    // Mostrar a pictureBox1 e esconder a pictureBox2
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = false;
                }
                else if (value == "close")
                {
                    // Esconder a pictureBox1 e mostrar a pictureBox2
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = true;
                }
            });



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private string generateSubscriptionXML(string nameText, string creation_dtText, string _endpoint, string _event)
        {
            XmlDocument doc = new XmlDocument();

            // Create the XML Declaration, and append it to XML document
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);

            XmlElement data = doc.CreateElement("Subscription");
            doc.AppendChild(data);

            XmlElement name = doc.CreateElement("name");
            name.InnerText = nameText;

            XmlElement creationDate = doc.CreateElement("creation_dt");
            creationDate.InnerText = creation_dtText;

            XmlElement event_ = doc.CreateElement("event_");
            event_.InnerText = _event;

            XmlElement endpoint = doc.CreateElement("endpoint");
            endpoint.InnerText = _endpoint;
                



            data.AppendChild(name);
            data.AppendChild(creationDate);
            data.AppendChild(event_);
            data.AppendChild(endpoint);

            string xmlOutput = doc.OuterXml;

            return xmlOutput;


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


        private string generateContainerXML(string nameText, string creation_dtText)
        {
            XmlDocument doc = new XmlDocument();

            // Create the XML Declaration, and append it to XML document
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);

            XmlElement data = doc.CreateElement("Container");
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
