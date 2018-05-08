using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour {
	public Camera minimapCamera;
	Quaternion camRot;
	Vector3 camPos;
	public float speed = 3.0f;
	Quaternion transformRot;

	// Use this for initialization
	void Start () {
		transformRot = transform.rotation;
		minimapCamera = GameObject.FindGameObjectWithTag ("Minimap").GetComponent<Camera>();
		camRot = minimapCamera.transform.rotation;
		camPos = minimapCamera.transform.position;
	}

	// Update is called once per frame
	void Update () {
		//Click on the minimap
		if (Input.GetMouseButtonDown(0)) //Left-click
		{
			RaycastHit hit = new RaycastHit();
			Ray ray = minimapCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{  
				//Debug.Log("Hello World!");

				//Coordinates for the cube you clicked
				Vector3 local = hit.collider.gameObject.transform.position;

				//Move main camera to position
				Camera.main.transform.position = new Vector3 (local.x, local.y, local.z - 3);
				transform.rotation = transformRot;
				//Camera.main.transform.localEulerAngles = new Vector3 (0, 0, 0);
				//Camera.main.transform.rotation = new Quaternion (0, 0, 0, 0);

				//Rotation does not work due to the Update function of the Main Camera
				//Camera.main.transform.Rotate(new Vector3(90,0,0));
			}
		}

		//Control translation of camera
		if (Input.GetKey (KeyCode.O)) {
			minimapCamera.transform.Translate (Vector3.forward * speed * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.U)) {
			minimapCamera.transform.Translate (Vector3.back * speed * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.J)) {
			minimapCamera.transform.Translate (Vector3.left * speed * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.L)) {
			minimapCamera.transform.Translate (Vector3.right * speed * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.I)) {
			minimapCamera.transform.Translate (Vector3.up * speed * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.K)) {
			minimapCamera.transform.Translate (Vector3.down * speed * Time.deltaTime);
		}

		//Reset minimapCamera position
		if (Input.GetKey (KeyCode.M)) {
			minimapCamera.transform.position = camPos;
			minimapCamera.transform.rotation = camRot;
		}

	}

	//Not using this
	/*public void MinimapClick(){
		Rect miniMapRect = minimapCamera.GetComponent<RectTransform>().rect;
		Rect screenRect = new Rect(
			minimapCamera.transform.position.x, 
			minimapCamera.transform.position.y, 
			miniMapRect.width, miniMapRect.height);

		Vector3 mousePos = Input.mousePosition;
		mousePos.y -= screenRect.y;
		mousePos.x -= screenRect.x;

		Vector3 camPos = new Vector3(
			mousePos.x *  (MapWidth / screenRect.width),
			mousePos.y *  (MapHeight / screenRect.height),
			Camera.main.transform.position.z);
		Camera.main.transform.position = camPos;
	}*/
}
