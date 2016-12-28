using UnityEngine;
using System.Collections;

public class HitController : MonoBehaviour {


	SpriteRenderer render ;

	public Material hitmaterial;
	Material defmaterial;

	int counter = 0;
	private float _time;
	private bool ishit = false;
	// Use this for initialization
	void Start () {
		render = 	this.GetComponent<SpriteRenderer> ();
		defmaterial = render.material;
	}
	
	// Update is called once per frame
	void Update  () {
	
		/*counter ++;
		if ((counter % 5) == 0) {
			render.material = hitmaterial;
		} else {
			render.material = defmaterial;
		}*/
		if (_time < Time.time) {
			ishit = false;
			render.material = defmaterial;
		}


	}

	public void HitCharacter(){
		if (!ishit) {
			ishit = true;
			_time = Time.time + 0.2f;
			render.material = hitmaterial;
		}
	}
}
