using UnityEngine;
using System.Collections;

public class GameMasterScript : MonoBehaviour {

    private static int TriangleCount = 0;
    private static int SquareCount = 0;
    private static int maxShapes = 3;
    private static int shapeCount = 0;

    public GameObject levelObject;
    public GameObject gameOverPanel;

    public BoxCollider2D greenZoneCollider;

    public GameObject Triangle;
    public GameObject Square;
	public GameObject Pentagon;

    public void Start() {
        DataManager.instance.Reset();
    }

	// Use this for initialization
	void Awake() {
        GameObject shape = Instantiate(Triangle) as GameObject;
        shape.transform.parent = levelObject.transform;
        shape.transform.localPosition = new Vector3(Random.Range(-3.5f, 3.5f), 10, -2);
	}

    void ShapeDestroyed(string shapeName) {
        if (shapeCount < maxShapes) {
            int selected = Random.Range(1, 2);
            GameObject shape = RandInstantiate(selected);
            if(shape != null) {
                shape.transform.parent = levelObject.transform;
                shape.transform.localPosition = new Vector3(Random.Range(-3.5f, 3.5f), 10, -2);
            }
        }
    }
    
    void GameOver() {
        DataManager.instance.updateData = false;
        DataManager.instance.SaveData();
        if(gameOverPanel != null) {
            gameOverPanel.SetActive(true);
        }
        if(levelObject != null) {
            levelObject.SetActive(false);
        }
    }
    
    GameObject RandInstantiate(int count) {
        bool made = false;
        for (int i = 0; i < count; i++) {
            int range = Random.Range(0, 4);
            if (!made) {
                range = Random.Range(1, 4);
            }
            switch (range) {
                case 0:
                break;
                case 1:
                made = true;
                if (shapeCount < maxShapes) {
                    return Instantiate(Triangle) as GameObject;
                }
                break;
                case 2:
                made = true;
                if (shapeCount < maxShapes) {
                    return Instantiate(Square) as GameObject;
                }
                break;
				case 3:
				made = true;
                if (shapeCount < maxShapes){
                    return Instantiate(Pentagon) as GameObject;
				}
				break;
            }   
        }
        return null;
    }
}
