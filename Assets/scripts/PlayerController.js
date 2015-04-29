#pragma strict

private var isDeactivated : boolean;
private var movementCounts : int;
private var playerBody : Rigidbody2D;

function Start () {
	isDeactivated = false;
	movementCounts = 0;
	playerBody = GetComponent(Rigidbody2D);
}

function Update () {
	if(Input.GetMouseButton(0)){
		var posVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		playerBody.AddForce(posVec, ForceMode2D.Impulse);		
	}
}

function OnCollisionEnter2D(Bam: Collision2D){
	if(Bam.transform.tag == "spikes"){
		Application.Quit();
		UnityEditor.EditorApplication.isPlaying = false;
	}
}