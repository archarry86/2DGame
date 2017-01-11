using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public  int NUMBER = 1;
	public GameObject prefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var result = 	GameObject.FindGameObjectsWithTag ("Enemy");
		
		UnityEngine.Debug.Log ("GAMECONTROLLER ENEMIES "+result.Length);
		
		if (result.Length == 0||  result.Length<NUMBER) {
			NUMBER++;
			for(int i = 0;i<NUMBER;i++){

			float rdX = UnityEngine.Random.Range(0,200)%10;
			float rdY = UnityEngine.Random.Range(0,200)%10;
			Instantiate(prefab, new Vector3(rdX,rdY,0),Quaternion.identity);
			}
		}
	}
}