using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public  float MaxHorizontalVelocity;


	private  bool onground;
	public  float radiousGround;
	public LayerMask groundLayer;
	public Transform groundCheck= null;
	public  float JumpHeight;


	public Transform transform;

	public Animator animator; 

	public Rigidbody2D rigidBody2D;

	private bool facingRight;

	// Use this for initialization
	void Start () {
	
		transform= this.GetComponent<Transform> ();
		animator= this.GetComponent<Animator> ();
		rigidBody2D= this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis ("Jump") > 0 && onground ) {
			onground = false;
			animator.SetBool ("onGround", onground);
			//adds a force to jump
			rigidBody2D.AddForce (new Vector2 (0, JumpHeight));
		} 

	}
	void FixedUpdate () {

		//jumpping 
	    //if(!onground)
		{

			//Debug.Log ("FixedUpdate");
			//jumpping
			onground = Physics2D.OverlapCircle (groundCheck.position, radiousGround);

			animator.SetBool ("onGround", onground);
		}
		//walking
		float movx = Input.GetAxis("Horizontal");

	
		if (movx > 0 && facingRight) {
			Flip();
		} else if (movx < 0 &&  !facingRight){
			Flip();
		}

		animator.SetFloat ("horSpeed",Mathf.Abs( movx));

		//if the player is jumpping the direction on X remains
		Vector2 _vector = new Vector2 (movx * MaxHorizontalVelocity, rigidBody2D.velocity.y);
		rigidBody2D.velocity = _vector;

	



	}

	private void Flip(){
		facingRight = !facingRight;
		Vector3 _localscale = transform.localScale;
		_localscale.x *= -1;
		transform.localScale = _localscale;
	}
}
