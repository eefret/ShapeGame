using UnityEngine;
using System.Collections;

public class OnClickListener : MonoBehaviour {

    private static RuntimePlatform platform = Application.platform;
	
	// Update is called once per frame
	void Update () {
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) {
            if (Input.touchCount > 0) {
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    checkTouch(Input.GetTouch(0).position);
                }
            }
        } else if(platform == RuntimePlatform.WindowsPlayer) {
            if (Input.GetMouseButtonDown(0)) {
                checkTouch(Input.GetTouch(0).position);
            }   
        }

        if (Input.GetMouseButtonDown(0)) {
            checkTouch(Input.mousePosition);
        }
	}

    void checkTouch(Vector3 pos) {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);
        if (hit) {
            Debug.Log(hit.transform.gameObject.name);
            hit.transform.gameObject.SendMessage("Clicked", SendMessageOptions.DontRequireReceiver);
        } else {
            RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
            if (ray.collider != null) {
                ray.transform.gameObject.SendMessage("Clicked", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
