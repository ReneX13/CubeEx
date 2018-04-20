using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public float speed = 1.0f;
	public float rot_speed =20.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.E)){
			transform.Rotate( Vector3.up * rot_speed * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.Q)){
			transform.Rotate(-Vector3.up * rot_speed * Time.deltaTime);
		}

		if(Input.GetKey(KeyCode.W)){
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.S)){
			transform.Translate(-Vector3.forward * speed * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.A)){
			transform.Translate(Vector3.left * speed * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.D)){
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.R)){
			transform.Translate(Vector3.up * speed * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.F)){
			transform.Translate(Vector3.down * speed * Time.deltaTime);
		}

	}
}
