using System;
using System.Collections.Generic;
using System.Text;

namespace Samurai.Domain.EventDelegatesNHandlers
{
	public class WorkEventArgs : EventArgs
	{
		public WorkEventArgs(int hours, string job)
		{
			Hours = hours;
			Job = job ?? throw new ArgumentNullException(nameof(job));
		}

		public int Hours { get; set; }
		public string Job { get; set; }
	}
}
