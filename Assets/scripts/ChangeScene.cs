using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public void ChangeSceneByName (string sceneName) {
		Application.LoadLevel(sceneName);
	}

	public void ChangeSceneByID (int sceneID) {
		Application.LoadLevel(sceneID);
	}

}