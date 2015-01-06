using NUnit.Framework;
using TestDoubles.System;

namespace TestDoubles.Tests
{
	[TestFixture]
	public class CustomerRenewalJobTests
	{
		[Test]
		public void Foo()
		{
			var timeService = new FakeTimeService();
			var customerRenewalJob = new CustomerRenewalJob(timeService);

			customerRenewalJob.Run();

			Assert.IsTrue(timeService.WasCalled);
		}
	}

	public class FakeTimeService : CustomerRenewalJob.ITimeService
	{
		public bool WasCalled
		{
			get { throw new global::System.NotImplementedException(); }
		}
	}


}