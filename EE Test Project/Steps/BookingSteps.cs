using System;
using TechTalk.SpecFlow;
using EE_Test_Project.Pages;
using EE_Test_Project.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace EE_Test_Project.Steps
{
    [Binding]
    class BookingSteps
    {
        private readonly BookingPage theBookingPage;
        private int RequiredInitialNumberOfBookingPageRows = 5; //Numeber of expected rows displayed +3 ( due to headings and blank line etc.)

        public BookingSteps()
        {
            theBookingPage = new BookingPage();
        }

        [Given(@"I navigate to the booking page")]
        public void GivenINavigateToTheBookingPage()
        {
            theBookingPage.GoToBookingPage();
            Driver.CheckPageTitle(Driver.GetBasePageTitle());
        }

        
        [Then(@"I delete all old bookings")]
        public void ThenIDeleteAllOldBookins()
        {
            theBookingPage.deleteAllRows();
        }


        [When(@"I enter ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"",""(.*)"", ""(.*)""")]
        public void WhenIEnter(string firstname, string surname, Decimal price, string deposit, string checkIn_date, string checkOut_date)
        {
            theBookingPage.enterBookingDetails(firstname, surname, price, deposit, checkIn_date, checkOut_date);
            theBookingPage.clickOnSaveButton();
        }

        [Then(@"the displayed values equal ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"",""(.*)"", ""(.*)""")]
        public void ThenTheDisplayedValuesEqual(string firstname, string surname, string price, string deposit, string checkIn_date, string checkOut_date)
        {
            //The rows are not displayed instantly so have to wait. Waiting for number of rows to increment does not seem to work.
            //So try assert again for up to 10 seconds. Break loop if assert passes
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    Assert.IsTrue(theBookingPage.checkDisplayedValues(firstname, surname,price, deposit,
                        checkIn_date,checkOut_date));
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(100);
                }

            }

            //We may have go here simply because loop completed, so assert again
            Assert.IsTrue(theBookingPage.checkDisplayedValues(firstname, surname, price, deposit,
                    checkIn_date, checkOut_date), "Displayed values did not match input values");

        }

        [When(@"I delete row ""(.*)""")]
        public void WhenIDeleteRow(int rowNum)
        {
            //Checks initial number of rows displayed is correct
            Assert.IsTrue(theBookingPage.CheckForInitialNumberOfRowsDisplayed(RequiredInitialNumberOfBookingPageRows));
        
            theBookingPage.deleteARow(rowNum);
        }

        [Then(@"the number of rows displayed is decreased by one")]
        public void ThenTheNumberOfRowsDisplayedIsDecreasedByOne()
        {
            //Expect displayed rows = 3
            Assert.AreEqual(theBookingPage.GetNumberOfRowsDisplayed(), RequiredInitialNumberOfBookingPageRows - 1);           
        }

    }


}

