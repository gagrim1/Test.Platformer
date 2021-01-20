using UnityEngine;
using System.Collections;

public class CameraFollow2D : MonoBehaviour {

	public RectTransform _rect;
	public float damping = 1.5f;
	public Vector2 offset = new Vector2(2f, 1f);
	public bool faceLeft;
	private Transform player;
	private int lastX;
	private float xMin;
	private float yMin;
	private float xMax;
	private float yMax;

	void Start ()
	{
		CalcBorders();
		offset = new Vector2(Mathf.Abs(offset.x), offset.y);
		FindPlayer(faceLeft);
	}

	void CalcBorders()
	{
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0)); // bottom-left corner
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1)); // top-right corner
		float height = (max - min).y;
		float width = (max - min).x;

		Vector3[] v = new Vector3[4];
		_rect.GetWorldCorners(v);
		xMin = v[0].x + width / 2;
		yMin = v[0].y + height / 2;
		xMax = v[2].x - width / 2;
		yMax = v[2].y - height / 2;
	}

	public void FindPlayer(bool playerFaceLeft)
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		lastX = Mathf.RoundToInt(player.position.x);
		if(playerFaceLeft)
		{
			transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
		}
		else
		{
			transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
		}
	}

	void Update () 
	{
		if(player)
		{
			int currentX = Mathf.RoundToInt(player.position.x);
			if(currentX > lastX) faceLeft = false; else if(currentX < lastX) faceLeft = true;
			lastX = Mathf.RoundToInt(player.position.x);

			Vector3 target;
			if(faceLeft)
			{
				target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
			}
			else
			{
				target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
			}
			Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);

			currentPosition.x = Mathf.Clamp(currentPosition.x, xMin, xMax);
			currentPosition.y = Mathf.Clamp(currentPosition.y, yMin, yMax);

			transform.position = currentPosition;
		}
	}
}