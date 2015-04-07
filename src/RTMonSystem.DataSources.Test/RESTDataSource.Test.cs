using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RTMonSystem.DataSources.REST;

namespace RTMonSystem.DataSources.Test
{
    [TestClass]
    public class RESTDataSourceTest
    {

        [TestMethod]
        public void GetUrl_Success()
        {
            var target = new RESTDataSource("http://www.google.com");
            Assert.IsTrue(!string.IsNullOrEmpty(target.GetDataAsync().Result));
        }
    }
}
