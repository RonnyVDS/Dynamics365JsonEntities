namespace Profility.JSONEntities.Tests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Profility.JSONEntities.Tests.Components;
	using System;
	using System.Linq;

	[TestClass]
	public class JsonEntitiesContext_Validate
	{
		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.InvalidMetaData)]
		public void ForCorrectMetaData()
		{
			var jsonEntitiesConvertor = new JsonEntitiesConverter(@".\TestFiles\Validate\forcorrectmetadata.json");
			jsonEntitiesConvertor.Load();
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.DuplicateKey)]
		public void ForDuplicateIds()
		{
			var jsonEntitiesConvertor = new JsonEntitiesConverter(@".\TestFiles\Validate\forduplicateids.json");
			jsonEntitiesConvertor.Load();
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.IntersectNotInRightFormat)]
		public void ForIntersectData_ForgotEntityTypeInMetaData()
		{
			var jsonEntitiesConvertor = new JsonEntitiesConverter(@".\TestFiles\Validate\forintersectdata1.json");
			var intersect = jsonEntitiesConvertor.Load();
		}

		[TestMethod]
		public void ForIntersectData_AllowToHaveNoEntities()
		{
			var jsonEntitiesConvertor = new JsonEntitiesConverter(@".\TestFiles\Validate\forintersectdata2.json");
			var intersect = jsonEntitiesConvertor.Load();
			Assert.IsTrue(!intersect.Any());
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.IntersectNotInRightFormat)]
		public void ForIntersectData_MoreThan2AttributesInMetaData()
		{
			var jsonEntitiesConvertor = new JsonEntitiesConverter(@".\TestFiles\Validate\forintersectdata3.json");
			var intersect = jsonEntitiesConvertor.Load();
		}

		[TestMethod]
		[ExpectedError(typeof(LoadJsonEntityException), LoadJsonEntityException.IntersectNotInRightFormat)]
		public void ForIntersectData_EntityAttributeNotOfTypeGuid()
		{
			var jsonEntitiesConvertor = new JsonEntitiesConverter(@".\TestFiles\Validate\forintersectdata4.json");
			var intersect = jsonEntitiesConvertor.Load();
		}

		[TestMethod]
		public void ForIntersectData_CheckForGuidtype()
		{
			var jsonEntitiesConvertor = new JsonEntitiesConverter(@".\TestFiles\Validate\forintersectdata5.json");
			var intersect = jsonEntitiesConvertor.Load();

			foreach (var i in intersect)
			{
				Assert.IsTrue(i.Id.GetType() == typeof(Guid));
				Assert.IsTrue(i["humanid"].GetType() == typeof(Guid));
				Assert.IsTrue(i["petid"].GetType() == typeof(Guid));			
			}
		}
	}
}