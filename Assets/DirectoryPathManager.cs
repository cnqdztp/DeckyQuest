using UnityEngine;

namespace inspiration.Utilities
{
	public static class DirectoryPathHelper
	{
		public enum OutputPath
		{
			RelativeToProject,
			RelativeToPeristentData,
			Absolute,
			RelativeToDesktop,
			RelativeToPictures,
			RelativeToVideos,
			PhotoLibrary
		}
		
		public static string GetFolder(OutputPath outputPathType, string path)
		{
			string folder = string.Empty;

#if UNITY_IOS && !UNITY_EDITOR
			// iOS only supports a very limited subset of OutputPath so fix up and warn the user
			switch (outputPathType)
			{
				case OutputPath.RelativeToPeristentData:
				case OutputPath.PhotoLibrary:
					// These are fine
					break;
				case OutputPath.RelativeToProject:
				case OutputPath.Absolute:
				case OutputPath.RelativeToDesktop:
				case OutputPath.RelativeToVideos:
				case OutputPath.RelativeToPictures:
					// These are unsupported
				default:
					Debug.LogWarning(string.Format("[AVProMovieCapture] 'OutputPath.{0}' is not supported on iOS, defaulting to 'OutputPath.RelativeToPeristentData'", outputPathType));
					outputPathType = OutputPath.RelativeToPeristentData;
					break;
			}
#endif

#if UNITY_EDITOR
			// Photo Library is unavailable in the editor
			if (outputPathType == OutputPath.PhotoLibrary)
			{
				Debug.LogWarning("[AVProMovieCapture] 'OutputPath.PhotoLibrary' is not available in the Unity Editor, defaulting to 'OutputPath.RelativeToProject'");
				outputPathType = OutputPath.RelativeToProject;
			}
#endif

			switch (outputPathType)
			{
				case OutputPath.RelativeToProject:
					#if UNITY_STANDALONE_OSX
					// For standalone macOS builds this puts the path at the same level as the application bundle
					folder = System.IO.Path.GetFullPath(System.IO.Path.Combine(Application.dataPath, "../.."));
					#else
					folder = System.IO.Path.GetFullPath(System.IO.Path.Combine(Application.dataPath, ".."));
					#endif
					break;
				case OutputPath.RelativeToPeristentData:
					folder = System.IO.Path.GetFullPath(Application.persistentDataPath);
					break;
				case OutputPath.Absolute:
					break;
				case OutputPath.RelativeToDesktop:
					folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
					break;
				case OutputPath.RelativeToPictures:
					folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
					break;
				case OutputPath.RelativeToVideos:
					#if NET_4_6
					folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyVideos);
					#else
					folder = System.Environment.GetFolderPath((System.Environment.SpecialFolder)14);	// Older Mono doesn't have MyVideos defined - but still works!
					#endif
					break;
				case OutputPath.PhotoLibrary:
					// use avpmc-photolibrary as the scheme
					folder = "avpmc-photolibrary:///";	// Three slashes are good as we don't need the host component
					break;
			}

			return System.IO.Path.Combine(folder, path);
		}

	}
}