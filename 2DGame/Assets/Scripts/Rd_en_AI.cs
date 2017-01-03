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

	
		
	

		var range = UnityEngine.Random.Range (0, 100);
		if (time < Time.time) {
			time= Time.time +Timetosetstate;

			if (MyState == RDStates.ilde) {
			
				if(range> 0 && range< 40){

					controller.Force = new Vector2(0,90);
					MyState = RDStates.jumping;

			
				}else	if(range>= 40 && range <=50){
					MyState = RDStates.walking;
					controller.Direction = new Vector2(-1,0);
				}
				else if(range> 50){
					MyState = RDStates.walking;
					controller.Direction = new Vector2(1,0);
				}

			}
			else if(MyState == RDStates.walking){

				 if(range< 40){
					controller.Force = new Vector2(0,90);
					MyState = RDStates.jumping;
				}
			/*	else if(range< 10){
					controller.Direction = new Vector2(0,0);
					controller.Force = new Vector2(0,0);
					
					MyState = RDStates.ilde;
					
				}*/else
				{
					//UnityEngine.Debug.Log("walking "+controller.Direction);
					controller.Direction = new Vector2(controller.Direction.x * -1, 0);

				}



			}
			else if(MyState == RDStates.jumping){

				if(controller.onground)
					MyState = RDStates.ilde;
				else{
					//UnityEngine.Debug.Log("changing direction "+controller.Direction);
					if(range> 0.0 && range< 50){
						controller.Direction = new Vector2(1,0);
					}else	if(range>= 50 && range <= 100){
						controller.Direction = new Vector2(-1,0);
					}

				}
			}
		}
	}
}
