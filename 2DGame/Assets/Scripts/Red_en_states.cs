﻿using UnityEngine;
using System.Collections;

public class Red_en_states : MonoBehaviour {


	public Animator animator;
	public Transform transform;
	public Rigidbody2D rigidbody;

	public bool facingRight;
	public  float MaxHorizontalVelocity = 1.5f;

	public EnemyHealth healthcontroller;
	public Transform GroundChecker ;

	public  float radiousGround;
	public LayerMask groundLayer;
	public bool onground ;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		transform = this.GetComponent<Transform> ();
		Direction = new Vector2 (0, 0);
		facingRight = false;
		rigidbody = this.GetComponent<Rigidbody2D> ();
		healthcontroller = this.GetComponent<EnemyHealth> ();
		Force = new Vector2 (0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		 onground = Physics2D.OverlapCircle(GroundChecker.position,radiousGround , groundLayer.value);
		UnityEngine.Debug.Log ("en onground?" +onground);
		if (healthcontroller.IsAlive()) {


			if (onground && Force.y>  0) {

				//transform.position = new Vector2(transform.position.x, transform.position.y+10);
				rigidbody.AddForce(new Vector2(0,Force.y));
			
				onground = false;
				Force = new Vector2(0,0);
			}
			animator.SetFloat ("xaxis", Mathf.Abs (Direction.x));

	

			if (!facingRight && Direction.x > 0) {
				Flip ();
			} else 	if (facingRight && Direction.x < 0) {
				Flip ();
			}


			animator.SetFloat ("yvel", rigidbody.velocity.y);

			Vector2 _vector = new Vector2 (Direction.x * MaxHorizontalVelocity, rigidbody.velocity.y);
			rigidbody.velocity = _vector;

			animator.SetBool ("onground", onground);
			//TODO DELETE JUST  TO PROVE
			if(transform.position.y < -10){
				transform.position = new Vector2(transform.position.x, 25);
			}
		}

	}

	private void Flip(){
		facingRight = !facingRight;
		Vector3 _localscale = transform.localScale;
		_localscale.x *= -1;
		transform.localScale = _localscale;
	}


	public Vector2 Direction{ 
		get;
		set;
	}

	public Vector2 Force{ 
		get;
		set;
	}
}


