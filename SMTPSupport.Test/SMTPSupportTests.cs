//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NUnit.Framework;
//using SMTPSupport;
//using System.Net.Mail;

//namespace SMTPSupport.Test
//{
//    [TestFixture]
//    public class SMTPSupportTests
//    {
//        [Test]
//        public void TestEnum()
//        {
//            Assert.That((int)ErrorCode.Ok, Is.EqualTo(250));
//        }

//        [Test]
//        public void TestEHLO()
//        {
//            SMTPParser parser = new SMTPParser();
//            SMTPSession session = new SMTPSession(new MetaCommandAPI());
//            SMTPClientTest client = new SMTPClientTest();
//            parser.Execute("EHLO tutu", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("250 tutu"));
//            Assert.That(session.IsInitialized, Is.True);
//            client.Clear();
//            session = new SMTPSession(new MetaCommandAPI());
//            parser.Execute("EHLO", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 500"));
//            Assert.That(session.IsInitialized, Is.False);
//            client.Clear();

//            session = new SMTPSession(new MetaCommandAPI());
//            parser.Execute("HELO tutu", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("250 tutu"));
//            Assert.That(session.IsInitialized, Is.True);
//            client.Clear();
//            session = new SMTPSession(new MetaCommandAPI());
//            parser.Execute("HELO", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 500"));
//            Assert.That(session.IsInitialized, Is.False);
//            client.Clear();

//            session = new SMTPSession(new MetaCommandAPI());
//            parser.Execute("BO", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 500\r\nClose\r\n"));
//            Assert.That(session.IsInitialized, Is.False);
//            client.Clear();

//            session = new SMTPSession(new MetaCommandAPI());
//            parser.Execute("BOHOYT", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 500\r\nClose\r\n"));
//            Assert.That(session.IsInitialized, Is.False);
//            client.Clear();

//            session = new SMTPSession(new MetaCommandAPI());
//            parser.Execute("", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 500\r\nClose\r\n"));
//            Assert.That(session.IsInitialized, Is.False);
//            client.Clear();
//        }

//        [Test]
//        public void TestRCPT()
//        {
//            SMTPParser parser = new SMTPParser();
//            SMTPSession session = new SMTPSession(new MetaCommandAPI());
//            SMTPClientTest client = new SMTPClientTest();
//            parser.Execute("EHLO tutu", session, client);
//            client.Clear();
//            parser.Execute("RCPT TO:<tutu@msn.com>", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("Success"));
//            Assert.That(session.mail.To[0].Address.Contains("tutu@msn.com"));
//            client.Clear();

//            parser.Execute("RCPT TO:<arnold@shwartz.com>", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("Success"));
//            Assert.That(session.mail.To[1].Address.Contains("arnold@shwartz.com"));
//            Assert.That(session.mail.To[0].Address.Contains("tutu@msn.com"));

//            session = new SMTPSession(new MetaCommandAPI());
//            parser.Execute("EHLO tutu", session, client);
//            client.Clear();
//            parser.Execute("RCPT TO", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 500"));

//            client.Clear();
//            parser.Execute("RCPT TO:<tutu@msn.com", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 500"));

//            client.Clear();
//            parser.Execute("RCPT TO:<>", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 501"));


//            client.Clear();
//            parser.Execute("RCPT TO:<toto@robert.com>", session, client);
//            Assert.That(session.mail.To.Count, Is.EqualTo(1));
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 550\r\n"));

//            session = new SMTPSession(new MetaCommandAPI());
//            parser.Execute("RCPT TO:<tutu@msn.com>", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 500\r\nClose\r\n"));
//            Assert.That(session.mail.To.Count, Is.EqualTo(0));
//        }

//        [Test]
//        public void TestNOOP()
//        {
//            SMTPParser parser = new SMTPParser();
//            SMTPSession session = new SMTPSession(new MetaCommandAPI());
//            SMTPClientTest client = new SMTPClientTest();
//            parser.Execute("EHLO tutu", session, client);
//            client.Clear();
//            parser.Execute("NOOP", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("Success"));
//            client.Clear();
//            parser.Execute("NOOP dsfqfsqifoj", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("Success"));
//            client.Clear();

