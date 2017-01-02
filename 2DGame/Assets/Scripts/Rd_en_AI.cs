using UnityEngine;
using System.Collections;

public class Rd_en_AI : MonoBehaviour {

	private UnityEngine.Random random = new UnityEngine.Random();


	public enum RDStates{
		ilde = 0,
		walking = 1,
		jumping = 2,
		flying = 3,
		dying = 4
	}
	
	private float time ;
	
	public RDStates MyState = RDStates.ilde;

	public Red_en_states controller;

	public float Timetosetstate;
	// Use this for initialization
	void Start () {
		time = Time.time + Timetosetstate;
		controller = this.GetComponent<Red_en_states> ();


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		UnityEngine.Debug.Log("AI EN "+this.MyState);

		if(Mathf.Abs( controller.riigbody.velocity.y) != 0)
			MyState = RDStates.jumping;
		else if( MyState == RDStates.jumping &&  controller.riigbody.velocity.y == 0)
			MyState = RDStates.ilde;
		
	

		var range = UnityEngine.Random.Range (0, 100);
		if (time < Time.time) {
			time= Time.time +Timetosetstate;

			if (MyState == RDStates.ilde) {

				if(range< 0.25f){
					MyState = RDStates.walking;
					controller.Direction = new Vector2(1,0);
				}else	if(range> 0.25f){
					MyState = RDStates.walking;
					controller.Direction = new Vector2(-1,0);
				}
				else	if(range> 0.50f){
					controller.Force = new Vector2(0,20);
					MyState = RDStates.jumping;
				}

			}
			else if(MyState == RDStates.walking){

				//if(range< 0.10f){
					//controller.Direction = new Vector2(0,0);
					
					//MyState = RDStates.ilde;
					
				//}else
				{
					UnityEngine.Debug.Log("walking "+controller.Direction);
					controller.Direction = new Vector2(controller.Direction.x * -1, controller.Direction.y);

				}



			}
			else if(MyState == RDStates.jumping){
				if(controller.riigbody.velocity.y == 0)
					MyState = RDStates.ilde;
			}
		}
	}
}
