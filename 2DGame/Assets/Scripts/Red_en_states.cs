using UnityEngine;
using System.Collections;

public class Red_en_states : MonoBehaviour {


	public Animator animator;
	public Transform transform;
	public Rigidbody2D riigbody;

	public bool facingRight;
	public  float MaxHorizontalVelocity = 1.5f;

	public EnemyHealth healthcontroller;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		transform = this.GetComponent<Transform> ();
		Direction = new Vector2 (0, 0);
		facingRight = false;
		riigbody = this.GetComponent<Rigidbody2D> ();
		healthcontroller = this.GetComponent<EnemyHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {

		if (healthcontroller.IsAlive()) {

			if (riigbody.velocity.y == 0 && Force.y != 0) {
				riigbody.AddForce (new Vector2 (0, Force.y));
			}
			animator.SetFloat ("xaxis", Mathf.Abs (Direction.x));

	

			if (!facingRight && Direction.x > 0) {
				Flip ();
			} else 	if (facingRight && Direction.x < 0) {
				Flip ();
			}


			animator.SetFloat ("yvel", riigbody.velocity.y);

			Vector2 _vector = new Vector2 (Direction.x * MaxHorizontalVelocity, riigbody.velocity.y);
			riigbody.velocity = _vector;

			animator.SetBool ("onground", riigbody.velocity.y == 0);
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


