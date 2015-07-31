using UnityEngine;
using System.Collections;

public class GameMasterScript : MonoBehaviour {

    private static int maxShapes = 3;
    private static int shapeCount = 0;

    public GameObject levelObject;
    public GameObject gameOverPanel;

    public BoxCollider2D greenZoneCollider;

    public GameObject shapePrefab;

    public void Start() {
        DataManager.instance.Reset();
    }

	// Use this for initialization
	void Awake() {
        GameObject shape = Instantiate(shapePrefab) as GameObject;
        shape.transform.parent = levelObject.transform;
        shape.transform.localPosition = new Vector3(Random.Range(-3.5f, 3.5f), 10, -2);
	}

    void ShapeDestroyed() {
        if (shapeCount < maxShapes) {
            GameObject shape = Instantiate(shapePrefab) as GameObject;
            shape.transform.parent = levelObject.transform;
            shape.transform.localPosition = new Vector3(Random.Range(-3.5f, 3.5f), 10, -2);
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
}
