namespace Profility.JSONEntities
{
	using System;
	using System.Linq;
	using Microsoft.Xrm.Sdk;
	using System.Collections.Generic;

	public class JsonEntitiesConverterSettings
	{
		/// <summary>
		/// <para>
		/// When saving to file the json can be Indented
		/// </para>
		/// <para>
		/// Default true
		/// </para>
		/// </summary>
		public bool IndentJson { get; set; } = true;

		/// <summary>
		/// By default json is created using double quotes, this settgin allow it to be changed to single quote
		/// </summary>
		public char QuoteChar { get; set; } = '\"';
	}
}