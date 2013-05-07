﻿using System;
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
    public partial class Form1 : Form
    {
        string[] fromAddresses = System.IO.File.ReadAllLines(@"..\..\..\FakeSmtp\senderAdresses.txt");
        string[] toAddresses = System.IO.File.ReadAllLines(@"..\..\..\FakeSmtp\adresses.txt");
        List<string> _blackList = new List<string>();
       
        public Form1()
        {
            InitializeComponent();

            foreach (string adress in fromAddresses)
            {
                _blackList.Add(adress);
            }
            listBox1.DataSource = _blackList;

            foreach (string adress in toAddresses)
            {
                comboBox1.Items.Add(adress);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
