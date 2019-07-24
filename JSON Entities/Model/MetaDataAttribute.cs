namespace Profility.JSONEntities
{
	using Newtonsoft.Json;
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	[DataContract]
	public class MetaDataAttribute
	{
		[DataMember(Name = "logicalName")]
		public string LogicalName { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; }

		[DataMember(Name = "logicalNames")]
		public List<string> LogicalNames { get; set; }
	}
}
