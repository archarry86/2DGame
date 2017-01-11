using UnityEngine;
using System.Collections;



public class PlayerController : MonoBehaviour {


	public  float MaxHorizontalVelocity;


	private  bool onground;

	public bool OnGoround{
		get{
			return onground;
		}
	}

	public bool IsJumping{
		get{
			return	animator.GetBool ("isjumping");
	
		}
	}
    public  float radiousGround;
	public LayerMask groundLayer;
	public Transform groundCheck= null;
	public  float JumpHeight;
	
	private Transform transform;

	public Animator animator; 

	public Rigidbody2D rigidBody2D;

	private bool facingRight;

	private Collider2D []MyColiders;

	private PlayerHealth _plhealth;

	void Start () {
	
		transform= this.GetComponent<Transform> ();
		animator= this.GetComponent<Animator> ();
		rigidBody2D= this.GetComponent<Rigidbody2D> ();
		_plhealth = this.GetComponent<PlayerHealth> ();
		MyColiders = this.GetComponents<Collider2D> ();
		BoxCollider2D boxsize = MyColiders[0] as BoxCollider2D;


	}
	
	// Update is called once per frame
	void Update () {
		if (!_plhealth.IsAlive ())
			return;

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
		if (!_plhealth.IsAlive ())
			return;

		float yaxis =Input.GetAxis ("Vertical");
		float xaxis = Input.GetAxis("Horizontal");

		float absxaxis = Mathf.Abs (xaxis);

		animator.SetFloat ("yaxis",yaxis);
		animator.SetFloat ("xaxis",xaxis);
		//onliyingcolider.enabled = (yaxis < 0);
		onground = Physics2D.OverlapCircle(groundCheck.position,radiousGround , groundLayer.value);
		animator.SetBool ("onground",onground);

		if (onground && animator.GetBool ("isjumping")) {
			animator.SetBool ("isjumping",false);

		}


	

		animator.SetBool ("onground", onground);


	
		if (xaxis > 0 && facingRight) {
			Flip();
		} else if (xaxis < 0 &&  !facingRight){
			Flip();
		}

		animator.SetFloat ("xvel",absxaxis);

		//if the player is jumpping the direction on X remains
		Vector2 _vector = new Vector2 (xaxis * MaxHorizontalVelocity, rigidBody2D.velocity.y);
		rigidBody2D.velocity = _vector;

		EnablingBoxesLying (onground && xaxis ==0 && yaxis < 0);




	
	}

	private void Flip(){
		facingRight = !facingRight;
		Vector3 _localscale = transform.localScale;
		_localscale.x *= -1;
		transform.localScale = _localscale;
	}

	public int GetFlipNumber(){
		return facingRight ? -1 : 1;
	}

	public void EnablingBoxesLying(bool value){

		MyColiders [1].enabled = !value;
		MyColiders [2].enabled = !value;
		MyColiders [3].enabled = value;
	}
}
