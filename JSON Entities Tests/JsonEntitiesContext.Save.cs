namespace Profility.JSONEntities.Tests
{
	using System;
	using System.IO;
	using System.Collections.Generic;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Microsoft.Xrm.Sdk;

	[TestClass]
	public class JsonEntitiesContext_Save : BaseTests
	{
		[TestMethod]
		public void Save_JsonValidation()
		{
			/*
			 * Metadata
			 */
			var meta = new MetaData()
			{
				LogicalName = "entityLogicalName",
				KeyAttributes = "entityLogicalNameid",
				Attributes = new List<MetaDataAttribute>()
				{
					new MetaDataAttribute("nullField", DataTypes.String),
					new MetaDataAttribute("stringField", DataTypes.String),
					new MetaDataAttribute("intField", DataTypes.Int),
					new MetaDataAttribute("optionsetvalueField", DataTypes.OptionSetValue),
					new MetaDataAttribute("decimalField", DataTypes.Decimal),
					new MetaDataAttribute("dateField", DataTypes.DateTime),
					new MetaDataAttribute("guidField", DataTypes.Guid),
					new MetaDataAttribute("entityReferenceField", DataTypes.EntityReference) {
						LogicalNames = new List<string>(){"otherEntityName"}
					}
				}
			};


			/*
			 * Data
			 */
			var entities = new List<Entity>();
			var e1 = new Entity("entityLogicalName", new Guid("{1293da55-897e-4aa6-9920-5a2ce7708e83}"));
			e1["nullField"] = null;
			e1["stringField"] = "testString";
			e1["intField"] = 5;
			e1["optionsetvalueField"] = new OptionSetValue(6);
			e1["decimalField"] = 5.5m;
			e1["dateField"] = new DateTime(2000, 02, 01, 20, 21, 22);
			e1["guidField"] = new Guid("{F8B7655D-E0D7-407E-BAEA-6F943B56DB92}");
			e1["entityReferenceField"] = new EntityReference("otherEntityName", new Guid("{47BC8104-600F-4C36-87E7-D7303D267EA0}"));
			e1["fieldNotInMetadata"] = "fieldNotInMetadataValue";
			entities.Add(e1);

			/*
			 * Execute
			 */
			var ctx = new JsonEntitiesConverter(new JsonEntitiesConverterSettings() {
				IndentJson = false,
				QuoteChar = '\''
			});
			var json = ctx.ToJson(meta, entities);

			/*
			 * Validate
			 */
			// Metadata
			Assert.IsTrue(json.Contains("{'logicalName':'nullField','type':'string'}"));
			Assert.IsTrue(json.Contains("{'logicalName':'stringField','type':'string'}"));
			Assert.IsTrue(json.Contains("{'logicalName':'intField','type':'int'}"));
			Assert.IsTrue(json.Contains("{'logicalName':'optionsetvalueField','type':'optionsetvalue'}"));
			Assert.IsTrue(json.Contains("{'logicalName':'decimalField','type':'decimal'}"));
			Assert.IsTrue(json.Contains("{'logicalName':'dateField','type':'datetime'}"));
			Assert.IsTrue(json.Contains("{'logicalName':'guidField','type':'guid'}"));
			Assert.IsTrue(json.Contains("{'logicalName':'entityReferenceField','type':'entityreference','logicalNames':['otherEntityName']}"));
			Assert.IsFalse(json.Contains("fieldNotInMetadata"));

			// Data
			 Assert.IsTrue(json.Contains("'nullField':null"));
			Assert.IsTrue(json.Contains("'stringField':'testString'"));
			Assert.IsTrue(json.Contains("'intField':5"));
			Assert.IsTrue(json.Contains("'optionsetvalueField':6"));
			Assert.IsTrue(json.Contains("'decimalField':5.5"));
			Assert.IsTrue(json.Contains("'dateField':'2000-02-01 20:21:22'"));
			Assert.IsTrue(json.Contains("'guidField':'{f8b7655d-e0d7-407e-baea-6f943b56db92}'"));
			Assert.IsTrue(json.Contains("'entityReferenceField':{'id':'{47bc8104-600f-4c36-87e7-d7303d267ea0}','logicalName':'otherEntityName'}"));
		}

		
		[TestMethod]
		public void Save_Entitycollection()
		{
			var loadPath = this.ToPath(@"Save\entitycollection-load.json");
			var savePath = this.ToPath(@"Save\entitycollection-expected.json");
			var meta = GetMetaDataForEntityCollection();

			var ctx = new JsonEntitiesConverter();
			var emails = ctx.Load(loadPath);
			ctx.Save(savePath, meta, emails);

			var txt_OnRead = RemoveWhiteSpacesAndTabs(ReadFile(loadPath));
			var txt_OnSave = RemoveWhiteSpacesAndTabs(ReadFile(savePath));

			Assert.IsTrue(txt_OnRead == txt_OnSave);
		}

		#region Helpers

		[TestInitialize]
		public void TestInitialize()
		{
			if (!Directory.Exists(this.ToPath(@"SaveTemporary")))
			{
				Directory.CreateDirectory(this.ToPath(@"SaveTemporary"));
			}
		}

		[TestCleanup]
		public void TestCleanup()
		{
			if (Directory.Exists(this.ToPath(@"SaveTemporary")))
			{
				Directory.Delete(this.ToPath(@"SaveTemporary"), true);
			}
		}

		private static string ReadFile(string path)
		{
			using (var r = new StreamReader(path))
			{
				return r.ReadToEnd();
			}
		}

		private static string RemoveWhiteSpacesAndTabs(string txt)
		{
			return txt.Replace('\u0009'.ToString(), "").Replace("\r", "").Replace("\n", "").Replace(" ", "");
		}

		private static MetaData GetMetaDataForEntityCollection()
		{
			return new MetaData()
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
		}

		#endregion Helpers
	}
}
