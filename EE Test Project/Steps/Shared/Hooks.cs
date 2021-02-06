using EE_Test_Project.Utilities;
using TechTalk.SpecFlow;
using EE_Test_Project.Pages;

namespace EE_Test_Project.Steps.Shared
{
    [Binding]
    public class Hooks
    {
        [BeforeScenario()]
        public static void Setup()
        {
            //Using appsettings.QA.json for settings
            Driver.BaseUrl = Driver.GetBaseUrl();
            Driver.DefaultTimeout = Driver.GetTimeouSeconds();
            Driver.OpenBrowser();
        }    

        [AfterScenario()]
        public static void ShutDown()
        {
            Driver.ShutDown();
        }
    }
}
