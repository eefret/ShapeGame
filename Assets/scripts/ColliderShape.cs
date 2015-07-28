using UnityEngine;
using System.Collections;

public class ColliderShape : MonoBehaviour {

    //Public
    [ContextMenuItem("Set points", "SetPoints")]
    public int points;
    public Vector2[] shapePoints;
    //Private
    private Vector2 lastPosition;

	// Use this for initialization
	void Start () {
        SetPoints();
	}
	
	// Update is called once per frame
	void Update () {
	    
    }
    
    void SetPoints () {
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        
        polygonCollider.points = shapePoints;
    }

    void OnDrawGizmos () {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.green;

        foreach(Vector2 point in shapePoints) {
            Gizmos.DrawCube(point, new Vector3(0.1f, 0.1f, 0.1f));
        }
    }
}
