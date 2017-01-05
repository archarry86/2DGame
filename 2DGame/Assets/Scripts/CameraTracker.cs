using UnityEngine;
using System.Collections;

public class CameraTracker : MonoBehaviour {

	public Transform target;

	public Vector3 offset;

	public float smoothing;

	public bool validminy;

	private float minY;

	private Transform mytransform;


	private bool active = true;

	// Use this for initialization
	void Start () {
	
		mytransform = GetComponent<Transform> ();
		offset = mytransform.position - target.position; 
		minY = mytransform.position.y;

	}
	
	// Update is called once per frame
	void Update () {

		active = target != null;
		if (active) {

			Vector3 result = target.position + offset;


			if (validminy && result.y < minY) {
				result.y = minY;
			}

			mytransform.position = Vector3.Lerp (mytransform.position, result, smoothing * Time.deltaTime);
		}
	}


}
