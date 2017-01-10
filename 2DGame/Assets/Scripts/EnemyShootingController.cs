using UnityEngine;
using System.Collections;

public class EnemyShootingController : MonoBehaviour {
	
	public Transform barrelGun;


	
	private Red_en_states _encontroller;

	
	public GameObject prefabEnemyShoot;
	
	public float fireRate = 0.5F;
	
	public float _forcescale = 1f;
	
	private new float _timetoshot; 
	
	private AudioSource adudiosource;
	
	// Use this for initialization
	void Start () {
		_timetoshot =  Time.time;
		_encontroller= this.GetComponent<Red_en_states> ();
	
		
		adudiosource= this.GetComponent<AudioSource> ();
		var MyColiders = this.GetComponents<Collider2D> ();

	

	
	}
	
	// Update is called once per frame
	void Update () {


		Vector2 postion = barrelGun.position;
		if (_encontroller.IsShooting() && postion.x!= 0 && postion.y != 0) {

			
			if(Time.time > _timetoshot  ) {
				
				_timetoshot =Time.time + fireRate;
				var bullet = Instantiate (prefabEnemyShoot, barrelGun.position, Quaternion.identity) as GameObject;

				var controller = bullet.GetComponent<BulletEnemyController>();


				controller.bullforce = new Vector2( _encontroller.GetLocalXScale()  , 0);
				//adudiosource.Play ();

			}
		}
		
		
	}
	
	

	
	
	
	
	
	
}

