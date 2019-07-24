namespace Profility.JSONEntities
{
	using System.Collections.Generic;
	using System.Linq;
	internal static partial class Validate
	{
		internal static void ForDuplicateIds(IEnumerable<Microsoft.Xrm.Sdk.Entity> collection)
		{
			if (collection.Count() > collection.Select(x => x.Id).Distinct().Count())
			{
				throw new LoadJsonEntityException(LoadJsonEntityException.DuplicateKey);
			}
		}
	}
}
