using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {


	public Vector2 bullforce;
	private Vector2 originalpos;

	public float velocity;

	private Transform myTransform;

	Vector2 vectorpos ;
	//private float _gunvel = 0.00000000003f;

	// Use this for initialization
	void Start () {
		myTransform = this.GetComponent<Transform> ();
		originalpos = myTransform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		 vectorpos = myTransform.position;//* _gunvel; 
		myTransform.position = vectorpos + bullforce* velocity * Time.deltaTime;


		Vector2 mynpos = myTransform.position;

		Vector2 _result = Camera.main.WorldToScreenPoint (vectorpos); 

	
		if (_result.x<0 || (Screen.width )< Mathf.Abs (_result.x) || (Screen.height)< Mathf.Abs (_result.y)||  _result.y<0)  {
		
			//UnityEngine.Debug.Log("DESTROY BULLET  "+_result.ToString());
			Destroy(this.gameObject);
		}
	

	}

	void OnGUI() {

		Vector2 aux = Camera.main.WorldToScreenPoint (vectorpos); 
	
		GUI.Label(new Rect(aux.x, Screen.height-aux.y, 200, 20), aux.ToString()+" "+vectorpos.ToString());
	}


}
