namespace Profility.JSONEntities
{
	using Newtonsoft.Json;
	using Profility.JSONEntities.Model;

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
					if (!(keyValuePair.Value is Newtonsoft.Json.Linq.JObject))
					{
						convertedAttributes.Add(keyValuePair.Key.ToLower(), keyValuePair.Value);
					}
					else
					{
						var refValue = (keyValuePair.Value as Newtonsoft.Json.Linq.JObject).ToObject<EntityReferenceValue>();
						convertedAttributes.Add(keyValuePair.Key.ToLower(), refValue);
					}
				}

				entityContainer.Entities[i] = convertedAttributes;
			}

			return entityContainer;
		}
	}
}
