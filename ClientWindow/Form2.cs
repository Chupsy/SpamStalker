using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSupport;

namespace ClientWindow
{
    public partial class Form2 : Form
    {
        Client _client;
        Session _session;
        string data;
        User _user;
       
        public Form2(Client client, Session session)
        {
            InitializeComponent();
            _client = client;
            _session = session;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _client.Connect(textBox1.Text, textBox2.Text);
            data = _client.GetData();
            _user = User.ParseInfos(data);
            _session.Data = _user.Data;

            _session.IsInitialized = true;
            this.Close();
        }
    }
}
