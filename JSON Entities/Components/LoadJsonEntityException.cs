namespace Profility.JSONEntities
{
	using System;

	[Serializable]
	public class LoadJsonEntityException : Exception
	{
		public const string DuplicateKey = "Duplicate key detected.";

		public const string BadDirectory = "Directory does not exist.";

		public const string BadFile = "File does not exist.";

		public const string IDNotDefined = "ID should be defined 1 time for each entity.";

		public const string FieldNameNotInMetaData = "FieldName is not in metadata.";

		public const string CannotConvertValue = "Failed to convert value.";

		public const string IntersectNotInRightFormat = "Intersect not in right format";

		public const string InvalidMetaData = "Invalid metadata.";


		public LoadJsonEntityException()
		{
		}

		public LoadJsonEntityException(string message)
			: base(message)
		{
		}

		public LoadJsonEntityException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
