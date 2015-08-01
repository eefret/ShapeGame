using UnityEngine;
using System.Collections;

public class GameMasterScript : MonoBehaviour {

    //Public
    public GameObject levelObject;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject ceilingObject;
    public GameObject shapePrefab;
    
    public BoxCollider2D greenZoneCollider;

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

    // Init a new shape if not on max
    void ShapeInit() {
        if (shapeCount < maxShapes) {
            shapeCount++;
            GameObject shape = Instantiate(shapePrefab) as GameObject;
            shape.transform.parent = levelObject.transform;
            float offsetY = (shapeCount > 1) ? Random.Range(5, 10f) : 0;
            shape.transform.localPosition = new Vector3(Random.Range(-3.5f, 3.5f), 16 + offsetY, -2);
        }
    }

    // Shape destroyed public function
    public void ShapeDestroyed(int size) {
        // If size of destroyed shape is 10 or more create 2 triangles
        if(size >= 10 && shapeCount < maxShapes) {
            DataManager.instance.lastShapeSize = 2;
            ShapeInit();
        } else {
            DataManager.instance.lastShapeSize = size;
        }
        // Decrece shape counter and init another shape
        shapeCount--;
        ShapeInit();
    }

    // On game over disable data update, save data to playerprefs
    public void GameOver() {
        // Datamanager
        DataManager.instance.updateData = false;
        DataManager.instance.SaveData();
        // Pause panel disable
        pausePanel.SetActive(false);
        // Enable gameoverpanel and disable level gameobject
        if(gameOverPanel != null) {
            gameOverPanel.SetActive(true);
        }
        if(levelObject != null) {
            levelObject.SetActive(false);
        }
    }
}
