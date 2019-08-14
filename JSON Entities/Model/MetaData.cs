namespace Profility.JSONEntities
{
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Runtime.Serialization;

	[DataContract]
	public class MetaData
	{

		[DataMember(Name = "logicalName")]
		public string LogicalName { get; set; }

		[DataMember(Name = "keyAttributes", EmitDefaultValue = false)]
		public string KeyAttributes { get; set; }

		[DataMember(Name = "attributes")]
		public List<MetaDataAttribute> Attributes { get; set; }

		[DefaultValue("normal")]
		[DataMember(Name = "entityType", EmitDefaultValue = false)]
		public string EntityType { get; set; } = "normal";
	}
}
