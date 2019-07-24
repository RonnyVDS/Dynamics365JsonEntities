﻿namespace Profility.JSONEntities.Tests.Components
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System;

	public class ExpectedError : ExpectedExceptionBaseAttribute
	{
		private Type _expectedExceptionType;
		private string _expectedExceptionMessage;

		public ExpectedError(Type expectedExceptionType)
		{
			_expectedExceptionType = expectedExceptionType;
			_expectedExceptionMessage = string.Empty;
		}

		public ExpectedError(Type expectedExceptionType, string expectedExceptionMessage)
		{
			_expectedExceptionType = expectedExceptionType;
			_expectedExceptionMessage = expectedExceptionMessage;
		}

		protected override void Verify(Exception exception)
		{
			Assert.IsNotNull(exception);

			Assert.IsInstanceOfType(exception, _expectedExceptionType, "Wrong type of exception was thrown.");

			if (!_expectedExceptionMessage.Length.Equals(0))
			{
				Assert.AreEqual(_expectedExceptionMessage, exception.Message, "Wrong exception message was returned.");
			}
		}
	}
}
