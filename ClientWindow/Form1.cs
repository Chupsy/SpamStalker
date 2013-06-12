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
        User _user;
        Session _session;
        string _selectedAdress;
        int _selectedindex;
        int _selectedblackadrs;
        string _username;




        public Form1(Client client, User user, string username)
        {
            InitializeComponent();
            _username = username;
            _client = client;
            _user = user;
            _session = new Session();

            LoadAddresses();
            comboBox1.SelectedIndex = 0;
            LoadBlacklist();
            this.Text += " " + _username;
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
            Form3 adWindow = new Form3(_client, _session, _user);

            adWindow.ShowDialog();
            if (adWindow.HasWorked())
            {
                _session.User.Username = _username;
                _user = _session.User;
            }

            LoadAddresses();
            LoadBlacklist();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBlacklist();
        }

        private void LoadAddresses()
        {
            comboBox1.Items.Clear();
            foreach (Address adress in _user.Addresses)
            {
                comboBox1.Items.Add(adress.SubscriptionAddress);
            }

        }

        private void LoadFuckIndicator()
        {
            _fuckList.Clear();
            foreach (BlackEmailAddress blackAddress in _user.Addresses[_selectedindex].Blacklist)
            {
                if (blackAddress.IsFucking == true)
                {
                    _fuckList.Add(1);
                }
                else
                {
                    _fuckList.Add(0);
                }
            }
            listBox2.DataSource = null;
            if (listBox2.Items != null)
            {
                listBox2.Items.Clear();
            }
            BindingList<int> binding = new BindingList<int>(_fuckList);
            listBox2.DataSource = binding;
        }


        private void LoadBlacklist()
        {
            _selectedAdress = (string)comboBox1.SelectedItem;
            foreach (Address a in _user.Addresses)
            {
                if (a.SubscriptionAddress == _selectedAdress)
                {
                    _selectedindex = _user.Addresses.IndexOf(a);
                }
            }
            _blackList.Clear();

            foreach (BlackEmailAddress blackAddress in _user.Addresses[_selectedindex].Blacklist)
            {
                _blackList.Add(blackAddress.Address);
            }
            listBox1.DataSource = null;
            if (listBox1.Items != null)
            {
                listBox1.Items.Clear();
            }

            BindingList<string> binding = new BindingList<string>(_blackList);
            listBox1.DataSource = binding;
            LoadFuckIndicator();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedblackadrs = listBox1.SelectedIndex;
            if (_selectedblackadrs != 0) listBox2.SelectedIndex = listBox1.SelectedIndex;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_user.Addresses[_selectedindex].Blacklist.Count > 0)
            {
                DelBlackAdrCommand _delBlackAdrCommand = new DelBlackAdrCommand(_user, _client, _user.Addresses[_selectedindex].Blacklist[_selectedblackadrs].Address, _user.Addresses[_selectedindex].SubscriptionAddress, _session);

                if (_delBlackAdrCommand.Execute())
                {
                    _session.User.Username = _username;
                    _user = _session.User;
                }
                LoadAddresses();
                LoadBlacklist();
            }
            else
            {
                MessageBox.Show("You have no address in your blacklist");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (_blackList.Count != 0)
            {
                FuckCommand _fuckCommand = new FuckCommand(_user, _client, _user.Addresses[_selectedindex].SubscriptionAddress, _user.Addresses[_selectedindex].Blacklist[_selectedblackadrs].Address, _session);
                if (_fuckCommand.Execute())
                {
                    _session.User.Username = _username;
                    _user = _session.User;

                }
                LoadAddresses();
                LoadBlacklist();

            }
            else
            {
                MessageBox.Show("No blacklisted email address selected");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (_blackList.Count != 0)
            {
                UnFuckCommand _unFuckCommand = new UnFuckCommand(_user, _client, _user.Addresses[_selectedindex].SubscriptionAddress, _user.Addresses[_selectedindex].Blacklist[_selectedblackadrs].Address, _session);
                if (_unFuckCommand.Execute())
                {
                    _session.User.Username = _username;
                    _user = _session.User;

                }
                LoadAddresses();
                LoadBlacklist();
            }
            else
            {
                MessageBox.Show("No blacklisted email address selected");
            }
        }

        private void AddBlackAddress_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                AdBlackAdrCommand _adBlackAdrCommand = new AdBlackAdrCommand(_user, _client, _user.Addresses[_selectedindex].SubscriptionAddress, textBox1.Text, _session);
                if (_adBlackAdrCommand.Execute())
                {
                    _session.User.Username = _username;
                    _user = _session.User;
                    textBox1.Clear();
                }
                LoadAddresses();
                LoadBlacklist();
            }
        }

        private void DeleteAddress_Click(object sender, EventArgs e)
        {
            DelAdrCommand _delAdrCommand = new DelAdrCommand(_user, _client, _user.Addresses[_selectedindex].SubscriptionAddress, _session);
            if (MessageBox.Show("Do you really want to delete this address :" + Environment.NewLine + _user.Addresses[_selectedindex].SubscriptionAddress, "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (_delAdrCommand.Execute())
                {
                    _session.User.Username = _username;
                    _user = _session.User;
                    textBox1.Clear();
                }
                comboBox1.SelectedIndex = 0;
                LoadAddresses();
                LoadBlacklist();

            }
        }
    }
}
