using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    //PUBLIC
    public enum ShapeType { TRIANGLE, SQUARE};
    public float strength = 1.0f;
    public ShapeType type;
    //PRIVATE
    private string playerName;
    private int maxSides = 0;
    private bool isDeactivated;
    private int sides;
    private Rigidbody2D playerBody;
    private bool isInZone;
    private Animator playerAnim;
    private GameObject gameMaster;
    private GameMasterScript gameController;

	// Use this for initialization
	void Start () {
        isDeactivated = false;
        isInZone = false;
        sides = 0;
        playerBody = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        switch (type) {
            case ShapeType.TRIANGLE:
                maxSides = 3;
                playerName = "TRIANGLE";
                break;
            case ShapeType.SQUARE:
                maxSides = 4;
                playerName = "SQUARE";
                break;
        }
        gameMaster = GameObject.FindGameObjectWithTag("GameController");
        gameController = gameMaster.GetComponent<GameMasterScript>();
	}

    public void OnCollisionEnter2D(Collision2D collision){
        if (collision.transform.tag == "spikes") {
            if (isDeactivated) {
                Destroy(this.gameObject);
                gameMaster.SendMessage("ShapeDestroyed", playerName, SendMessageOptions.DontRequireReceiver);
                gameController.addScore(10);
                Debug.Log("Destroyed Shape: " + playerName);
            } else {
                gameMaster.SendMessage("GameOver");//Game Over
                #if UNITY_EDITOR
                Debug.Log("Unity Editor");
                UnityEditor.EditorApplication.isPlaying = false;
                #endif
            }
        }
    }

    public void Clicked() {
        Debug.Log("clicked");
        if (isInZone) {
            Debug.Log("clicked in zone");
            Vector3 pos = new Vector3(Random.Range(-5f, 5f), 10);
            playerBody.AddForce(pos * strength, ForceMode2D.Impulse);
            sides++;
            gameController.addScore(1);
        } else {
            if (isDeactivated) {
                gameMaster.SendMessage("GameOver");//Game Over
            }
        }
        isDeactivated = sides == maxSides;
        playerAnim.SetInteger("Sides", sides);
    }

    public void OnTriggerStay2D(Collider2D collision) {
        isInZone = true;
    }

    public void OnTriggerExit2D(Collider2D collision) {
        isInZone = false;
    }



}
