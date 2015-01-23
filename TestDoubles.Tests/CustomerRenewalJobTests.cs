using System;
using System.Collections.Generic;
using NUnit.Framework;
using TestDoubles.System;

namespace TestDoubles.Tests
{
	[TestFixture]
	public class CustomerRenewalJobTests : ICustomerRepository
	{
		private DateTime _actualStartDate;
		private DateTime _actualEndDate;
		private DateTime _stubbedDateTime = new DateTime(2001, 1, 1);
		private CustomerRenewalJob _customerRenewalJob;
		private FakeTimeService _timeService;

		[SetUp]
		public void SetUp()
		{
			_timeService = new FakeTimeService(_stubbedDateTime);
			_customerRenewalJob = new CustomerRenewalJob(_timeService, this);
		}

		[Test]
		public void Should_get_date_and_time()
		{
			_customerRenewalJob.Run();

			Assert.IsTrue(_timeService.WasCalled);
		}

		[Test]
		public void Should_call_customer_repository_correctly()
		{
			_customerRenewalJob.Run();

			Assert.That(_actualStartDate, Is.EqualTo(_stubbedDateTime));
			Assert.That(_actualEndDate, Is.EqualTo(_stubbedDateTime.AddMonths(1)));
		}

		public IEnumerable<Customer> GetCustomersExpiringBetween(DateTime startDate, DateTime endDate)
		{
			_actualStartDate = startDate;
			_actualEndDate = endDate;
			return new List<Customer>();
		}
	}

	public class FakeTimeService : ITimeService
	{
		private readonly DateTime _stubbedDateTime;

		public FakeTimeService(DateTime dateTime)
		{
			_stubbedDateTime = dateTime;
		}

		public bool WasCalled { get; set; }
		public DateTime GetDateTime()
		{
			WasCalled = true;
			return _stubbedDateTime;
		}
	}


}