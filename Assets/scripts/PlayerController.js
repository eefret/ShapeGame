#pragma strict

private var isDeactivated : boolean;
private var sides : int;
private var playerBody : Rigidbody2D;
private var isInZone: boolean;
private var playerAnim : Animator;
var strength : float = 1.0;

function Start () {
	isDeactivated = false;
	isInZone = false;
	sides = 0;
	playerBody = GetComponent(Rigidbody2D);
	playerAnim = GetComponent(Animator);
}

function Update () {
	if(Input.GetMouseButtonDown(0)){
		if (isInZone) {
			var posVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			playerBody.AddForce(posVec * strength, ForceMode2D.Impulse);
			sides++;
		} else {
			sides--;
		}
		isDeactivated = sides == 3;
		playerAnim.SetInteger("Sides",sides);
	}
}

function OnCollisionEnter2D(Bam: Collision2D){
	if(Bam.transform.tag == "spikes"){
		if(isDeactivated) {
			Destroy(this.gameObject);
		} else {
			Application.Quit();
			UnityEditor.EditorApplication.isPlaying = false;
		}
	}
}

function OnTriggerEnter2D(other: Collider2D) {
	isInZone = true;
}

function OnTriggerExit2D(other) {
	isInZone = false;
}