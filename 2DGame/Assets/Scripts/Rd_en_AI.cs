using UnityEngine;
using System.Collections;

public class Rd_en_AI : MonoBehaviour {

	private UnityEngine.Random random = new UnityEngine.Random();


	public enum RDStates{
		ilde = 0,
		walking = 1,
		jumping = 2,
		flying = 3,
		dying = 4,
		shooting= 5,
	}
	
	private float time ;
	
	public RDStates MyState = RDStates.ilde;

	public RDStates PreviousMyState = RDStates.ilde;

	public Red_en_states controller;
	public EnemyHealth enemyHealth;

	public float Timetosetstate;

	public float Timetosetstatepursuingoints;

	public float Timetostateflying;

	private int pointtopursue;

	private Vector2[] _points = new Vector2[3]{
	//Vector2 point1 = 
		new Vector2(-10,-6),
		//Vector2 point2 = 
		new Vector2(-10,4),
		//Vector2 point3 = 
		new Vector2(-1,-2)
	};

	public bool pursuingpoints;
	// Use this for initialization
	void Start () {
		time = Time.time + Timetosetstate;
		controller = this.GetComponent<Red_en_states> ();
		enemyHealth = this.GetComponent<EnemyHealth> ();
		pursuingpoints = false;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//UnityEngine.Debug.Log("AI EN "+this.MyState);



		if (!enemyHealth.IsAlive ()) {
			MyState = RDStates.dying;

			return;
		}

	
		
	

		var range = UnityEngine.Random.Range (0, 200)%100;
		if (time < Time.time) {
			//default timetostate
			time= Time.time +Timetosetstate;

			if(MyState == RDStates.shooting && !controller.isShoting ){

				MyState= PreviousMyState;
			}
			else if(MyState == RDStates.shooting && controller.isShoting ){
				
				controller.isShoting = false;
				return;
			}

			else if(range % 7 == 0 ){//%2 == 0 && range %7 == 0){

				controller.isShoting = true;
				controller.AimPlayer();
				PreviousMyState = MyState;
				MyState = RDStates.shooting;
				return ;
			}

		

			if (MyState == RDStates.ilde) {
			
				/*if(range> 0 && range< 40){

					controller.Force = new Vector2(0,90);
					MyState = RDStates.jumping;

			
				}else	*/if(range>= 40 && range <=50){
					MyState = RDStates.walking;
					controller.Direction = new Vector2(-1,0);
				}
				else if(range> 50){
					MyState = RDStates.walking;
					controller.Direction = new Vector2(1,0);
				}

				if (!controller.onground) {
					MyState = RDStates.jumping;
				}

			}
			else if(MyState == RDStates.walking){

					 /*if(range< 40){
					controller.Force = new Vector2(0,90);
					MyState = RDStates.jumping;
				}
				else */if(range< 10){
					controller.Direction = new Vector2(0,0);
					controller.Force = new Vector2(0,0);
					
					MyState = RDStates.ilde;
					
				}else
				{
					//UnityEngine.Debug.Log("walking "+controller.Direction);
					controller.Direction = new Vector2(controller.Direction.x * -1, 0);

				}


				if (!controller.onground) {
					MyState = RDStates.jumping;
				}


			}
			else if(MyState == RDStates.jumping){

				if(controller.onground)
					MyState = RDStates.ilde;
				else{
					//UnityEngine.Debug.Log("changing direction "+controller.Direction);
					if(range> 0 && range< 50){
						controller.Direction = new Vector2(1,0);
					}else	if(range>= 50 && range <= 90){
						controller.Direction = new Vector2(-1,0);
					}
					else if(range> 90){
						controller.SetFlyingMode(true);
						MyState = RDStates.flying;
					}

					
					if(controller.transform.position.y < -6){
						time = Time.time+ Timetosetstatepursuingoints;
						MyState = RDStates.flying;
						pursuingpoints = true;
					
						controller.SetFlyingMode(true);
					}

				}
			}
			else if(MyState == RDStates.flying){

				time = Time.time +Timetostateflying;

				if(controller.onground){
					MyState = RDStates.ilde;
					controller.SetFlyingMode(false);
				}
				else{

					if(!pursuingpoints){
						if(range> 0 && range< 40){
							controller.Direction = new Vector2(1,0);
						}else	if(range>= 40 && range <= 80){
							controller.Direction = new Vector2(-1,0);
						}
						else	if(range>= 80 && range <= 85){
							controller.Direction = Vector2.zero;
						}
						//
						else if(  range > 85 ){
							time = Time.time +Timetosetstate;
							controller.SetFlyingMode(false);
							MyState = RDStates.jumping;
						}

						if(controller.transform.position.y <= -6){
							time = Time.time+ Timetosetstatepursuingoints;
							pursuingpoints = true;
						
						}
					}else{

						/*
						 * 	Vector2 point1 = new Vector2(-9,0);
							Vector2 point2 = new Vector2(0,2);
							Vector2 point3 = new Vector2(-1,0)
						 */ 
						int val = 	(int)pointtopursue % 3;
						
						Vector2 dif= Vector2.zero;
						Vector2 PAUX= _points[val];
						
						dif=   PAUX - new Vector2( controller.transform.position.x, controller.transform.position.y)  ;
						
						if( Mathf.Abs(dif.magnitude) > 1.3f)
						{
							dif.Normalize();
							controller.Direction = dif; // * Time.deltaTime;//new Vector2(dif.x != 0.0f? dif.x / Mathf.Abs(dif.x): 0  ,dif.y != 0.0f? dif.y / Mathf.Abs(dif.y):0);
						}
						else
						{
							controller.transform.position = PAUX;
							var aux = pointtopursue;
							pointtopursue = (pointtopursue+1) % 3;
							controller.Direction = Vector2.zero;
							if(aux == 2 && pointtopursue == 0)
								pursuingpoints = false;

						}

					}

				}
			}
		}

	    
	}
}
