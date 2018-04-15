using UnityEngine;
using System.Collections;

public class CamController : MonoBehaviour {
	public float speed = 5.0f;
	public float rot_speed = 100.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (0) && Input.GetKey(KeyCode.LeftShift)) {
			transform.Translate(Vector3.forward * rot_speed * Time.deltaTime);
		}
		else if (Input.GetMouseButton (1) && Input.GetKey(KeyCode.LeftShift)) {
			transform.Translate(- Vector3.forward * speed * Time.deltaTime);
		}
		else if (Input.GetMouseButton (1) && Input.GetKey(KeyCode.LeftControl)) {
			transform.Translate( Vector3.right * speed * Time.deltaTime);
		}
		else if (Input.GetMouseButton (0) && Input.GetKey(KeyCode.LeftControl)) {
			transform.Translate( Vector3.left * speed * Time.deltaTime);
		}
		else if (Input.GetMouseButton (1) && Input.GetKey(KeyCode.LeftAlt)) {
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		else if (Input.GetMouseButton (0) && Input.GetKey(KeyCode.LeftAlt)) {
			transform.position += Vector3.up * speed * Time.deltaTime;
		}

		else if (Input.GetMouseButton (0)) {
			Debug.Log ("hit!   " + Input.GetAxis ("Mouse X") +"   " +Input.GetAxis ("Mouse Y") );

			transform.Rotate( new Vector3 (Input.GetAxis ("Mouse Y")*rot_speed*Time.deltaTime, 0.0f, 0.0f));
		}

		/*else if (Input.GetMouseButton (0)) {
			Debug.Log ("hit!   " + Input.GetAxis ("Mouse X") +"   " +Input.GetAxis ("Mouse Y") );

			transform.position += new Vector3 (Input.GetAxis ("Mouse X")*speed*Time.deltaTime, Input.GetAxisRaw ("Mouse Y")*speed*Time.deltaTime, 0.0f);
		}*/

	}
}
