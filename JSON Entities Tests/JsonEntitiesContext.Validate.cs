namespace Profility.JSONEntities.Tests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Profility.JSONEntities.Tests.Components;
	using System;
	using System.Linq;

	[TestClass]
	public class JsonEntitiesContext_Validate : BaseTests
	{
		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.InvalidMetaData)]
		public void ForCorrectMetaData()
		{
			var jsonEntitiesConvertor = new JsonEntitiesConverter();
			jsonEntitiesConvertor.Load(this.ToPath(@"Validate\forcorrectmetadata.json"));
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.DuplicateKey)]
		public void ForDuplicateIds()
		{
			var jsonEntitiesConvertor = new JsonEntitiesConverter();
			jsonEntitiesConvertor.Load(this.ToPath(@"Validate\forduplicateids.json"));
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.IntersectNotInRightFormat)]
		public void ForIntersectData_ForgotEntityTypeInMetaData()
		{
			var jsonEntitiesConvertor = new JsonEntitiesConverter();
			var intersect = jsonEntitiesConvertor.Load(this.ToPath(@"Validate\forintersectdata1.json"));
		}

		[TestMethod]
		public void ForIntersectData_AllowToHaveNoEntities()
		{
			var jsonEntitiesConvertor = new JsonEntitiesConverter();
			var intersect = jsonEntitiesConvertor.Load(this.ToPath(@"Validate\forintersectdata2.json"));
			Assert.IsTrue(!intersect.Any());
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.IntersectNotInRightFormat)]
		public void ForIntersectData_MoreThan2AttributesInMetaData()
		{
			var jsonEntitiesConvertor = new JsonEntitiesConverter();
			var intersect = jsonEntitiesConvertor.Load(this.ToPath(@"Validate\forintersectdata3.json"));
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.IntersectNotInRightFormat)]
		public void ForIntersectData_EntityAttributeNotOfTypeGuid()
		{
			var jsonEntitiesConvertor = new JsonEntitiesConverter();
			var intersect = jsonEntitiesConvertor.Load(this.ToPath(@"Validate\forintersectdata4.json"));
		}

		[TestMethod]
		public void ForIntersectData_CheckForGuidtype()
		{
			var jsonEntitiesConvertor = new JsonEntitiesConverter();
			var intersect = jsonEntitiesConvertor.Load(this.ToPath(@"Validate\forintersectdata5.json"));

			foreach (var i in intersect)
			{
				Assert.IsTrue(i.Id.GetType() == typeof(Guid));
				Assert.IsTrue(i["humanid"].GetType() == typeof(Guid));
				Assert.IsTrue(i["petid"].GetType() == typeof(Guid));			
			}
		}
	}
}