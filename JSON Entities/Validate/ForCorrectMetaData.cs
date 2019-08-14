namespace Profility.JSONEntities
{
	internal static partial class Validate
	{
		internal static void ForCorrectMetaData(MetaData meta)
		{
			if (meta.LogicalName == meta.KeyAttributes)
			{
				throw new LoadJsonEntityException(LoadJsonEntityException.InvalidMetaData);
			}
		}
	}	
}
