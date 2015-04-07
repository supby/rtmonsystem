using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RTMonSystem.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace RTMonSystem.Workers.Test
{
    class TestDataSource: IDataSource
    {
        public Task<string> GetDataAsync()
        {
            return Task.Run<string>(() =>
                {
                    Thread.Sleep(2000);
                    return "Test result 33";
                });
        }
    }

    class TestObserver: IObserver<string>
    {
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(string value)
        {
            Assert.AreEqual("Test result 33", value);
        }
    }

    [TestClass]
    public class DefaultWorkerTest
    {
        [TestMethod]
        public void DefaultWorker_Def()
        {
            var target = new DefaultWorker(new TestDataSource());

            var ts = new CancellationTokenSource();
            target.Run(ts.Token).Subscribe(new TestObserver());

            Task.Delay(3000).Wait();

            ts.Cancel();
        }
    }
}
