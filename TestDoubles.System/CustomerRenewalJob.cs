using System;
using System.Collections.Generic;

namespace TestDoubles.System
{
	public interface ITimeService
	{
		DateTime GetDateTime();
	}
	
	public interface ICustomerRepository
	{
		IEnumerable<Customer> GetCustomersExpiringBetween(DateTime startDate, DateTime endDate);
	}

	public class Customer
	{
	}

	public class CustomerRenewalJob
	{
		private readonly ITimeService _timeService;
		private readonly ICustomerRepository _customerRepository;
		private readonly IMailSpool _mailSpool;

		public CustomerRenewalJob(ITimeService timeService, ICustomerRepository customerRepository, IMailSpool mailSpool)
		{
			_timeService = timeService;
			_customerRepository = customerRepository;
			_mailSpool = mailSpool;
		}

		public void Run()
		{
			var startDate = _timeService.GetDateTime();
			var endDate = startDate.AddMonths(1);
			var expiringCustomers = _customerRepository.GetCustomersExpiringBetween(startDate, endDate);
			_mailSpool.SendReminderEmails(expiringCustomers);
		}
	}

}