using UnityEngine;
using System.Collections;

public class ShapeManager : MonoBehaviour {
    
    //PUBLIC
    public float forceStrength = 10.0f;

    public PolygonCollider2D polygonCollider;

    private int _maxSides;
    public int maxSides {
        get { return _maxSides; }
        set {
            angleStep = Mathf.PI * 2 / value;
            _maxSides = value;
        }
    }

    //PRIVATE
    private int sides;
    private float angleStep;
    private float angle;

    private Rigidbody2D playerBody;
    private PolygonCollider2D polygonTrigger;

    private GameMasterScript gameController;
    private Material material;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        sides = 0;
        angle = 0;

        // Setup material
        Renderer renderer = GetComponent<Renderer>();
        material = new Material(renderer.material);
        renderer.material = material;

        // Setup collider and trigger
        polygonTrigger = GetComponent<PolygonCollider2D>();
        polygonCollider.enabled = false;
        playerBody = GetComponent<Rigidbody2D>();

        // Get audiosource
        audioSource = GetComponent<AudioSource>();

        // Get game master script
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMasterScript>();
	}

    void Update () {
        // Enable polygon collider if pos y > 0
        if(transform.position.y < 0) {
            polygonCollider.enabled = true;
        }

        // Input manager
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

    // Check input
    public void checkInput(Vector3 inputPosition) {
        Vector2 position = Camera.main.ScreenToWorldPoint(inputPosition);
        BoxCollider2D collider = gameController.greenZoneCollider;

        if(collider != null && collider.OverlapPoint(position)) {
            if (polygonTrigger.OverlapPoint(position)) {
                if (sides == maxSides) {
                    gameController.GameOver(); //Game Over
                }

                Vector3 pos = new Vector3(Random.Range(-5f, 5f), 10);
                playerBody.AddForce(pos * forceStrength, ForceMode2D.Impulse);

                audioSource.pitch = Random.Range(0.8f, 1.2f);
                audioSource.Play();

                DataManager.instance.AddScore(1);
                angle += angleStep;
                material.SetFloat("_Angle", angle);
                sides++;
            }
        }
    }

    // Collision manager
    public void OnCollisionEnter2D(Collision2D collision){
        if (collision.transform.tag == "spikes") {
            if (sides == maxSides) {
                Destroy(this.gameObject);
                gameController.ShapeDestroyed(maxSides);
                DataManager.instance.AddScore(maxSides);
            } else {
                gameController.GameOver(); //Game Over
                #if UNITY_EDITOR
                //Debug.Log("Unity Editor");
                //UnityEditor.EditorApplication.isPlaying = false;
                #endif
            }
        }
    }

}
