﻿namespace Profility.Dynamics365.JSONEntities.Tests
{
	using System;
	using System.Linq;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class JsonEntitiesContext_load
	{
		[TestMethod]
		[ExpectedException(typeof(System.IO.DirectoryNotFoundException))]
		public void BadDirectory()
		{
			var ctx = new JsonEntitiesContext(@"\badPath");
			ctx.Load();
		}

		[TestMethod]
		[ExpectedException(typeof(System.IO.DirectoryNotFoundException))]
		public void BadDirectoryFile()
		{
			var ctx = new JsonEntitiesContext(@"\badPath\acc*.json");
			ctx.Load();
		}

		[TestMethod]
		public void TestFiles_SimpleLoad()
		{
			var ctx = new JsonEntitiesContext(@"\TestFiles\SimpleLoad");
			var entities = ctx.Load();

			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}

		[TestMethod]
		public void TestFiles_SimpleLoadOneFile()
		{
			var ctx = new JsonEntitiesContext(@"\TestFiles\SimpleLoad\acc*.json");
			var entities = ctx.Load();

			Assert.AreEqual(2, entities.Count);
		}

		[TestMethod]
		public void TestFiles_Empty()
		{
			var ctx = new JsonEntitiesContext(@"\TestFiles\Empty");
			var entities = ctx.Load();

			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}

		[TestMethod]
		[ExpectedException(typeof(LoadJsonEntityException))]
		public void TestFiles_DuplicateKey()
		{
			var ctx = new JsonEntitiesContext(@"\TestFiles\DuplicateKey");
			ctx.Load();
		}
	}
}
