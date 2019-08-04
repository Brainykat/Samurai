using System;
using System.Collections.Generic;
using System.Text;

namespace Samurai.Domain.EventDelegatesNHandlers
{
	class Consume
	{
		void chukua()
		{
			var mmh = new mmmmmmhh();
			//Wire up
			//mmh.JobPerformed += new EventHandler<WorkEventArgs>(JobBeingPerformed);
			//mmh.JobPerformed += delegate (object sender, WorkEventArgs e) { /* Anonymous/Nested method (Cant be re-used)*/ };
			mmh.JobPerformed += (s,e) => new NotImplementedException();
			mmh.JobPerformed += Mmh_JobPerformed;
			//mmh.JobDone += new EventHandler(JobIsDone);
			mmh.JobDone += Mmh_JobDone;
			//trigger
			mmh.FanyaKazi(5,"Soma");
		}

		private void Mmh_JobPerformed(object sender, WorkEventArgs e) => throw new NotImplementedException();

		private void Mmh_JobDone(object sender, EventArgs e) => throw new NotImplementedException();
		#region Handle Events
		public void JobBeingPerformed(object sender, WorkEventArgs e)
		{
			//Do your thing umemabiwa inafanyika
		}
		public void JobIsDone(object sender, EventArgs e)
		{
			//Do your thing umemabiwa imefanyika
		}

		#endregion
	}
}
