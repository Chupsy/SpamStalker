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
    public partial class Form3 : Form
    {
        Client _client;
        Session _session;
        User _user;
        bool _worked;

        public Form3(Client client, Session session, User user)
        {
            _user = user;
            _client = client;
            _session = session;
            _worked = false;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adress = textBox1.Text;
            string description = textBox2.Text;
            string relayAddress = textBox3.Text;
            AddAdrCommand command = new AddAdrCommand(_user, _session, _client, adress, description, relayAddress);
           _worked = command.Execute();
            this.Close();
        }

        public bool HasWorked()
        {
            return _worked;
        }
    }
}
