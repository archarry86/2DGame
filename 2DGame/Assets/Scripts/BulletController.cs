using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {


	public Vector2 bullforce;
	private Vector2 originalpos;

	public float velocity;

	private Transform myTransform;


	//private float _gunvel = 0.00000000003f;

	// Use this for initialization
	void Start () {
		myTransform = this.GetComponent<Transform> ();
		originalpos = myTransform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector2 vectorpos = myTransform.position;//* _gunvel; 
		myTransform.position = vectorpos + bullforce* velocity * Time.deltaTime;


		Vector2 mynpos = myTransform.position;

		Vector2 _result = originalpos - mynpos ;

		_result = Camera.main.WorldToScreenPoint(_result);

		if ((Screen.width )< Mathf.Abs (_result.x) || (Screen.height)< Mathf.Abs (_result.y)) {
		
			Destroy(this.gameObject);
		}
	

	}


}
