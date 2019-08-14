namespace Profility.JSONEntities.Tests
{
	using System;

	public abstract class BaseTests
	{
		protected string BasePath { get; private set; }

		public BaseTests()
		{
			this.BasePath = @".\..\..\TestFiles\";
		}

		protected string ToPath(string file)
		{
			if (string.IsNullOrEmpty(file)) throw new ArgumentNullException("File cannot be null or empty.");
			if (file.StartsWith(@"\")) file = file.Substring(1);
			return this.BasePath + file;

		}
	}
}