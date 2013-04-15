using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

    namespace FakeSmtp.Data.Tests
    {
        [TestFixture]
        public class FakeSmtpTest
        {
            [Test]
            public void MailIncomeToMailMessage()
            {
                string to = "tutu@intechinfo.fr";
                string from = "boby@hotmail.net";
                string body = "coucou comment ca va?";
                string cc = "robbert@test.fr";
                string subject = "coucou";
                MailTransfer NewMail = new MailTransfer(to, from, cc, subject, body);
                Assert.That(NewMail.To, Is.EqualTo("tutu@intechinfo.fr"));
                Assert.That(NewMail.From, Is.EqualTo("boby@hotmail.net"));
                Assert.That(NewMail.Cc, Is.EqualTo("robbert@test.fr"));
                Assert.That(NewMail.Body, Is.EqualTo(body));
                Assert.That(NewMail.Subject, Is.EqualTo(subject));
            }

        }
    }


