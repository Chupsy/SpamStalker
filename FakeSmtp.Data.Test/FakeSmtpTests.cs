using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FakeSmtp;
using DataSupport;

    namespace FakeSmtp.Data.Tests
    {
        [TestFixture]
        public class FakeSmtpTest
        {
            [Test]
            public void test()
            {
                User test = User.GetInfo("vincent", "C:\\Users\\Admin\\Semestre 3\\spamkiller\\test");
                Assert.That(test.Username, Is.EqualTo("vincent"));
                test = User.GetData(test, "C:\\Users\\Admin\\Semestre 3\\spamkiller\\test");
                Assert.That(test.Username, Is.EqualTo("vincent"));

            }
        }
    }


