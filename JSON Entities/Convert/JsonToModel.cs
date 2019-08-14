namespace Profility.JSONEntities
{
	using Newtonsoft.Json;
	using Profility.JSONEntities.Model;
	using System.Collections.Generic;

	internal static partial class Convert
	{
		internal static EntityContainer JsonToModel(string json) {

			var entityContainer = JsonConvert.DeserializeObject<EntityContainer>(json);

			// Remove all newtonsoft json objects
			for (int i = 0; i < entityContainer.Entities.Count; i++)
			{
				var convertedAttributes = new EntityAttributes();

				foreach (var keyValuePair in entityContainer.Entities[i])
				{
					if (keyValuePair.Value is Newtonsoft.Json.Linq.JObject)
					{
						var refValue = (keyValuePair.Value as Newtonsoft.Json.Linq.JObject).ToObject<EntityReferenceValue>();
						convertedAttributes.Add(keyValuePair.Key.ToLower(), refValue);
					}
					else if (keyValuePair.Value is Newtonsoft.Json.Linq.JArray)
					{
						var refValue = (keyValuePair.Value as Newtonsoft.Json.Linq.JArray).ToObject<List<EntityAttributes>>();
						convertedAttributes.Add(keyValuePair.Key.ToLower(), refValue);
					}
					else
					{
						convertedAttributes.Add(keyValuePair.Key.ToLower(), keyValuePair.Value);
					}
				}

				entityContainer.Entities[i] = convertedAttributes;
			}

			return entityContainer;
		}
	}
}
