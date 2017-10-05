using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace OurCars
{
  [TestClass]
  public class Tests
  {
    static string carName = "BMW";

    [TestMethod]
    public void ChromeTest()
    {
      IWebDriver chromeDriver = new ChromeDriver(Environment.CurrentDirectory);

      Logic.RunTest(chromeDriver, carName);
    }

    [TestMethod]
    public void FireFoxTest()
    {
      IWebDriver fireFoxDriver = new FirefoxDriver();

      Logic.RunTest(fireFoxDriver, carName);
    }
  }
}
