namespace Profility.JSONEntities
{
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	[DataContract]
	public class MetaDataAttribute
	{
		public MetaDataAttribute()
		{
		}

		public MetaDataAttribute(string logicalName, string type)
		{
			LogicalName = logicalName ?? throw new ArgumentNullException(nameof(logicalName));
			Type = type ?? throw new ArgumentNullException(nameof(type));
		}

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
