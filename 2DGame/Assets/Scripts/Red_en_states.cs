using UnityEngine;
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
	public bool isFlying =false;

	public bool isShoting = false;


	public GameObject player;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		transform = this.GetComponent<Transform> ();
		Direction = new Vector2 (0, 0);
		facingRight = false;
		rigidbody = this.GetComponent<Rigidbody2D> ();
		healthcontroller = this.GetComponent<EnemyHealth> ();
		Force = new Vector2 (0, 0);

		player = GameObject.FindGameObjectWithTag("Player");


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {






		 onground = Physics2D.OverlapCircle(GroundChecker.position,radiousGround , groundLayer.value);
		//UnityEngine.Debug.Log ("en onground?" +onground);
		if (healthcontroller.IsAlive ()) {

			animator.SetBool ("isshooting", isShoting);
			if (isShoting) {



				return ;
			}


			if (onground && Force.y > 0) {

				//transform.position = new Vector2(transform.position.x, transform.position.y+10);
				rigidbody.AddForce (new Vector2 (0, Force.y));
			
				onground = false;
				Force = Vector2.zero;
			}
			animator.SetFloat ("xaxis", Mathf.Abs (Direction.x));

	

			if (!facingRight && Direction.x > 0) {
				Flip ();
			} else 	if (facingRight && Direction.x < 0) {
				Flip ();
			}


			animator.SetFloat ("yvel", rigidbody.velocity.y);

			Vector2 _vector = new Vector2 (Direction.x * MaxHorizontalVelocity, (isFlying ? Direction.y * MaxHorizontalVelocity : rigidbody.velocity.y));
			rigidbody.velocity = _vector;

			animator.SetBool ("onground", onground);

			if (isFlying && onground) {
				//this means as soon as it lands or groundcheker marks true then 
				//the state change
				SetFlyingMode (false);
			}

			animator.SetBool ("isflying", isFlying);

			//Direction = Vector2.zero;

			//TODO DELETE JUST  TO PROVE
			if (transform.position.y < -10) {
				//transform.position = new Vector2(transform.position.x, 25);
			}
		} else {
			animator.SetBool ("onground", onground);
			if (isFlying ) {
				//this means as soon as it lands or groundcheker marks true then 
				//the state change
				SetFlyingMode (false);
			}


		}

	}

	private void Flip(){
		facingRight = !facingRight;
		Vector3 _localscale = transform.localScale;
		_localscale.x *= -1;
		transform.localScale = _localscale;
	}

	public float GetLocalXScale(){
		Vector3 _localscale = transform.localScale;
		return	_localscale.x /Mathf.Abs(_localscale.x) ;
	}

	public void SetFlyingMode(bool mode){
		if (mode) {
			isFlying = mode;
			rigidbody2D.gravityScale = 0;
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
		} else {
			isFlying = mode;
			rigidbody2D.gravityScale = 1;
		}
	}
	public Vector2 Direction{ 
		get;
		set;
	}

	public Vector2 Force{ 
		get;
		set;
	}

	public bool IsShooting(){
		return animator.GetBool ("isshooting");
	}

	public void AimPlayer(){
		if(player!= null){

			Vector3 VEC = player.GetComponent<PlayerController>().transform.position;
			var result = (transform.position - VEC).x;
			//UnityEngine.Debug.Log("AimPlayer  "+transform.position.ToString()+" "+VEC.ToString()+" "+result +" "+Time.time);
			if(transform.localScale.x > 0 && result > 0){
				//UnityEngine.Debug.Log("FLIP RIGHT ");
				Flip();
			}else if(transform.localScale.x < 0 && result < 0){
				//UnityEngine.Debug.Log("FLIP LEFT ");
				Flip();
			}
		}
	}
}


