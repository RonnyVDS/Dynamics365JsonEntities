namespace Profility.Dynamics365.JSONEntities
{
	using System;
	using System.Runtime.Serialization;

	[Serializable]
	public class LoadJsonEntityException : Exception
	{
		public LoadJsonEntityException()
		{
		}

		public LoadJsonEntityException(string message)
			: base(message)
		{
		}

		public LoadJsonEntityException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
