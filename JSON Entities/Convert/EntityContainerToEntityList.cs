﻿namespace Profility.JSONEntities
{
	using Microsoft.Xrm.Sdk;
	using Profility.JSONEntities.Model;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	internal static partial class Convert
	{
		private static string[] DateTimeFormats = new string[] {
			"yyyy-MM-dd HH:mm:ss",
			"yyyy-MM-dd",
			"yyyy/MM/dd HH:mm:ss",
			"yyyy/MM/dd",
			"yyyyMMdd HH:mm:ss",
			"yyyyMMdd"
		};

		internal static IEnumerable<Entity> EntityContainerToEntityList(EntityContainer entityContainer)
		{

			var collection = new List<Entity>();

			foreach (var entityModel in entityContainer.Entities)
			{
				var entity = new Entity(entityContainer.Metadata.LogicalName, GetId(entityModel));
				entity[entityContainer.Metadata.KeyAttributes] = entity.Id;

				foreach (var field in entityModel)
				{
					var fieldName = field.Key;
					var fieldValue = field.Value;
					if (fieldName == "id") { continue; }

					//Get Type from fieldName 
					var metaDataAttribute = GetMetaDataAttribute(entityContainer.Metadata, fieldName);
					entity[fieldName] = ConvertValue(metaDataAttribute, fieldValue);
				}
				collection.Add(entity);
			}

			Validate.ForCorrectMetaData(entityContainer.Metadata);
			Validate.ForIntersectData(entityContainer.Metadata, collection);

			return collection;
		}

		private static object ConvertValue(MetaDataAttribute metaDataAttribute, object fieldValue)
		{
			if (fieldValue == null) return null;

			switch (metaDataAttribute.Type.ToLower())
			{
				case DataTypes.String:
					return fieldValue.ToString();

				case DataTypes.Int:
					if (fieldValue is int) return fieldValue;
					if (int.TryParse(fieldValue.ToString(), out int intValue)) return intValue;
					throw new LoadJsonEntityException(LoadJsonEntityException.CannotConvertValue);

				case DataTypes.OptionSetValue:
					if (fieldValue is int) return new OptionSetValue((int)fieldValue);
					if (int.TryParse(fieldValue.ToString(), out int intOptionValue)) return new OptionSetValue(intOptionValue);
					throw new LoadJsonEntityException(LoadJsonEntityException.CannotConvertValue);

				case DataTypes.Float:
					if (fieldValue is float) return fieldValue;
					if (float.TryParse(fieldValue.ToString(), out float floatValue)) return floatValue;
					throw new LoadJsonEntityException(LoadJsonEntityException.CannotConvertValue);

				case DataTypes.Decimal:
					if (fieldValue is decimal) return fieldValue;
					if (decimal.TryParse(fieldValue.ToString(), out decimal decimalValue)) return decimalValue;
					throw new LoadJsonEntityException(LoadJsonEntityException.CannotConvertValue);

				case DataTypes.Bool:
					if (fieldValue is bool) return fieldValue;
					if (bool.TryParse(fieldValue.ToString(), out bool boolValue)) return boolValue;
					throw new LoadJsonEntityException(LoadJsonEntityException.CannotConvertValue);

				case DataTypes.DateTime:
					if (DateTime.TryParseExact(
						fieldValue as string,
						DateTimeFormats,
						System.Globalization.CultureInfo.InvariantCulture,
						System.Globalization.DateTimeStyles.None,
						out DateTime dtValue))
					{
						dtValue = DateTime.SpecifyKind(dtValue, DateTimeKind.Utc);
						return dtValue;
					}

					throw new LoadJsonEntityException(LoadJsonEntityException.CannotConvertValue);

				case DataTypes.EntityReference:
					var entityRef = new EntityReference();
					var valueRef = fieldValue as EntityReferenceValue;
					if (valueRef == null || valueRef.Id == null) throw new LoadJsonEntityException(LoadJsonEntityException.CannotConvertValue);

					if (Guid.TryParse(valueRef.Id as string, out Guid entityReferenceId))
					{
						entityRef.Id = entityReferenceId;
					}
					else throw new LoadJsonEntityException(LoadJsonEntityException.CannotConvertValue);
					entityRef.LogicalName = valueRef.LogicalName;
					entityRef.Name = valueRef.Name;

					return entityRef;

				case DataTypes.Guid:
					if (Guid.TryParse(fieldValue as string, out Guid id))
					{
						return id;
					}
					else throw new LoadJsonEntityException(LoadJsonEntityException.CannotConvertValue);

				case DataTypes.EntityCollection:
					try
					{
						return GetEntityCollection(fieldValue as List<EntityAttributes>, metaDataAttribute.SubMetadata); ;
					}
					catch (Exception)
					{
						throw new LoadJsonEntityException(LoadJsonEntityException.CannotConvertValue);
					}
					
				default:
					throw new NotImplementedException("Value type not not implemented.");
			}
		}

		private static Guid GetId(EntityAttributes attributes)
		{
			if (!attributes.ContainsKey("id")) throw new LoadJsonEntityException(LoadJsonEntityException.IDNotDefined);
			if (attributes["id"] as string == null) throw new LoadJsonEntityException(LoadJsonEntityException.IDNotDefined);

			if (Guid.TryParse(attributes["id"] as string, out Guid id))
			{
				return id;
			}

			throw new LoadJsonEntityException(LoadJsonEntityException.IDNotDefined);
		}

		private static MetaDataAttribute GetMetaDataAttribute(MetaData metaData, string fieldName)
		{
			var meta = metaData.Attributes.FirstOrDefault(a => a.LogicalName.ToLower() == fieldName);
			if (meta == null)
			{
				throw new LoadJsonEntityException(LoadJsonEntityException.FieldNameNotInMetaData);
			}

			return meta;
		}

		private static EntityCollection GetEntityCollection(List<EntityAttributes> collection, MetaData subMetadata)
		{
			var entityCollection = new EntityCollection();

			foreach (var item in collection)
			{
				var activityParty = new Entity(subMetadata.LogicalName, GetId(item));
				
				foreach (var i in item)
				{
					var fieldName = i.Key;
					var fieldValue = i.Value;
					if (fieldName == "id") { continue; }

					var metaDataAttribute = GetMetaDataAttribute(subMetadata, fieldName);
					activityParty[fieldName] = ConvertValue(metaDataAttribute, fieldValue);

				}

				entityCollection.Entities.Add(activityParty);
			}

			return entityCollection;
		}
	}
}
