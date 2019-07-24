namespace Profility.JSONEntities
{
	using System.IO;
	using System.Linq;
	using Newtonsoft.Json;
	using Microsoft.Xrm.Sdk;
	using System.Collections.Generic;
	using Profility.JSONEntities.Model;

	public class JsonEntitiesConverter
	{
		private readonly string Path;

		public JsonEntitiesConverter(string path)
		{
			this.Path = path;
		}

		public List<Entity> Load()
		{
			// Get all files
			var files = IO.Helper.ResolveFileNames(this.Path);

			// Load into models
			var entityContainers = files.Select(f => Convert.JsonToModel(IO.Helper.ReadFile(f)));

			// Extract only the entities
			var entities = new List<Entity>();
			foreach (var entityContainer in entityContainers)
			{
				entities.AddRange(Convert.EntityContainerToEntityList(entityContainer));
			}

			// Validate collection
			Validate.ForDuplicateIds(entities);

			return entities;
		}

		public bool Save(string savePath, MetaData meta, List<Entity> collection)
		{
			var obj = GetRootObject(meta, collection);

			var txt = JsonConvert.SerializeObject(obj, Formatting.Indented);

			File.WriteAllText(savePath, txt);
			File.WriteAllText(@".\..\." + savePath, txt);

			return true;
		}

		#region Helpers

		private static EntityContainer GetRootObject(MetaData meta, List<Entity> collection)
		{

			var list = new List<EntityAttributes>();
			foreach (var entity in collection)
			{
				var d = new EntityAttributes();
				d.Add("id", $"{{{entity.Id.ToString()}}}");
				foreach (var a in entity.Attributes)
				{
					if (a.Key == meta.KeyAttributes) { continue; }
					d.Add(a.Key, a.Value);
				}
				list.Add(d);
			}


			return new EntityContainer() { Metadata = meta, Entities = list };

		}


		#endregion
	}
}