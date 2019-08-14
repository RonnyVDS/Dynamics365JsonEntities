namespace Profility.JSONEntities
{
	using System.Collections.Generic;
	using System.Linq;
	using System;
	using Microsoft.Xrm.Sdk;

	internal static partial class Validate
	{
		internal static void ForIntersectData(MetaData meta, List<Entity> entities)
		{
			if (meta.EntityType.ToLower() != "intersect")
			{
				foreach (var entity in entities)
				{
					foreach (var a in entity.Attributes)
					{
						
						if (a.Value.GetType() == typeof(Guid) && a.Key.ToLower() != meta.KeyAttributes.ToLower())
						{
							throw new LoadJsonEntityException(LoadJsonEntityException.IntersectNotInRightFormat);
						}
					}
				}

				return;
			}

			if (meta.Attributes.Count != 2)
			{
				throw new LoadJsonEntityException(LoadJsonEntityException.IntersectNotInRightFormat);
			}

			foreach (var entity in entities)
			{

				if (entity.Attributes.Count != 3)
				{
					throw new LoadJsonEntityException(LoadJsonEntityException.IntersectNotInRightFormat);
				}

				foreach (var a in entity.Attributes)
				{
					if (a.Value.GetType() != typeof(Guid))
					{
						throw new LoadJsonEntityException(LoadJsonEntityException.IntersectNotInRightFormat);
					}
				}
			}
		}
	}
}
