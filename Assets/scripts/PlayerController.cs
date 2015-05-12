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
	}

    // Update is called once per frame
    void Update() {
        if (Input.touchCount > 0) {
            if (Input.GetTouch(0).phase.Equals(TouchPhase.Began)) {
                if (isInZone) {
                    Debug.Log("clicked in zone");
                    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    playerBody.AddForce(pos * strength, ForceMode2D.Impulse);
                    sides++;
                } else {
                    Debug.Log("Clicked out zone");
                    sides--;
                }
            }
            Debug.Log("sides :" + sides);
            isDeactivated = sides == maxSides;
            playerAnim.SetInteger("Sides", sides);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision){
        if (collision.transform.tag == "spikes") {
            if (isDeactivated) {
                Destroy(this.gameObject);
                gameMaster.SendMessage("ShapeDestroyed", playerName, SendMessageOptions.DontRequireReceiver);
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

    public void OnTriggerStay2D(Collider2D collision) {
        Debug.Log("Trigger stayed");
        isInZone = true;
    }

    public void OnTriggerExit2D(Collider2D collision) {
        Debug.Log("Trigger out");
        isInZone = false;
    }



}
