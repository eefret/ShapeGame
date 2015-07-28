using UnityEngine;
using System.Collections;

public class AdaptToScreen : MonoBehaviour {

	public OffsetPosition offsetPosition;
	public float offsetX;
	public float offsetY;
	public float offsetZ;

	// Use this for initialization
	void Start () {
		//Posiciona el objecto en el punto deseado
		Vector3 cameraPos = new Vector3();
		switch (offsetPosition)	{
		case OffsetPosition.TopLeft:
			cameraPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
			break;
		case OffsetPosition.TopMiddle:
			cameraPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, 0));
			break;
		case OffsetPosition.TopRight:
			cameraPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
			break;
		case OffsetPosition.MiddleLeft:
			cameraPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
			break;
		case OffsetPosition.Middle:
			cameraPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
			break;
		case OffsetPosition.MiddleRight:
			cameraPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
			break;
		case OffsetPosition.BottomLeft:
			cameraPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
			break;
		case OffsetPosition.BottomMiddle:
			cameraPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, 0));
			break;
		case OffsetPosition.BottomRight:
			cameraPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
			break;
		}
		transform.position = new Vector3(cameraPos.x + offsetX, cameraPos.y + offsetY, offsetZ);
	}

	public enum OffsetPosition
	{
		TopLeft, TopMiddle, TopRight, MiddleLeft, Middle, MiddleRight, BottomLeft, BottomMiddle, BottomRight
	}
}
