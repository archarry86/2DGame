using UnityEngine;
using System.Collections;

public class BulletEnemyController : MonoBehaviour {
	
	public float BulletDamage;
	
	public Vector2 bullforce;
	
	public float velocity;
	
	public Transform myTransform;
	


	//GameObject player = null:
	//private float _gunvel = 0.00000000003f;

	void Awake(){
		//player = GameObject.FindGameObjectWithTag("Player");
	}

	// Use this for initialization
	void Start () {

		myTransform = this.GetComponent<Transform> ();
		if (bullforce != null) {
			Vector2 local = myTransform.localScale;
			local.x *= bullforce.x;
			myTransform.localScale = local;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2	vectorpos = myTransform.position;//* _gunvel; 
		myTransform.position = vectorpos + bullforce* velocity * Time.deltaTime;
		
		
		Vector2 mynpos = myTransform.position;
		
		Vector2 _result = Camera.main.WorldToScreenPoint (vectorpos); 
		
		
		if (_result.x<0 || (Screen.width )< Mathf.Abs (_result.x) || (Screen.height)< Mathf.Abs (_result.y)||  _result.y<0)  {
			
			//UnityEngine.Debug.Log("DESTROY BULLET  "+_result.ToString());
			Destroy(this.gameObject);
		}
		
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		//UnityEngine.Debug.Log ("OnTriggerEnter2D "+other.gameObject.tag + " " + other.gameObject.layer);
		if (other.gameObject.tag== "Player"){// == 11){
			//processDamage(other.gameObject);

			var palyerhealth = other.gameObject.GetComponent<PlayerHealth>();
			if(palyerhealth.IsAlive()){
				
				palyerhealth.AddDamage(BulletDamage);;
				Destroy(this.gameObject);
			}
		}
	}
	
	void OnTriggerStay2D(Collider2D other) {
		//UnityEngine.Debug.Log ("OnTriggerStay2D "+other.gameObject.tag + " " + other.gameObject.layer);
		if (other.gameObject.tag == "Player"){//.layer == 11){
			//processDamage(other.gameObject);

			var palyerhealth = other.gameObject.GetComponent<PlayerHealth>();
			if(palyerhealth.IsAlive()){
				
				palyerhealth.AddDamage(BulletDamage);;
				Destroy(this.gameObject);
			}
		}
	}

	private void processDamage(GameObject gameobject){

		//UnityEngine.Debug.Log (gameObject.tag + " " + gameObject.layer);
		var palyerhealth = gameObject.GetComponent<PlayerHealth>();
		if(palyerhealth.IsAlive()){

			palyerhealth.AddDamage(BulletDamage);;
			Destroy(this.gameObject);
		}
	}

	/*
	void OnGUI() {

		Vector2 aux = Camera.main.WorldToScreenPoint (vectorpos); 
	
		GUI.Label(new Rect(aux.x, Screen.height-aux.y, 200, 20), aux.ToString()+" "+vectorpos.ToString());
	}*/
	
	
}
