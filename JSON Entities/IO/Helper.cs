namespace Profility.JSONEntities.IO
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	internal static class Helper
	{
		internal static IEnumerable<string> ResolveFileNames(string Path)
		{
			//Path is probably a file
			if (Path.EndsWith(".json"))
			{
				//If it's a file, return the entities in the file
				if (File.Exists(Path))
				{
					return new string[] { (new FileInfo(Path)).FullName };
				}
				// if it's not a file, we have to check for wildcard
				else if (Path.Contains('*'))
				{
					var directoryPath = Path.Substring(0, Path.LastIndexOf('\\'));
					var searchPattern = Path.Substring(Path.LastIndexOf('\\') + 1);

					directoryPath.DirectoryCheck();

					return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
				}
				//If file does not exist at all, throw an error 
				else
				{
					throw new LoadJsonEntityException(LoadJsonEntityException.BadFile);
				}
			}
			//Path is probably a directory
			else
			{
				Path.DirectoryCheck();

				return Directory.GetFiles(Path, "*.json", SearchOption.AllDirectories);
			}
		}

		internal static string ReadFile(string path) {

			using (var r = new StreamReader(path))
			{
				return r.ReadToEnd();
			}
		}

		internal static void SaveJson(string path, string json)
		{
			File.WriteAllText(path, json);
		}

		private static void DirectoryCheck(this string path)
		{
			if (!System.IO.Directory.Exists(path))
			{
				throw new LoadJsonEntityException(LoadJsonEntityException.BadDirectory);
			}
		}
	}
}
