using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour {
	Camera minimapCamera;
	Quaternion camRot;
	Vector3 camPos;

	// Use this for initialization
	void Start () {
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
				//Move main camera to position
				Camera.main.transform.rotation = camRot;
				Camera.main.transform.position = camPos;
			}
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
