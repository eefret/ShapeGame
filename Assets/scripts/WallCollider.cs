using UnityEngine;
using System.Collections;

public class WallCollider : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    private float alpha;
    private Vector3 originalScale;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        alpha = spriteRenderer.color.a;

        originalScale = transform.localScale;
    }
	
	// Update is called once per frame
    void Update () {
        Color color = spriteRenderer.color;

        if(alpha > 0.5f) {
            alpha -= Time.deltaTime;
        }

        if(transform.localScale.x > originalScale.x) {
            transform.localScale = new Vector3(Mathf.Max(transform.localScale.x - Time.deltaTime * 10, originalScale.x),
                                               Mathf.Max(transform.localScale.y - Time.deltaTime * 10, originalScale.y),
                                               originalScale.z);
        }

        color.a = alpha;
        spriteRenderer.color = color;
	}

    void OnCollisionEnter2D () {
        alpha = 1.0f;

        transform.localScale = originalScale + new Vector3(5, 5, 0);
    }
}
