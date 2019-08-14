namespace Profility.JSONEntities
{
	using Newtonsoft.Json;
	using Profility.JSONEntities.Model;

	internal static partial class Convert
	{
		internal static string ModelToJson(EntityContainer entityContainer)
		{
			return JsonConvert.SerializeObject(entityContainer, Formatting.Indented);
		}
	}
}
