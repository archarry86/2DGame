using UnityEngine;
using System.Collections;

public class CameraTracker : MonoBehaviour {

	public Transform target;

	public Vector3 offset;

	public float smoothing;

	public float minY;

	private Transform mytransform;

	// Use this for initialization
	void Start () {
	
		mytransform = GetComponent<Transform> ();
		offset = mytransform.position - target.position; 
		minY = mytransform.position.y;

	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 result = target.position + offset;

		//if (result.y < minY)
		{
			result.y = minY;
		}

		mytransform.position = Vector3.Lerp(mytransform.position ,result,smoothing* Time.deltaTime);

	}


}
