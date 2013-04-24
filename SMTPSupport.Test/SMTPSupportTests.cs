using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SMTPSupport;

namespace SMTPSupport.Test
{
    [TestFixture]
    public class SMTPSupportTests
    {
        [Test]
        public void testEHLO()
        {
            SMTPParser parser = new SMTPParser();
            SMTPSession session = new SMTPSession();
            SMTPClientTest client = new SMTPClientTest();
            parser.Execute("EHLO tutu", session, client);
            Assert.That(client.ToString(), Is.StringContaining("Success"));
            Assert.That(session.IsInitialized, Is.True);
            client.Clear();
            session = new SMTPSession();
            parser.Execute("EHLO", session, client);
            Assert.That(client.ToString(), Is.StringContaining("SendError: 500 Missing domain name."));
            Assert.That(session.IsInitialized, Is.False);
            client.Clear();

            session = new SMTPSession();
            parser.Execute("HELO tutu", session, client);
            Assert.That(client.ToString(), Is.StringContaining("Success"));
            Assert.That(session.IsInitialized, Is.True);
            client.Clear();
            session = new SMTPSession();
            parser.Execute("HELO", session, client);
            Assert.That(client.ToString(), Is.StringContaining("SendError: 500 Missing domain name."));
            Assert.That(session.IsInitialized, Is.False);
            client.Clear();

            session = new SMTPSession();
            parser.Execute("BO", session, client);
            Assert.That(client.ToString(), Is.StringContaining("SendError: 500\r\nClose\r\n"));
            Assert.That(session.IsInitialized, Is.False);
            client.Clear();

            session = new SMTPSession();
            parser.Execute("BOHOYT", session, client);
            Assert.That(client.ToString(), Is.StringContaining("SendError: 500\r\nClose\r\n"));
            Assert.That(session.IsInitialized, Is.False);
            client.Clear();

            session = new SMTPSession();
            parser.Execute("", session, client);
            Assert.That(client.ToString(), Is.StringContaining("SendError: 500\r\nClose\r\n"));
            Assert.That(session.IsInitialized, Is.False);
            client.Clear();
        }

        [Test]
        public void testRCPT()
        {
            SMTPParser parser = new SMTPParser();
            SMTPSession session = new SMTPSession();
            SMTPClientTest client = new SMTPClientTest();
            parser.Execute("EHLO tutu", session, client);
            client.Clear();
            parser.Execute("RCPT TO:<tutu@msn.com>", session, client);
            Assert.That(client.ToString(), Is.StringContaining("Success"));
            Assert.That(session.Recipients.Contains("tutu@msn.com"));
            client.Clear();

            parser.Execute("RCPT TO:<arnold@shwartz.com>", session, client);
            Assert.That(client.ToString(), Is.StringContaining("Success"));
            Assert.That(session.Recipients.Contains("arnold@shwartz.com"));
            Assert.That(session.Recipients.Contains("tutu@msn.com"));

            session = new SMTPSession();
            parser.Execute("EHLO tutu", session, client);
            client.Clear();
            parser.Execute("RCPT TO", session, client);
            Assert.That(client.ToString(), Is.StringContaining("SendError: 500 Syntax error."));

            client.Clear();
            parser.Execute("RCPT TO:<tutu@msn.com", session, client);
            Assert.That(client.ToString(), Is.StringContaining("SendError: 500 Syntax error."));

            client.Clear();
            parser.Execute("RCPT TO:<>", session, client);
            Assert.That(client.ToString(), Is.StringContaining("SendError: 501 Missing mail address."));


            client.Clear();
            parser.Execute("RCPT TO:<toto@robert.com>", session, client);
            Assert.That(session.Recipients.Count, Is.EqualTo(0));
            Assert.That(client.ToString(), Is.StringContaining("SendError: 550 No such user here."));

        }
        
    }
}
