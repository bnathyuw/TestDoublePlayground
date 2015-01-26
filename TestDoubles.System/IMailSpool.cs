using System.Collections.Generic;

namespace TestDoubles.System
{
	public interface IMailSpool
	{
		void SendReminderEmails(IEnumerable<Customer> stubbedCustomers);
	}
}