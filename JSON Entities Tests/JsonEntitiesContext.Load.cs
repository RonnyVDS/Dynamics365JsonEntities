namespace Profility.JSONEntities.Tests
{
	using System.Linq;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Profility.JSONEntities.Tests.Components;

	[TestClass]
	public class JsonEntitiesContext_load
	{
		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.BadDirectory)]
		public void Load_BadDirectory()
		{
			var ctx = new JsonEntitiesConverter(@".TestFiles\Load\badPath");
			ctx.Load();
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.BadDirectory)]
		public void Load_BadDirectoryFile()
		{
			var ctx = new JsonEntitiesConverter(@".TestFiles\Load\badPath\acc*.json");
			var entities = ctx.Load();
		}

		[TestMethod]
		public void Load_SimpleLoad()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Load\NotEmpty");
			var entities = ctx.Load();
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}

		[TestMethod]
		public void Load_SimpleLoadOneFile()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Load\acc*.json");
			var entities = ctx.Load();
			Assert.AreEqual(2, entities.Count);
		}

		[TestMethod]
		public void Load_Empty()
		{
			var path = @".\TestFiles\Load\Empty\";
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
		public void Load_DuplicateKey()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Load\duplicateKey.json");
			var entities = ctx.Load();
			Assert.IsNotNull(entities);
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.BadFile)]
		public void Load_MissingFile()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Load\acount.json");
			var entities = ctx.Load();
			Assert.IsNotNull(entities);
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.IDNotDefined)]
		public void Load_IdNotDefined()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Load\idnotdefined.json");
			var entities = ctx.Load();
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.FieldNameNotInMetaData)]
		public void Load_FieldNameNotInMetaData()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Load\fieldnamenotinmetadata.json");
			ctx.Load();
		}

		[TestMethod]
		public void Load_DuplicateFieldName()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Load\duplicatefieldname.json");
			var entities = ctx.Load();
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}
	}
}