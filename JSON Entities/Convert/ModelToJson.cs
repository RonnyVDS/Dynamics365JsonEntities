namespace Profility.JSONEntities
{
	using Newtonsoft.Json;
	using Profility.JSONEntities.Model;
	using System.IO;
	using System.Text;

	internal static partial class Convert
	{
		internal static string ModelToJson(EntityContainer entityContainer, JsonEntitiesConverterSettings jsonEntitiesConverterSettings)
		{
			var format = jsonEntitiesConverterSettings.IndentJson ? Formatting.Indented : Formatting.None;
			var quoteChar = jsonEntitiesConverterSettings.QuoteChar;

			var sb = new StringBuilder();
			using (var sw = new StringWriter(sb))
			{
				using (JsonTextWriter writer = new JsonTextWriter(sw))
				{
					writer.QuoteChar = quoteChar;
					writer.Formatting = format;

					JsonSerializer ser = new JsonSerializer();
					ser.Serialize(writer, entityContainer);
				}
			}

			return sb.ToString();
		}
	}
}
