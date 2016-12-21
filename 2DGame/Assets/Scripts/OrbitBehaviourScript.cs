using UnityEngine;
using System.Collections;

public class OrbitBehaviourScript : MonoBehaviour {


	public Transform tbullet = null;
	// Use this for initialization
	float var = 0;

	float magnitude = 0;

float	Degree2Rad =  (Mathf.PI * 2.0f )/ 360.0f;

	void Start () {

		magnitude = 0.33f;

		UnityEngine.Debug.Log (" magnitude "+magnitude);
	}

	// Update is called once per frame
	void Update () {
	
		float angresult = var + 1;
		Vector3 ROTA = tbullet.localPosition;


		ROTA.x = Mathf.Cos (angresult * Degree2Rad)*magnitude;// + " " + 
		ROTA.y = Mathf.Sin (angresult * Degree2Rad)*magnitude;

		tbullet.localPosition = ROTA;
		UnityEngine.Debug.Log (" position "+ROTA);

		//tbullet.localPosition = Quaternion.LookRotation (new Vector3 (0, 0, angresult)).eulerAngles * magnitude;

		var = angresult;
		var = var % 360;
	}
}