//            session = new SMTPSession(new MetaCommandAPI());
//            parser.Execute("NOOP", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 500\r\nClose\r\n"));
//        }

//        [Test]
//        public void TestQUIT()
//        {
//            SMTPParser parser = new SMTPParser();
//            SMTPSession session = new SMTPSession(new MetaCommandAPI());
//            SMTPClientTest client = new SMTPClientTest();
//            parser.Execute("EHLO tutu", session, client);
//            client.Clear();
//            parser.Execute("QUIT", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 221\r\nClose\r\n"));
//            client.Clear();

//            parser.Execute("QUIT dsfqfsqifoj", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 221\r\nClose\r\n"));
//            client.Clear();

//            session = new SMTPSession(new MetaCommandAPI());
//            parser.Execute("QUIT", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 221\r\nClose\r\n"));
//        }

//        [Test]
//        public void TestMAIL()
//        {
//            SMTPParser parser = new SMTPParser();
//            SMTPSession session = new SMTPSession(new MetaCommandAPI());
//            SMTPClientTest client = new SMTPClientTest();
//            MailAddress testSender = new MailAddress("vincent@waza.com");
//            parser.Execute("EHLO tutu", session, client);
//            client.Clear();
//            parser.Execute("MAIL FROM:<vincent@waza.com>", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("Success"));
//            Assert.That(session.Sender.Address == testSender.Address);
//            client.Clear();

//            session = new SMTPSession(new MetaCommandAPI());
//            testSender = new MailAddress("dufrasnes@cake.fr");
//            parser.Execute("EHLO tutu", session, client);
//            client.Clear();
//            parser.Execute("MAIL FROM:<dufrasnes@cake.fr>", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 500"));
//            client.Clear();

//            session = new SMTPSession(new MetaCommandAPI());
//            parser.Execute("MAIL FROM:<>", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 501"));
//            client.Clear();

//            session = new SMTPSession(new MetaCommandAPI());
//            parser.Execute("MAIL FROM:<johan@bouh.com>", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 500"));
//            client.Clear();

//            session = new SMTPSession(new MetaCommandAPI());
//            parser.Execute("MAIL Blueberry is good", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("SendError: 500"));
//            client.Clear();


//        }

//        [Test]
//        public void TestRSET()
//        {
//            SMTPParser parser = new SMTPParser();
//            SMTPSession session = new SMTPSession(new MetaCommandAPI());
//            SMTPClientTest client = new SMTPClientTest();
//            MailAddress testSender = new MailAddress("vincent@test.com");
//            parser.Execute("EHLO tutu", session, client);
//            parser.Execute("RCPT TO:<tutu@msn.com>", session, client);
//            parser.Execute("MAIL FROM:<vincent@test.com>", session, client);
//            client.Clear();

//            parser.Execute("RSET", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("Success"));
//            Assert.That(session.mail.To.Count, Is.EqualTo(0));
//            Assert.That(session.Sender, Is.Null);

//       }

//        [Test]
//        public void TestHELP()
//        {
//            SMTPParser parser = new SMTPParser();
//            SMTPSession session = new SMTPSession(new MetaCommandAPI());
//            SMTPClientTest client = new SMTPClientTest();
//            MailAddress testSender = new MailAddress("vincent@test.com");
//            parser.Execute("HELO tutu", session, client);
//            parser.Execute("HELP", session, client);
//            Assert.That(client.ToString(), Is.StringContaining("RCPT : Adds a recipient mail address.HELO : Initialize a stream. (HELO is also working)EHLO : Initialize a stream. (HELO is also working)MAIL : Specifies sender mail adress.HELP : Shows SMTP commands help.NOOP : Require 250 OK.QUIT : Shut down session.DATA : Get data from user.RSET : Clear all session.!EHLO : Initialize a mail owner - server transmission"));
//            Assert.That(client.ToString(), Is.StringContaining("250 OK"));
//        }
//    }
//}
