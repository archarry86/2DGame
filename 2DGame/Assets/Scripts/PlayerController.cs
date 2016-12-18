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

	//TODO HOW TO ENABLED AND DISABLED SOME COLIDERS
	//private BoxCollider2D onliyingcolider;

	// Use this for initialization
	void Start () {
	
		transform= this.GetComponent<Transform> ();
		animator= this.GetComponent<Animator> ();
		rigidBody2D= this.GetComponent<Rigidbody2D> ();

	
		/*var coliders = this.GetComponents<BoxCollider2D> (); 
		onliyingcolider = coliders[coliders.Length-1];
		onliyingcolider.enabled = false;
*/
	}
	
	// Update is called once per frame
	void Update () {
		
		if(onground){
			
			if (Input.GetAxis ("Jump") > 0.1  ) {
				animator.SetBool ("isjumping",true);
				rigidBody2D.AddForce (new Vector2 (0, JumpHeight));
				onground = false;
				
				animator.SetBool ("onground", onground);
				//adds a force to jump
				
			} 
			
		}
	}
	void FixedUpdate () {

	
	
		float yaxis =Input.GetAxis ("Vertical");
		animator.SetFloat ("yaxis",yaxis);

		//onliyingcolider.enabled = (yaxis < 0);

		onground = Physics2D.OverlapCircle (groundCheck.position, radiousGround);

		animator.SetBool ("onground", onground);

		//if(onground)
		//animator.SetBool ("isjumping", false);

		if(onground){
			
			if (Input.GetAxis ("Jump") > 0.1  ) {
				animator.SetBool ("isjumping",true);
				rigidBody2D.AddForce (new Vector2 (0, JumpHeight));
				onground = false;
				
				animator.SetBool ("onground", onground);
				//adds a force to jump
				
			} 
			
		}
		//walking
		float movx = Input.GetAxis("Horizontal");

	
		if (movx > 0 && facingRight) {
			Flip();
		} else if (movx < 0 &&  !facingRight){
			Flip();
		}

		animator.SetFloat ("xvel",Mathf.Abs( movx));

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
