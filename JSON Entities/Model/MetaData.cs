namespace Profility.JSONEntities
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	[DataContract]
	public class MetaData
	{

		[DataMember(Name = "logicalName")]
		public string LogicalName { get; set; }

		[DataMember(Name = "keyAttributes")]
		public string KeyAttributes { get; set; }

		[DataMember(Name = "attributes")]
		public List<MetaDataAttribute> Attributes { get; set; }
	}
}
