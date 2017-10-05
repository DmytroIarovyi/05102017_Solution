using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Net;

namespace OurCars
{
  public class Helpers
  {
    /// <summary>
    /// Check that image is valid
    /// </summary>
    /// <param name="imageUri">path to image</param>
    public static void CheckImageValid(string imageUri)
    {
      var request = (HttpWebRequest)WebRequest.Create(imageUri);
      request.Method = "HEAD";

      bool exists;
      try
      {
        request.GetResponse();
        exists = true;
      }
      catch
      {
        exists = false;
      }
      Assert.IsTrue(exists, $"Image not exists or access is denied : {imageUri}");
    }

    /// <summary>
    /// Check that all info (Mileage, Registration etc.) is not empty
    /// </summary>
    /// <param name="table">table with all info</param>
    public static void CheckCorrectDataInTable(IWebElement table)
    {
      var values = table.FindElements(By.TagName("tr"));
      foreach (var value in values)
      {
        var infoLine = value.FindElements(By.TagName("td"));
        Assert.IsTrue(!string.IsNullOrEmpty(infoLine[1].Text),
          $"{infoLine[0]} does not contain any info"); //error in case when value (Mileage, Registration etc) is empty
      }
    }
  }
}
