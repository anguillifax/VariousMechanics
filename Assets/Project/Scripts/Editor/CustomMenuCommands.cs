using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace VariousMechanicsEditor
{
	public class CustomMenuCommands
	{
		[MenuItem("Custom/Open Saves Folder")]
		public static void OpenSavesFolder()
		{
			EditorUtility.RevealInFinder(Application.persistentDataPath);
		}

		//[MenuItem("Custom/Play Game From Start %#&P")]
		//public static void PlayGameFromStart()
		//{
		//	if (EditorApplication.isPlaying)
		//	{
		//		var config = Resources.Load<PlaymodeStartConfig>("DataLookup/PlaymodeStartConfig");
		//		EditorSceneManager.LoadSceneInPlayMode(config.startScene.ScenePath, new LoadSceneParameters()
		//		{
		//			loadSceneMode = LoadSceneMode.Single,
		//			localPhysicsMode = LocalPhysicsMode.None
		//		});
		//	}
		//}

		[MenuItem("Custom/Reset Editor Startup Scene")]
		public static void ResetEditorStartupScene()
		{
			EditorSceneManager.playModeStartScene = null;
		}
	}
}