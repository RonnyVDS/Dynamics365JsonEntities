namespace Profility.JSONEntities
{
	using System;
	using Microsoft.Xrm.Sdk;
	using System.Collections.Generic;
	using Profility.JSONEntities.Model;

	internal static partial class Convert
	{
		internal static EntityContainer EntityListToEntityContainer(MetaData meta, List<Entity> collection)
		{
			var attributes = new List<EntityAttributes>();

			foreach (var crmEntity in collection)
			{
				var attribute = new EntityAttributes();

				attribute.Add("id", $"{{{crmEntity.Id.ToString()}}}");

				foreach (var attr in crmEntity.Attributes)
				{
					var fieldName = attr.Key;
					var fieldValue = attr.Value;

					if (fieldName == meta.KeyAttributes) { continue; }

					switch (fieldValue.GetType().Name.ToLower())
					{
						case DataTypes.EntityReference:
							var r = fieldValue as EntityReference;
							attribute.Add(fieldName, new EntityReferenceValue()
							{
								Id = $"{{{r.Id.ToString()}}}",
								LogicalName = r.LogicalName,
								Name = r.Name
							});
							break;

						case DataTypes.OptionSetValue:
							attribute.Add(fieldName, (fieldValue as OptionSetValue).Value);
							break;

						case DataTypes.DateTime:
							attribute.Add(fieldName, ((DateTime)fieldValue).ToString("yyyy-MM-dd hh:mm:ss"));
							break;

						case DataTypes.Guid:
							attribute.Add(fieldName, $"{{{fieldValue}}}");
							break;

						case DataTypes.EntityCollection:
							var listOfEntityAttributes = new List<EntityAttributes>();
							foreach (var entity in (fieldValue as EntityCollection).Entities)
							{
								var entityAttribute = new EntityAttributes();
								entityAttribute.Add("id", $"{{{entity.Id}}}");
								foreach (var a in entity.Attributes)
								{
									entityAttribute.Add(a.Key, a.Value);
								}
								listOfEntityAttributes.Add(entityAttribute);
							}
							attribute.Add(fieldName, listOfEntityAttributes);
							break;

						default:
							attribute.Add(fieldName, fieldValue);
							break;
					}
				}

				attributes.Add(attribute);
			}

			return new EntityContainer() { Metadata = meta, Entities = attributes };
		}
	}
}