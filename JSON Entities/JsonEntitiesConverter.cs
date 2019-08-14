namespace Profility.JSONEntities
{
	using System;
	using System.Linq;
	using Microsoft.Xrm.Sdk;
	using System.Collections.Generic;

	public class JsonEntitiesConverter
	{
		private readonly JsonEntitiesConverterSettings JsonEntitiesConverterSettings = null;

		public JsonEntitiesConverter() : this(new JsonEntitiesConverterSettings())
		{}

		public JsonEntitiesConverter(JsonEntitiesConverterSettings jsonEntitiesConverterSettings)
		{
			JsonEntitiesConverterSettings = jsonEntitiesConverterSettings ?? new JsonEntitiesConverterSettings();
		}

		public List<Entity> Load(string path)
		{
			// Get all files
			var files = IO.Helper.ResolveFileNames(path);

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

		public void Save(string path, MetaData meta, IEnumerable<Entity> collection)
		{
			var txt = this.ToJson(meta, collection);

			// Save the Json
			IO.Helper.SaveJson(path, txt);
		}

		public string ToJson(MetaData meta, IEnumerable<Entity> collection)
		{
			// Load into entityContainer
			var entityContainer = Convert.EntityListToEntityContainer(meta, collection);

			// Get Json from entityContainer
			return Convert.ModelToJson(entityContainer, this.JsonEntitiesConverterSettings);
		}
	}
}