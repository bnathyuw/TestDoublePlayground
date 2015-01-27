using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using TestDoubles.System;

namespace TestDoubles.Tests
{
	[TestFixture]
	public class CustomerRenewalJobTests
	{
		private DateTime _stubbedDateTime = new DateTime(2001, 1, 1);
		private CustomerRenewalJob _customerRenewalJob;
		private IMailSpool _mailSpool;
		private IEnumerable<Customer> _stubbedCustomers;
	    private ITimeService _timeService;
	    private ICustomerRepository _customerRepository;

	    [SetUp]
		public void SetUp()
		{
		    _timeService = MockRepository.GenerateStub<ITimeService>();
	        _timeService.Stub(ts => ts.GetDateTime()).Return(_stubbedDateTime);

	        _customerRepository = MockRepository.GenerateStub<ICustomerRepository>();
	        _stubbedCustomers = new List<Customer>{new Customer()};
	        _customerRepository.Stub(cr => cr.GetCustomersExpiringBetween(_stubbedDateTime, _stubbedDateTime.AddMonths(1)))
	                           .Return(_stubbedCustomers);

	        _mailSpool = MockRepository.GenerateStub<IMailSpool>();

	        _customerRenewalJob = new CustomerRenewalJob(_timeService, _customerRepository, _mailSpool);

			_customerRenewalJob.Run();
		}

		[Test]
		public void Should_get_date_and_time()
		{
            _timeService.AssertWasCalled(ts => ts.GetDateTime());
		}

		[Test]
		public void Should_call_customer_repository_correctly()
		{
		    _customerRepository.AssertWasCalled(cr => cr.GetCustomersExpiringBetween(_stubbedDateTime, _stubbedDateTime.AddMonths(1)));
		}

		[Test]
		public void Should_call_mail_spool_correctly()
		{
			_mailSpool.AssertWasCalled(ms => ms.SendReminderEmails(_stubbedCustomers));
		}
	}
}