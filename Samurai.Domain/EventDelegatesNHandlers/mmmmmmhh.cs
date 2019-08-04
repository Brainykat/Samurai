using System;

namespace Samurai.Domain.EventDelegatesNHandlers
{
	public class mmmmmmhh
	{
		#region Delegates
		//Delegate
		public delegate void SitakiWewe(int x, string steph);
		public string OurThing() 
		{
			//Hook up the two
			//Handler passed in the constructor
			SitakiWewe sitakiWewe = new SitakiWewe(Fanya_SitakiWewe);
			//Invoke the delegate
			sitakiWewe(5, "Nakaa peke yangu");
			return null;
		}
		//Handler
		static void Fanya_SitakiWewe(int y, string nini)
		{
			for (int i = y; i > 0; y--)
			{
				string Nimefanya = $"{nini} {i.ToString()}";
			}
		}
		#endregion
		#region Events delegate way
		public delegate int WorkPerformedHandler(int hours, string work);
		public event WorkPerformedHandler WorkPerformed;
		/// <summary>
		/// EventHandler is a built in Delegate
		/// </summary>
		public event EventHandler WorkCompleted;
		public void DoWork(int hours, string work)
		{
			for (int i = 0; i < hours; i++)
			{
				OnWorkPerformed(i, work);
			}
			OnWorkCompleted();
		}

		protected virtual void OnWorkPerformed(int hour, string work)
		{
			//Raise event
			//Check whether null
			WorkPerformed?.Invoke(hour, work);
		}
		protected virtual void OnWorkCompleted()
		{
			if (WorkCompleted != null)
			{
				WorkCompleted(this, EventArgs.Empty);
			}
		}
		#endregion
		#region Event The Excellent Way
		public event EventHandler<WorkEventArgs> JobPerformed;
		public event EventHandler JobDone;
		public void FanyaKazi(int hours, string work)
		{
			for (int i = 0; i < hours; i++)
			{
				OnJobPerformed(i, work);
			}
			OnJobDone();
		}


		protected virtual void OnJobPerformed(int hours, string work)
		{
			var del = JobPerformed as EventHandler<WorkEventArgs>;
			if (del != null)
			{
				del(this, new WorkEventArgs(hours, work));
			}
		}
		protected virtual void OnJobDone() => JobDone?.Invoke(this, EventArgs.Empty);
		#endregion

	}
}
