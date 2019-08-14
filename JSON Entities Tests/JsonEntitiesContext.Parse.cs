namespace Profility.JSONEntities.Tests
{
	using System;
	using System.Linq;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Microsoft.Xrm.Sdk;
	using Profility.JSONEntities.Tests.Components;

	[TestClass]
	public class JsonEntitiesContext_Parse : BaseTests
	{
		[TestMethod]
		public void Parse_PriceIsString()
		{
			var ctx = new JsonEntitiesConverter();
			var entities = ctx.Load(this.ToPath(@"Parse\priceisstring.json"));

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
			var ctx = new JsonEntitiesConverter();
			var entities = ctx.Load(this.ToPath(@"Parse\string.json"));

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
			var ctx = new JsonEntitiesConverter();
			var entities = ctx.Load(this.ToPath(@"Parse\int.json"));

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
			var ctx = new JsonEntitiesConverter();
			var entities = ctx.Load(this.ToPath(@"Parse\decimal.json"));

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
			var ctx = new JsonEntitiesConverter();
			var entities = ctx.Load(this.ToPath(@"Parse\datetime.json"));
			Assert.IsTrue(entities.Any());

			foreach (var entity in entities)
			{
				Assert.IsTrue(entity["datefield"].GetType() == typeof(DateTime));
				var dt = (DateTime)entity["datefield"];
				Assert.IsTrue(dt.Kind == DateTimeKind.Utc);
			}

			var fullDateTime = (DateTime)entities[0]["datefield"];
			Assert.IsTrue(fullDateTime == new DateTime(2000, 02, 01, 21, 22, 23, DateTimeKind.Utc));

			var dateOnly = (DateTime)entities[1]["datefield"];
			Assert.IsTrue(dateOnly == new DateTime(2000, 03, 04, 0, 0, 0, DateTimeKind.Utc));

			var dateOnly2 = (DateTime)entities[2]["datefield"];
			Assert.IsTrue(dateOnly2 == new DateTime(2000, 05, 06, 0, 0, 0, DateTimeKind.Utc));
		}


		[TestMethod]
		public void Parse_Bool()
		{
			var ctx = new JsonEntitiesConverter();
			var entities = ctx.Load(this.ToPath(@"Parse\bool.json"));

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
			var ctx = new JsonEntitiesConverter();
			var entities = ctx.Load(this.ToPath(@"Parse\optionsetvalue.json"));

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
			var ctx = new JsonEntitiesConverter();
			var entities = ctx.Load(this.ToPath(@"Parse\float.json"));

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
			var ctx = new JsonEntitiesConverter();
			var emails = ctx.Load(this.ToPath(@"Parse\entitycollection.json"));

			foreach (var email in emails)
			{
				Assert.IsTrue(email["to"].GetType() == typeof(EntityCollection));
			}
			Assert.IsNotNull(emails);
			Assert.IsTrue(emails.Any());
		}
	}
}