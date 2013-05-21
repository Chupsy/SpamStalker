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
    public partial class Form3 : Form
    {
        Client client = new Client();
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adress = textBox1.Text;
            string description = textBox2.Text;
            string message = "!ADDA "+ adress+ " " + description;
            client.Connect(message);
        }
    }
}
