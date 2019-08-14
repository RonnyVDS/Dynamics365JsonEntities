namespace Profility.JSONEntities
{
	using System;
	using System.Linq;
	using Microsoft.Xrm.Sdk;
	using System.Collections.Generic;

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

		public void Save(string savePath, MetaData meta, List<Entity> collection)
		{
			// Load into entityContainer
			var entityContainer = Convert.EntityListToEntityContainer(meta, collection);

			// Get Json from entityContainer
			var txt = Convert.ModelToJson(entityContainer);

			// Save the Json
			IO.Helper.SaveJson(savePath, txt);
			IO.Helper.SaveJson(@".\..\." + savePath, txt);		
		}	
	}
}