namespace Profility.JSONEntities.Tests
{
	using System;
	using System.Linq;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Profility.JSONEntities.Tests.Components;

	[TestClass]
	public class JsonEntitiesContext_Parse
	{
		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.FieldNameNotInMetaData)]
		public void TestFiles_UndefinedField()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Parse\vehicles.json");
			var entities = ctx.Load();
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.IDNotDefined)]
		public void TestFiles_IDNotDefined()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Parse\carsWithMissingID.json");
			var entities = ctx.Load();
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}


		[TestMethod]
		public void TestFiles_PriceIsString()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Parse\carsWherePriceIsString.json");
			var entities = ctx.Load();

			foreach (var entity in entities)
			{
				Assert.IsTrue(entity["price"].GetType() == typeof(int));
			}
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}
		//[TestMethod]
		//[ExpectedError(typeof(LoadJsonEntityException), JsonEntityException.DuplicateFieldName)]
		//public void TestFiles_DuplicateFieldName()
		//{
		//	var ctx = new JsonEntitiesContext(@".\TestFiles\Parse\bikes.json");
		//	var entities = ctx.Load();
		//	Assert.IsNotNull(entities);
		//	Assert.IsTrue(entities.Any());
		//}

		[TestMethod]
		public void TestFiles_ParsingAllTypes()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Parse\Contracts.json");

			//var ctx = new JsonEntitiesContext(@"C:\Users\YentheRossel\Source\Repos\Dynamics365_JSON_Entities\JSON Entities Tests\TestFiles\Parse\contracts.json");
			var entities = ctx.Load();

			

			Assert.IsNotNull(entities);

			Assert.IsTrue(entities.Any());
		}
	}
}