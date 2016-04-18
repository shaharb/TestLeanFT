using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HP.LFT.SDK;
using HP.LFT.SDK.Web;
using HP.LFT.Verifications;
using HP.LFT.Report;

namespace LeanFtTestProject1
{
    [TestClass]
    public class LeanFtTest2 : UnitTestClassBase<LeanFtTest2>
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            GlobalSetup(context);
        }

        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void Verify_CounterShowsNumberOfButtonClicks()
        {
            int numOfClicks = 10;

            // Launch the browser and navigate to site.
            var browser = BrowserFactory.Launch(BrowserType.Chrome);
            browser.Navigate("http://shaharbehagenmail.wix.com/tutoring-company");

            // Wait for the browser to complete the navigation.
            browser.Sync();

            try
            {
                IFrame frame = browser.Describe<IFrame>(new FrameDescription
                {
                    Id = string.Empty,
                    Name = string.Empty,
                    Index = 2
                });

                IButton butt = frame.Describe<IButton>(new ButtonDescription
                {
                    ButtonType = @"button",
                    TagName = @"BUTTON",
                    Name = @"Click Me"
                });

                IWebElement counter = frame.Describe<IWebElement>(new WebElementDescription
		        {
			        TagName = @"H1",
			        // InnerText = @"1" // Cannot use InnerText to describe object, since changes during the test.
		        });
            
                // Give it a few good Buttkicks!
                for (int i = 0; i < numOfClicks; i++)             
                    butt.Click();           
            
                // (Is this necessary here?)
                browser.Sync();          

                // Verify that the web page counter shows the number of clicks we just clicked.               
                int presentedCounter = Int32.Parse(counter.InnerText);

                Assert.AreEqual(numOfClicks, presentedCounter);

                Reporter.ReportEvent("Verify_CounterShowsNumberOfButtonClicks", "Passed validation", Status.Passed);
            }            
            catch (AssertFailedException e) //catch (AssertionException e)
            // Add a log message to the results report on failure.
            {
                Reporter.ReportEvent("Verify_CounterShowsNumberOfButtonClicks", "Failed during validation", Status.Failed, e);
                throw;
            }
            finally
            {
                browser.Close();
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            GlobalTearDown();
        }
    }
}
