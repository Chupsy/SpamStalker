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
        }

    }
}
