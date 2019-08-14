namespace Profility.JSONEntities
{
	using System;
	using Microsoft.Xrm.Sdk;
	using System.Collections.Generic;
	using Profility.JSONEntities.Model;
	using System.Linq;

	internal static partial class Convert
	{
		internal static EntityContainer EntityListToEntityContainer(MetaData meta, IEnumerable<Entity> collection)
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
					var fieldMeta = meta.Attributes.SingleOrDefault(a => a.LogicalName == fieldName);
					if (fieldMeta == null) continue;

					attribute[fieldName] = null;
					if (fieldValue == null) continue;

					switch (fieldMeta.Type)
					{
						case DataTypes.String:
						case DataTypes.Int:
						case DataTypes.Float:
						case DataTypes.Decimal:
						case DataTypes.Bool:
							attribute[fieldName] = fieldValue;
							break;
						case DataTypes.EntityReference:
							var r = fieldValue as EntityReference;
							attribute[fieldName] = new EntityReferenceValue()
							{
								Id = $"{{{r.Id.ToString()}}}",
								LogicalName = r.LogicalName,
								Name = r.Name
							};
							break;

						case DataTypes.OptionSetValue:
							attribute[fieldName] = (fieldValue as OptionSetValue).Value;
							break;

						case DataTypes.DateTime:
							attribute[fieldName] = ((DateTime)fieldValue).ToString("yyyy-MM-dd HH:mm:ss");
							break;

						case DataTypes.Guid:
							attribute[fieldName] = $"{{{fieldValue}}}";
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
							attribute[fieldName] = listOfEntityAttributes;
							break;

						default:
							throw new NotImplementedException("Type not (yet) implemented.");
					}
				}

				attributes.Add(attribute);
			}

			return new EntityContainer() { Metadata = meta, Entities = attributes };
		}
	}
}