using UnityEngine;
using System.Collections;

public class GameMasterScript : MonoBehaviour {

    private static int TriangleCount = 0;
    private static int SquareCount = 0;
    public GameObject Triangle;
    public GameObject Square;

	// Use this for initialization
	void Awake() {
        Instantiate(Triangle, new Vector3(-2, 10, 0), Quaternion.identity);
	}

    void ShapeDestroyed(string shape) {
        switch (shape) {
            case "TRIANGLE":
                TriangleCount++;
                RandInstantiate(TriangleCount);
                break;
            case "SQUARE":
                RandInstantiate(SquareCount);
                break;
        }
    }

    void GameOver() {

    }

    void RandInstantiate(int count) {
        bool made = false;
        for (int i = 0; i < count; i++) {
            int range = Random.Range(0, 2);
            if (!made) {
                range = Random.Range(1, 2);
            }
            switch (range) {
                case 0:
                   break;
                case 1:
                    made = true;
                    Instantiate(Triangle, new Vector3(-2, 10, 0), Quaternion.identity);
                    break;
                case 2:
                    made = true;
                    Instantiate(Square, new Vector3(2, 10, 0), Quaternion.identity);
                    break;
            }   
        }
    }
}
