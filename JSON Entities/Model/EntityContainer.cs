namespace Profility.JSONEntities.Model
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	[DataContract]
	public class EntityContainer
	{
		[DataMember(Name = "metadata")]
		public MetaData Metadata { get; set; }

		[DataMember(Name = "entities")]
		public List<EntityAttributes> Entities { get; set; }

	}
}