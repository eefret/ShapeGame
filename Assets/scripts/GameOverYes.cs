using UnityEngine;
using System.Collections;

public class GameOverYes : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Clicked() {
        Application.LoadLevel(1);
    }
}
