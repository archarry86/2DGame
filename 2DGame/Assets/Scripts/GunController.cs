using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public Transform barrelGun;
	private Vector2 defPosBarrGun;
	public Vector2[][] bulletPositions= new Vector2[3][];

	private Vector2[][] bulletVelocities= new Vector2[3][];

	private PlayerController _plcontroller;

	private Transform _playerTransform;

	private BoxCollider2D boxsize ;

	public Transform cameraTransform;

	public int IndexBarrelGun =0;

	public GameObject prefabBullet;

	public float _time = 0.5f;

	public float _forcescale = 1f;
	
	private float _timetoshot; 

	private AudioSource adudiosource;

	// Use this for initialization
	void Start () {
		_timetoshot =  Time.time;
		_plcontroller= this.GetComponent<PlayerController> ();
		_playerTransform = this.GetComponent<Transform> ();

		adudiosource= this.GetComponent<AudioSource> ();
		 var MyColiders = this.GetComponents<Collider2D> ();
		boxsize = MyColiders[0] as BoxCollider2D;
		
		/*var coliders = this.GetComponents<BoxCollider2D> (); 
		onliyingcolider = coliders[coliders.Length-1];
		onliyingcolider.enabled = false;
*/
		//GameObject auxgo =  this.GetComponent<GameObject>("barrelgun") ;
		
		//Vector3 localscale = auxgo.transform.localScale;
		defPosBarrGun = barrelGun.localPosition;
		bulletPositions [0] = new Vector2[]{new Vector2 (0, 0),
			new Vector2 (-boxsize.size.x / 2, boxsize.size.y / 2),
			new Vector2 (0, -boxsize.size.y / 2)};
		bulletPositions [1] = new Vector2[]{new Vector2 (0, 0),
			new Vector2 (0, boxsize.size.y / 2),
			new Vector2 (0, -boxsize.size.y / 2)};
		bulletPositions [2] = new Vector2[]{new Vector2 (0, 0),
			new Vector2 (0, boxsize.size.x / 2),
			new Vector2 (0, - boxsize.size.y / 2)};

		bulletVelocities [0] = new Vector2[]{
			new Vector2 (1, 0),
			new Vector2 (0, 1),
			new Vector2 (1,0)
		};

		bulletVelocities [1] = new Vector2[]{
			new Vector2 (1, 0),
			new Vector2 (1,1),
			new Vector2 (1,-1)
		};
		bulletVelocities [2] = new Vector2[]{
			new Vector2 (1, 0),
			new Vector2 (1, 1),
			new Vector2 (1,0)
		};

	}
	
	// Update is called once per frame
	void Update () {

		float yaxis =Input.GetAxis ("Vertical");
		float xaxis = Input.GetAxis("Horizontal");

		float absxaxis = Mathf.Abs (xaxis);

		if (absxaxis == 0 ) {
			
			if( yaxis == 0)
			{
				IndexBarrelGun = 0;
			}
			else if( yaxis > 0)
			{
				IndexBarrelGun =1;
			}
			else if( yaxis < 0)
			{
				IndexBarrelGun =2;
			}
			
		}
		else if (absxaxis > 0 ) {

			if( yaxis == 0)
			{
				IndexBarrelGun =3;
			}
			else if( yaxis > 0)
			{
				IndexBarrelGun =4;
			}
			else if( yaxis < 0)
			{
				IndexBarrelGun =5;
				UnityEngine.Debug.Log("FORCE 5 "+GetGunForce().ToString());

				UnityEngine.Debug.Log("X"+IndexBarrelGun/bulletVelocities.Length+" Y"+IndexBarrelGun%bulletVelocities[0].Length);
			}
		}
		UnityEngine.Debug.Log("INDEX "+IndexBarrelGun);

		barrelGun.localPosition = defPosBarrGun+GetBarrelGunPosition();
		var _fire = Input.GetAxis ("Fire1");
	
		//UnityEngine.Debug.Log("Fire 1 "+val.ToString());

		if (Time.time > _timetoshot && _fire > 0) {
			_timetoshot += _time;

			var bullet = 	Instantiate(prefabBullet,barrelGun.position , Quaternion.identity) as GameObject ;
			//UnityEngine.Debug.Log(bullet.GetType().Name);
			if(bullet!= null){
			

				//UnityEngine.Debug.DrawLine(barrelGun.localPosition, GetGunForce());

				var bullContro = 	bullet.GetComponent<BulletController>();
				bullContro.bullforce = GetGunForce();

			}
			adudiosource.Play();
		}
	}


	private Vector2 GetBarrelGunPosition(){
		
		var position = bulletPositions[IndexBarrelGun/bulletPositions.Length][IndexBarrelGun%bulletPositions.Length];
		if(IndexBarrelGun > 2)
			position.x *= _plcontroller.GetFlipNumber ();
		return position;
	}

	
	private Vector2 GetGunForce(){
		
		var position = bulletVelocities[IndexBarrelGun/bulletVelocities.Length][IndexBarrelGun%bulletVelocities[0].Length];
		//if(IndexBarrelGun != 0 &&IndexBarrelGun != 3 )
		position.x *= _plcontroller.GetFlipNumber ()*_forcescale;
		return position;
	}
	





}
