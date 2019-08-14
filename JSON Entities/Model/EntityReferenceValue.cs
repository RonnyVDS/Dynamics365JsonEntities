namespace Profility.JSONEntities.Model
{
	using System.Runtime.Serialization;

	[DataContract]
	public class EntityReferenceValue
	{
		[DataMember(Name = "id")]
		public string Id { get; set; }
		[DataMember(Name = "logicalName")]
		public string LogicalName { get; set; }
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }
	}
}
