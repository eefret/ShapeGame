using UnityEngine;
using System.Collections;

public class ShapeManager : MonoBehaviour {
    
    //PUBLIC
    public enum ShapeType { TRIANGLE, SQUARE, PENTAGON};

    public float forceStrength = 10.0f;
    public ShapeType type;
    
    public Sprite[] spriteList;

    //PRIVATE
    private string playerName;

    private bool isInZone;

    private int sides;
    private int maxSides = 0;
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D playerBody;
    private PolygonCollider2D polygonCollider;

    private GameObject gameMaster;
    private GameMasterScript gameController;

	// Use this for initialization
	void Start () {
        isInZone = false;
        sides = 0;

        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        playerBody = GetComponent<Rigidbody2D>();

        switch (type) {
            case ShapeType.TRIANGLE:
                maxSides = 3;
                playerName = "TRIANGLE";
                break;
            case ShapeType.SQUARE:
                maxSides = 4;
                playerName = "SQUARE";
                break;
			case ShapeType.PENTAGON:
				maxSides = 5;
				playerName = "PENTAGON";
				break;
        }

        gameMaster = GameObject.FindGameObjectWithTag("GameController");
        gameController = gameMaster.GetComponent<GameMasterScript>();
	}

    void Update () {
        #if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0) {
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                checkInput(Input.GetTouch(0).position);
            }
        }
        #endif
        #if UNITY_WINDOWS || UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            checkInput(Input.mousePosition);
        }
        #endif
    }

    public void checkInput(Vector3 inputPosition) {
        Vector2 position = Camera.main.ScreenToWorldPoint(inputPosition);
        BoxCollider2D collider = gameController.greenZoneCollider;

        if(collider != null && collider.OverlapPoint(position)) {
            if (polygonCollider.OverlapPoint(position)) {
                if (sides == maxSides) {
                    gameMaster.SendMessage("GameOver");//Game Over
                }

                Vector3 pos = new Vector3(Random.Range(-5f, 5f), 10);
                playerBody.AddForce(pos * forceStrength, ForceMode2D.Impulse);

                DataManager.instance.AddScore(1);
                sides++;
                
                spriteRenderer.sprite = spriteList[sides];
            }
        }
    }
    
    public void OnCollisionEnter2D(Collision2D collision){
        if (collision.transform.tag == "spikes") {
            if (sides == maxSides) {
                Destroy(this.gameObject);
                gameMaster.SendMessage("ShapeDestroyed", playerName, SendMessageOptions.DontRequireReceiver);
                DataManager.instance.AddScore(10);
                Debug.Log("Destroyed Shape: " + playerName);
            } else {
                gameMaster.SendMessage("GameOver");//Game Over
                #if UNITY_EDITOR
                //Debug.Log("Unity Editor");
                //UnityEditor.EditorApplication.isPlaying = false;
                #endif
            }
        }
    }

}
