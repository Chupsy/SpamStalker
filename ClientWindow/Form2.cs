using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWindow
{
    public partial class Form2 : Form
    {
        Client client = new Client();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string logins = textBox1.Text +" "+ textBox2.Text;
            string message = "!EHLO " + logins;
            client.Connect(message);
        }
    }
}
