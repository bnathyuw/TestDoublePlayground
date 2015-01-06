using System;

namespace TestDoubles.System
{
	public class CustomerRenewalJob
	{
		public interface ITimeService
		{
		}

		public CustomerRenewalJob(ITimeService timeService)
		{
			throw new NotImplementedException();
		}

		public void Run()
		{
			throw new NotImplementedException();
		}
	}
}