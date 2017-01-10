using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float Health;
	
	private Animator animator;
	public float timetoDestroy = 0.001f;
	private float _time;
	private PlayerController controller;


	private bool Isvulnerable = false;
	
	private Transform transform;
	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController> ();
		animator = GetComponent<Animator> ();

		transform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!IsAlive() && !animator.GetBool("isdying")) {
			
			animator.SetBool("isdying", true);
		}
		
		if( _time == 0 &&animator.GetBool("isdying") && controller.OnGoround)
			_time = Time.time+timetoDestroy;
		
	}
	
	void FixedUpdate(){
		
		if (!IsAlive() ){
			
			if(_time!= 0 && _time < Time.time  )
				Destroy(this.gameObject);
			else
			if (transform.position.y < -10) {
				//transform.position = new Vector2(transform.position.x, 25);
				Destroy(this.gameObject);
			}
		}
	}
	
	public void AddDamage(float damage){
		if(Isvulnerable)
		Health -= damage;
		
	}
	
	public void AddHealth(float health){
		Health += health;
	}
	
	
	public bool IsAlive(){
		
		return Health > 0.0f;
	}
}
