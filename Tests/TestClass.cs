using NUnit.Framework;
using VSCodeSelenium.Models;

namespace VSCodeSelenium.Tests
{

    [TestFixture]
    public class TestProgram : BaseClass

    {     
        [SetUp]

        public void SetupTest () {
            browser();
            InitialiseBrowser();
            URL();
        }

   
        [Category ("Google Category")]
        [Test]
        public void NewTestForExample () {
            var page = new GooglePage_Model(Driver);
            Assert.AreEqual ("test", page.Link.Text);
         }
    }
}