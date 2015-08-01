using UnityEngine;
using System.Collections;

public class GameMasterScript : MonoBehaviour {

    //Public
    public GameObject levelObject;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    public GameObject ceilingObject;

    public BoxCollider2D greenZoneCollider;

    public GameObject shapePrefab;

    //Private
    private int maxShapes = 3;
    private int shapeCount = 0;

    public void Start() {
        DataManager.instance.Reset();
    }

	// Use this for initialization
	void Awake() {
        ShapeInit();
	}

    void ShapeInit() {
        if (shapeCount < maxShapes) {
            shapeCount++;
            GameObject shape = Instantiate(shapePrefab) as GameObject;
            shape.transform.parent = levelObject.transform;
            float offsetY = (shapeCount > 1) ? Random.Range(5, 10f) : 0;
            shape.transform.localPosition = new Vector3(Random.Range(-3.5f, 3.5f), 16 + offsetY, -2);
        }
    }
    
    public void ShapeDestroyed(int size) {
        if(size >= 10 && shapeCount < maxShapes) {
            DataManager.instance.lastShapeSize = 2;
            ShapeInit();
        } else {
            DataManager.instance.lastShapeSize = size;
        }
        shapeCount--;
        ShapeInit();
    }
    
    public void GameOver() {
        DataManager.instance.updateData = false;
        DataManager.instance.SaveData();
        pausePanel.SetActive(false);
        if(gameOverPanel != null) {
            gameOverPanel.SetActive(true);
        }
        if(levelObject != null) {
            levelObject.SetActive(false);
        }
    }
}
