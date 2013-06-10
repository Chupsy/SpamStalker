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
    public partial class Form1 : Form
    {

        List<string> _blackList = new List<string>();
        List<int> _fuckList = new List<int>();
        Client _client;
        Session _session;
        string _selectedAdress;
        int _selectedindex;
        int _selectedblackadrs;
        int i;



        public Form1(Client client, Session session)
        {
            InitializeComponent();
            _client = client;
            _session = session;

            LoadAddresses();
            comboBox1.SelectedIndex = 0;
            LoadBlacklist();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //string message = "EHLO tutu";
            //_client.Connect(message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 adWindow = new Form3(_client, _session);
            adWindow.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBlacklist();
        }

        private void LoadAddresses()
        {
            foreach (Address adress in _session.Data)
            {
                comboBox1.Items.Add(adress.UserAddress.Address);
            }

        }

        private void LoadFuckIndicator()
        {
            i = 0;
            foreach (BlackEmailAddress blackAddress in _session.Data[_selectedindex].AddressBlacklist.list)
            {
                if (blackAddress.IsFucking == true)
                {
                    _fuckList[i] = 1;
                }
                else
                {
                    _fuckList[i] = 0;
                }
                i++;
            }
            listBox2.DataSource = _fuckList;
        }


        private void LoadBlacklist()
        {
            _selectedAdress = (string)comboBox1.SelectedItem;
            _selectedindex = (int)comboBox1.SelectedItem;

            foreach (BlackEmailAddress blackAddress in _session.Data[_selectedindex].AddressBlacklist.list)
            {
                _blackList.Add(blackAddress.Address);
            }

            listBox1.DataSource = _blackList;
            LoadFuckIndicator();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedblackadrs = (int)listBox1.SelectedIndex;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DelBlackAdrCommand _delBlackAdrCommand = new DelBlackAdrCommand(_session, _client, _session.Data[_selectedindex].AddressBlacklist.list[_selectedblackadrs].Address);
            _delBlackAdrCommand.Execute();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FuckCommand _fuckCommand = new FuckCommand(_session, _client, _session.Data[_selectedindex].UserAddress.Address, _session.Data[_selectedindex].AddressBlacklist.list[_selectedblackadrs].Address);
            _fuckCommand.Execute();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UnFuckCommand _unFuckCommand = new UnFuckCommand(_session, _client, _session.Data[_selectedindex].UserAddress.Address, _session.Data[_selectedindex].AddressBlacklist.list[_selectedblackadrs].Address);
            _unFuckCommand.Execute();
        }
    }
}
