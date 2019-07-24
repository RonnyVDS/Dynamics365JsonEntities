namespace Profility.JSONEntities.Tests
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class JsonEntitiesContext_Save
	{

		[TestMethod]

		public void FirstTestOnSave()
		{
			var loadPath = @".\TestFiles\Save\save1.json";
			var savePath = @".\TestFiles\Save\save2.json";
			var saverCtx = new JsonEntitiesConverter(loadPath);
			var cars = saverCtx.Load();

			foreach (var car in cars)
			{
				car["price"] = 500;
			}

			var meta = new MetaData()
			{
				LogicalName = "car",
				KeyAttributes = "carid",
				Attributes = new List<MetaDataAttribute>()
				{
					new MetaDataAttribute() {LogicalName = "brand", Type = DataTypes.String},
					new MetaDataAttribute() {LogicalName = "price", Type = DataTypes.Int}
				}
			};
			

			saverCtx.Save(savePath, meta, cars);

			var loaderCtx = new JsonEntitiesConverter(savePath);
			var updatedCars = loaderCtx.Load();

			foreach (var updatedCar in updatedCars)
			{
				Assert.IsTrue((int)updatedCar["price"] == 500);
			}
		}
	}
}
