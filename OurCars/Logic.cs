using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace OurCars
{
  public class Logic
  {
    /// <summary>
    /// Run test
    /// </summary>
    /// <param name="driver">WebDriver Chrome/FireFox/InternetExplorer etc.</param>
    /// <param name="carName">Car name</param>
    /// <param name="uri">web page to use</param>
    public static void RunTest(IWebDriver driver, string carName, string uri = "https://auto1.com/en/our-cars")
    {
      // 1. Open url
      driver.Navigate().GoToUrl(uri);
      driver.Manage().Window.Maximize();

      try
      {
        // 2. Filter by manufacture by clicking checkbox(BMW)
        var checkboxList = driver.FindElements(By.ClassName("checkbox"));
        foreach (var checkbox in checkboxList)
        {
          var liElement = checkbox.FindElement(By.XPath(".."));
          if (liElement.Text == carName)
          {
            liElement.Click();
            break;
          }
        }

        // 3. Verify filter was selected
        var selectBoxInfo = driver.FindElement(By.ClassName("select2"));
        Assert.AreEqual($"{carName}", selectBoxInfo.Text);

        //wait until page will load
        var loadingTicker = driver.FindElement(By.ClassName("loading-ticker"));
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("loading-ticker")));

        var carItems = driver.FindElements(By.ClassName("car-item"));
        foreach (var car in carItems)
        {
          // 4.Verify all cars are BMW’s on the page
          var name = car.FindElement(By.ClassName("car-name"));
          Assert.IsTrue(name.Text.Contains($"{carName}"), $"Name of car is wrong : {name.Text}");

          // 5.Verify each car has picture
          var image = car.FindElement(By.ClassName("car-img"));
          var imagePath = image.FindElement(By.XPath(".//img")).GetAttribute("src");
          Helpers.CheckImageValid(imagePath);

          // 6. Verify each car has complete information (Mileage, Registration is not empty etc.)
          var table = car.FindElement(By.TagName("table"));
          Helpers.CheckCorrectDataInTable(table);
        }
      }
      catch (Exception e)
      {
        Assert.Fail($"Test Failed : {e.Message}");
      }
      finally
      {
        driver.Close();
        driver.Quit();
        driver.Dispose();
      }
    }
  }
}
