using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
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
		private IMailSpool _mailSpool;
		private IEnumerable<Customer> _stubbedCustomers;

		[SetUp]
		public void SetUp()
		{
			_timeService = new FakeTimeService(_stubbedDateTime);
			_mailSpool = MockRepository.GenerateStub<IMailSpool>();
			_stubbedCustomers = new List<Customer>{new Customer()};
			_customerRenewalJob = new CustomerRenewalJob(_timeService, this, _mailSpool);

			_customerRenewalJob.Run();
		}

		[Test]
		public void Should_get_date_and_time()
		{
			Assert.IsTrue(_timeService.WasCalled);
		}

		[Test]
		public void Should_call_customer_repository_correctly()
		{
			Assert.That(_actualStartDate, Is.EqualTo(_stubbedDateTime));
			Assert.That(_actualEndDate, Is.EqualTo(_stubbedDateTime.AddMonths(1)));
		}

		[Test]
		public void Should_call_mail_spool_correctly()
		{
			_mailSpool.AssertWasCalled(ms => ms.SendReminderEmails(_stubbedCustomers));
		}

		public IEnumerable<Customer> GetCustomersExpiringBetween(DateTime startDate, DateTime endDate)
		{
			_actualStartDate = startDate;
			_actualEndDate = endDate;
			return _stubbedCustomers;
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