namespace Profility.JSONEntities.Tests
{
	using System;
	using System.Linq;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Profility.JSONEntities.Tests.Components;

	[TestClass]
	public class JsonEntitiesContext_load
	{
		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.BadDirectory)]
		public void BadDirectory()
		{
			var ctx = new JsonEntitiesConverter(@".\badPath");
			ctx.Load();
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.BadDirectory)]
		public void BadDirectoryFile()
		{
			var ctx = new JsonEntitiesConverter(@".\badPath\acc*.json");
			var entities = ctx.Load();
		}

		[TestMethod]
		public void TestFiles_SimpleLoad()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\SimpleLoad\");
			var entities = ctx.Load();
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}

		[TestMethod]
		public void TestFiles_SimpleLoadOneFile()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\SimpleLoad\acc*.json");
			var entities = ctx.Load();
			Assert.AreEqual(2, entities.Count);
		}

		[TestMethod]
		public void TestFiles_Empty()
		{
			var path = @".\TestFiles\Empty\";
			if (!System.IO.Directory.Exists(path))
			{
				System.IO.Directory.CreateDirectory(path);
			}

			var ctx = new JsonEntitiesConverter(path);
			var entities = ctx.Load();
			Assert.IsNotNull(entities);
			Assert.IsTrue(!entities.Any());
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.DuplicateKey)]
		public void TestFiles_DuplicateKey()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\DuplicateKey");
			var entities = ctx.Load();
			Assert.IsNotNull(entities);
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.BadFile)]
		public void TestFiles_MissingFile()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\SimpleLoad\acount.json");
			var entities = ctx.Load();
			Assert.IsNotNull(entities);
		}
	}
}