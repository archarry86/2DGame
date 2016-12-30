using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {



	public float Health;

	private Animator animator;
	public float timetoDestroy = 0.001f;
	private float _time;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!IsAlive() && !animator.GetBool("isdying")) {
			_time = Time.time+timetoDestroy;
			animator.SetBool("isdying", true);
		}

	}

	void FixedUpdate(){

		 if (!IsAlive() ){
			
			if(_time < Time.time  )
				Destroy(this.gameObject);
		}
	}

	public void AddDamage(float damage){

		Health -= damage;

	}

	public void AddHealth(float health){
		Health += health;
	}


	public bool IsAlive(){

		return Health > 0.0f;
	}

}
