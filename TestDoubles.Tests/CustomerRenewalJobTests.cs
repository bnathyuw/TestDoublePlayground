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
			var customerRenewalJob = new CustomerRenewalJob();

			customerRenewalJob.Run();

			// TODO: assert
		}
	}
}