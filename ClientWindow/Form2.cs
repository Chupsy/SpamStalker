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
        User _user;
       
       
        public Form2(Client client, Session session)
        {
            InitializeComponent();
            _client = client;
            _session = session;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_client.Connect(textBox1.Text, textBox2.Text))
            {
                _user = _client.GetData();
                _user.IsInitialized = true;
                _session.IsInitialized = true;
                _user.Username = textBox1.Text;
                _session.User = _user;

                this.Close();
            }
            else { MessageBox.Show("Error in username or password"); }
        }
    }
}
