namespace Profility.Dynamics365.JSONEntities
{
	using Microsoft.Xrm.Sdk;
	using System;
	using System.Collections.Generic;

	public class JsonEntitiesContext
	{


		public JsonEntitiesContext(string path)
		{

		}

		public List<Entity> Load() {
			return new List<Entity>();
		}
	}
}
