using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using EE_Test_Project.Utilities;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;


namespace EE_Test_Project.Pages
{
    public class BookingPage: BasePage
    {
        //Element Locators
        private readonly By firstnameInputFieldLocator = By.Id("firstname");
        private readonly By surnameInputFieldLocator = By.Id("lastname");
        private readonly By priceInputFieldLocator = By.Id("totalprice");
        private readonly By depositFieldLocator = By.Id("depositpaid");
        private readonly By checkInFieldLocator = By.Id("checkin"); 
        private readonly By checkOutFieldLocator = By.Id("checkout"); 
        private readonly By saveButtonLocator = By.CssSelector("[type='button'][value=' Save ']");
        private readonly By deleteButtonsLocator = By.CssSelector("[type='button'][value='Delete']");
      
        //Methods
        public void GoToBookingPage()
        {
            Driver.NavigateTo(Driver.BaseUrl);    
        }
    

        public void enterBookingDetails(string firstname, string surname, decimal price, string deposit, string checkInDate, string checkOutDate)
        {
            EnterText(firstnameInputFieldLocator, firstname);
            EnterText(surnameInputFieldLocator, surname);
            EnterText(priceInputFieldLocator, price.ToString());

            IWebElement depositField = GetVisibleElementByLocator(depositFieldLocator);
            SelectElement select = new SelectElement(depositField);
            select.SelectByText(deposit);

            EnterText(checkInFieldLocator, checkInDate);
            EnterText(checkOutFieldLocator, checkOutDate);

        }


        public void clickOnSaveButton()
        {
            ClickOnElement(saveButtonLocator);
        }

        public void deleteARow(int rowToDelete)
        {

            var deleteButtons = GetAllVisibleElements(deleteButtonsLocator);

            if (rowToDelete <= deleteButtons.Count && rowToDelete > 0)
            {
                //deleteButtons is zero referenced
                deleteButtons[rowToDelete - 1].Click();
            }

        }

        public void deleteAllRows()
        {
            while (IsElementDisplayed(deleteButtonsLocator))
            {
                try
                {
                    GetClickableElement(deleteButtonsLocator).Click();
                }
                catch (Exception)
                {

                    //Exception should be caught in IsElementDisplayed(), but sometimes
                    //it does not. I expect that after delete is pressed for the last one, it 
                    //is still present for a moment and the loop continues.
                }
                
            }
        }

        public bool CheckForInitialNumberOfRowsDisplayed(int requiredRows)
        {
            //Need start of test to be in a predicable state. So check we have the expecteded number of rows
            //I tried using javascript executor to check for page load to finish , but it did not work

            //The rows are not displayed instantly so have to wait. 
            //So try assert again for up to 10 seconds. Break loop if assert passes
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    Assert.AreEqual(requiredRows, GetAllVisibleElements(By.ClassName("row")).Count);
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(100);
                }
            }

               
            return GetAllVisibleElements(By.ClassName("row")).Count.Equals(requiredRows);
        }

        public int GetNumberOfRowsDisplayed()
        { 
            return GetAllVisibleElements(By.ClassName("row")).Count;
        }
                        
        // Gets the elements in the last data row in the table
        // The last line is the input and childElements is zero referenced, so number of data rows is the value - 2
        public ReadOnlyCollection<IWebElement> GetLastDataRowElements()
        {
            var dataRows = GetAllVisibleElements(By.ClassName("row"));
            var index = dataRows.Count - 2;
            var lastRow = dataRows[index];

            //Now get the child elements
            var childElements = lastRow.FindElements(By.XPath(".//*"));
            return childElements;

        }

        public bool checkDisplayedValues(string expectedFirstname, string expectedSurname, string expectedPrice,
            string expectedDeposit, string expectedCheckinDate, string expectedCheckoutDate)
        {
            var lastRowElements = GetLastDataRowElements();
            string displayedFirstname = lastRowElements[1].Text; ;
            string displayedSurname = lastRowElements[3].Text; ;
            string displayedPrice = lastRowElements[5].Text; ;
            string displayedDeposit = lastRowElements[7].Text;
            string displayedCheckinDate = lastRowElements[9].Text;
            string displayedCheckoutDate = lastRowElements[11].Text;

            if (expectedFirstname.Equals(displayedFirstname) &&
                expectedSurname.Equals(displayedSurname) &&
                expectedPrice.Equals(displayedPrice) &&
                expectedDeposit.Equals(displayedDeposit) &&
                expectedCheckinDate.Equals(displayedCheckinDate) &&
                expectedCheckoutDate.Equals(displayedCheckoutDate)
                )
            {
                return true;
            }

            return false;
        }

    }
}
