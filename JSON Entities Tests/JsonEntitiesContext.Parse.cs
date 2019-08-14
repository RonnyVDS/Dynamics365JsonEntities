namespace Profility.JSONEntities.Tests
{
	using System;
	using System.Linq;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Microsoft.Xrm.Sdk;
	using Profility.JSONEntities.Tests.Components;

	[TestClass]
	public class JsonEntitiesContext_Parse
	{
		[TestMethod]
		public void Parse_PriceIsString()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Parse\priceisstring.json");
			var entities = ctx.Load();

			foreach (var entity in entities)
			{
				Assert.IsTrue(entity["price"].GetType() == typeof(int));
			}
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}
	
		[TestMethod]
		public void Parse_String()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Parse\string.json");
			var entities = ctx.Load();

			foreach (var entity in entities)
			{
				Assert.IsTrue(entity["fullname"].GetType() == typeof(string));
			}
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}

		[TestMethod]
		public void Parse_Int()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Parse\int.json");
			var entities = ctx.Load();

			foreach (var entity in entities)
			{
				Assert.IsTrue(entity["age"].GetType() == typeof(int));
			}
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}

		[TestMethod]
		public void Parse_Decimal()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Parse\decimal.json");
			var entities = ctx.Load();

			foreach (var entity in entities)
			{
				Assert.IsTrue(entity["height"].GetType() == typeof(decimal));
			}
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}

		[TestMethod]
		public void Parse_Datetime()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Parse\datetime.json");
			var entities = ctx.Load();

			foreach (var entity in entities)
			{
				Assert.IsTrue(entity["dateofbirth"].GetType() == typeof(DateTime));
			}
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}


		[TestMethod]
		public void Parse_Bool()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Parse\bool.json");
			var entities = ctx.Load();

			foreach (var entity in entities)
			{
				Assert.IsTrue(entity["ismarried"].GetType() == typeof(bool));
			}
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}

		[TestMethod]
		public void Parse_OptionSetValue()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Parse\optionsetvalue.json");
			var entities = ctx.Load();

			foreach (var entity in entities)
			{
				Assert.IsTrue(entity["gender"].GetType() == typeof(OptionSetValue));
			}
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}

		[TestMethod]
		public void Parse_Float()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Parse\float.json");
			var entities = ctx.Load();

			foreach (var entity in entities)
			{
				Assert.IsTrue(entity["floatnumber"].GetType() == typeof(float));
			}
			Assert.IsNotNull(entities);
			Assert.IsTrue(entities.Any());
		}

		[TestMethod]
		public void Parse_EntityCollection()
		{
			var ctx = new JsonEntitiesConverter(@".\TestFiles\Parse\entitycollection.json");
			var emails = ctx.Load();

			foreach (var email in emails)
			{
				Assert.IsTrue(email["to"].GetType() == typeof(EntityCollection));
			}
			Assert.IsNotNull(emails);
			Assert.IsTrue(emails.Any());
		}
	}
}