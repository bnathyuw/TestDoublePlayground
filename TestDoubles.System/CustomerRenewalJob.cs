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

		public CustomerRenewalJob(ITimeService timeService, ICustomerRepository customerRepository)
		{
			_timeService = timeService;
			_customerRepository = customerRepository;
		}

		public void Run()
		{
			var startDate = _timeService.GetDateTime();
			var endDate = startDate.AddMonths(1);
			_customerRepository.GetCustomersExpiringBetween(startDate, endDate);
		}
	}

}