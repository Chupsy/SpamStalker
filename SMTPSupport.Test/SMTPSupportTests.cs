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
            Assert.That(client.ToString(), Is.StringContaining("Success"));
        }
        
    }
}
