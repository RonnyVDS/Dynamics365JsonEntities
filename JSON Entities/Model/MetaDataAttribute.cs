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

		[DataMember(Name = "logicalNames", EmitDefaultValue = false)]
		public List<string> LogicalNames { get; set; }

		[DataMember(Name = "subMetadata", EmitDefaultValue = false)]
		public MetaData SubMetadata { get; set; }
	}
}
