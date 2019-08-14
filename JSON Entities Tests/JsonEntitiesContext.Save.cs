namespace Profility.JSONEntities.Tests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	[TestClass]
	public class JsonEntitiesContext_Save
	{
		[TestMethod]
		public void Save_SimpleDatatypes()
		{
			var loadPath = @".\TestFiles\Save\savesimpledatatypes-load.json";
			var savepath = @".\TestFiles\Save\savesimpledatatypes-save.json";

			var ctx = new JsonEntitiesConverter(loadPath);
			var humans = ctx.Load();
			var meta = new MetaData()
			{
				LogicalName = "human",
				KeyAttributes = "humanid",
				Attributes = new List<MetaDataAttribute>()
				{
					new MetaDataAttribute() {LogicalName = "fullname", Type = DataTypes.String},
					new MetaDataAttribute() {LogicalName = "age", Type = DataTypes.Int},
					new MetaDataAttribute() {LogicalName = "haircolor", Type = DataTypes.OptionSetValue},
					new MetaDataAttribute() {LogicalName = "heightincm", Type = DataTypes.Decimal},
				}
			};

			ctx.Save(savepath, meta, humans);
		}

		[TestMethod]
		public void Save_DateTime()
		{
			var loadPath = @".\TestFiles\Save\datetime-load.json";
			var savePath = @".\TestFiles\Save\datetime-save.json";
			var meta = new MetaData()
			{
				LogicalName = "human",
				KeyAttributes = "humanid",
				Attributes = new List<MetaDataAttribute>()
				{
					new MetaDataAttribute() {LogicalName = "dateofbirth", Type = DataTypes.DateTime},
				}
			};

			var ctx = new JsonEntitiesConverter(loadPath);
			var humans = ctx.Load();

			ctx.Save(savePath, meta, humans);
		}

		[TestMethod]
		public void Save_EntityReference()
		{
			var loadPath = @".\TestFiles\Save\entityreference-load.json";
			var savePath = @".\TestFiles\Save\entityreference-save.json";
			var meta = new MetaData()
			{
				LogicalName = "human",
				KeyAttributes = "humanid",
				Attributes = new List<MetaDataAttribute>()
				{
					new MetaDataAttribute() {
						LogicalName = "countryid",
						Type = DataTypes.EntityReference,
						LogicalNames = new List<string>(){"country"}
					},
				}
			};

			var ctx = new JsonEntitiesConverter(loadPath);
			var humans = ctx.Load();

			ctx.Save(savePath, meta, humans);
		}

		[TestMethod]
		public void Save_Intersect()
		{
			var loadPath = @".\TestFiles\Save\intersect-load.json";
			var savePath = @".\TestFiles\Save\intersect-save.json";
			var ctx = new JsonEntitiesConverter(loadPath);
			var intersect = ctx.Load();

			var meta = new MetaData()
			{
				LogicalName = "human_pet",
				KeyAttributes = "human_petid",
				Attributes = new List<MetaDataAttribute>()
				{
					new MetaDataAttribute() {LogicalName = "humanid", Type = DataTypes.Guid},
					new MetaDataAttribute() {LogicalName = "petid", Type = DataTypes.Guid},
				},
				EntityType = "intersect"
			};

			ctx.Save(savePath, meta, intersect);
		}

		[TestMethod]
		public void Save_ValidateDataAfterSave()
		{
			var loadPath = @".\TestFiles\Save\validatedataaftersave.json";
			var ctx = new JsonEntitiesConverter(loadPath);
			var humans = ctx.Load();
			var d = new Dictionary<Guid, int>();

			foreach (var human in humans)
			{
				d.Add(human.Id, (int)human["age"] + 1);
				human["age"] = d.First(x => x.Key == human.Id).Value;
			}

			var meta = new MetaData()
			{
				LogicalName = "human",
				KeyAttributes = "humanid",
				Attributes = new List<MetaDataAttribute>()
				{
					new MetaDataAttribute() {LogicalName = "fullname", Type = DataTypes.String},
					new MetaDataAttribute() {LogicalName = "age", Type = DataTypes.Int},
				}
			};

			// Save and reload
			ctx.Save(loadPath, meta, humans);
			humans = ctx.Load();

			foreach (var human in humans)
			{
				Assert.IsTrue((int)human["age"] == d.First(x => x.Key == human.Id).Value);
			}
		}

		[TestMethod]
		public void Save_Entitycollection()
		{
			var loadPath = @".\TestFiles\Save\entitycollection-load.json";
			var savePath = @".\TestFiles\Save\entitycollection-save.json";

			var ctx = new JsonEntitiesConverter(loadPath);
			var emails = ctx.Load();

			var meta = new MetaData()
			{
				LogicalName = "email",
				KeyAttributes = "activityid",
				Attributes = new List<MetaDataAttribute>()
				{
					new MetaDataAttribute() {LogicalName = "mimetype", Type = DataTypes.String},
					new MetaDataAttribute() {
						LogicalName = "to",
						Type = DataTypes.EntityCollection,
						SubMetadata = new MetaData()
						{
								LogicalName = "activityparty",
								Attributes = new List<MetaDataAttribute>()
								{
									new MetaDataAttribute() {LogicalName = "addressused", Type = DataTypes.String},
									new MetaDataAttribute() {LogicalName = "addressusedemailcolumnnumber", Type = DataTypes.Int},
									new MetaDataAttribute() {LogicalName = "donotfax", Type = DataTypes.Bool},
								}
						}
					},
				}
			};

			ctx.Save(savePath, meta, emails);
		}
	}
}
