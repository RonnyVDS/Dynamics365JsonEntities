namespace Profility.JSONEntities.Tests
{
	using System.Linq;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Profility.JSONEntities.Tests.Components;

	[TestClass]
	public class JsonEntitiesContext_load : BaseTests
	{
		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.BadDirectory)]
		public void Load_BadDirectory()
		{
			var ctx = new JsonEntitiesConverter();
			ctx.Load(this.ToPath(@"Load\badPath"));
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.BadDirectory)]
		public void Load_BadDirectoryFile()
		{
			var ctx = new JsonEntitiesConverter();
			var entities = ctx.Load(this.ToPath(@"Load\badPath\acc*.json"));
		}

		[TestMethod]
		public void Load_SimpleLoad()
		{
			var ctx = new JsonEntitiesConverter();
			var entities = ctx.Load(this.ToPath(@"Load\NotEmpty"));
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}

		[TestMethod]
		public void Load_SimpleLoadOneFile()
		{
			var ctx = new JsonEntitiesConverter();
			var entities = ctx.Load(this.ToPath(@"Load\acc*.json"));
			Assert.AreEqual(2, entities.Count);
		}

		[TestMethod]
		public void Load_Empty()
		{
			var path = this.ToPath(@"Load\Empty\");
			if (!System.IO.Directory.Exists(path))
			{
				System.IO.Directory.CreateDirectory(path);
			}

			var ctx = new JsonEntitiesConverter();
			var entities = ctx.Load(path);
			Assert.IsNotNull(entities);
			Assert.IsTrue(!entities.Any());
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.DuplicateKey)]
		public void Load_DuplicateKey()
		{
			var ctx = new JsonEntitiesConverter();
			ctx.Load(this.ToPath(@"Load\duplicateKey.json"));
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.BadFile)]
		public void Load_MissingFile()
		{
			var ctx = new JsonEntitiesConverter();
			ctx.Load(this.ToPath(@"Load\notexising.json"));
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.IDNotDefined)]
		public void Load_IdNotDefined()
		{
			var ctx = new JsonEntitiesConverter();
			ctx.Load(this.ToPath(@"Load\idnotdefined.json"));
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.FieldNameNotInMetaData)]
		public void Load_FieldNameNotInMetaData()
		{
			var ctx = new JsonEntitiesConverter();
			ctx.Load(this.ToPath(@"Load\fieldnamenotinmetadata.json"));
		}

		[TestMethod]
		public void Load_DuplicateFieldName()
		{
			var ctx = new JsonEntitiesConverter();
			var entities = ctx.Load(this.ToPath(@"Load\duplicatefieldname.json"));
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}
	}
}