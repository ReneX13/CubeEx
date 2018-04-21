using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Set Layer for object and all its children.
public static class UtilityClasses {
	public static void SetLayerRecursively(this GameObject obj, int layer)
	{
		obj.layer = layer;

		foreach (Transform child in obj.transform) 
		{
			child.gameObject.SetLayerRecursively (layer);
		}
	}
}

public class CubeView : MonoBehaviour {
	GameObject cubeToView;
	public Quaternion myCamRot1;
	public Vector3 myCamPos1;
	public Quaternion myCubeRot;
	public float mouseSpeed = 70.0f;
	GameObject[] allObjects;
	GameObject tmpCube;
	public int layerToShow = 30;
	Camera controlCamera;
	public float rot_speed = 20.0f;
	public float mouseScrollSpeed = 15.0f;
	public float translationSpeed = 1.0f;

	public bool shift = false;
	public bool w = false;
	public bool s = false;
	public bool a = false;
	public bool d = false;

	// Use this for initialization
	void Start () {
		allObjects = FindObjectsOfType<GameObject> ();
		controlCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		myCamRot1 = controlCamera.transform.rotation;
		myCamPos1 = controlCamera.transform.position;
		controlCamera.GetComponent<Controller> ().enabled = false;
		Camera.main.transform.position = new Vector3(0,0,-3);
		//Camera.main.transform.rotation = new Quaternion (0,0,0,0);
		SceneManager.SetActiveScene (SceneManager.GetSceneByName ("Object_View"));
		cubeToView = GameObject.Find ("cubeSelected");
		tmpCube = (GameObject)Instantiate (cubeToView, new Vector3(0,0,0), new Quaternion());
		myCubeRot = tmpCube.transform.rotation;
		//tmpCube.layer = layerToShow;
		tmpCube.SetLayerRecursively(layerToShow);
		tmpCube.name = "SelectedCube";
		cubeToView.name = "CubeSeen";

		foreach (GameObject obj in allObjects) 
		{
			if((obj.name.StartsWith("Cube Template")) || (obj.name.StartsWith("Button_Panel")) 
				|| (obj.name.StartsWith("Minimap")) && ((obj.layer == 0) || (obj.layer == 8)))
				obj.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		/*
		// Smoothly tilts a transform towards a target rotation.
		float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
		float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

		Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

		// Dampen towards the target rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
		*/

		//Control rotation of camera
		if(Input.GetKey(KeyCode.RightArrow)){
			controlCamera.transform.Rotate( Vector3.up * rot_speed * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.LeftArrow)){
			controlCamera.transform.Rotate( Vector3.down * rot_speed * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.UpArrow)){
			controlCamera.transform.Rotate( Vector3.left * rot_speed * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.DownArrow)){
			controlCamera.transform.Rotate( Vector3.right * rot_speed * Time.deltaTime);
		}

		//Camera controls according to world coordinates
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			shift = true;
		}
		if (Input.GetKey (KeyCode.W)) {
			w = true;
		} else if (Input.GetKey (KeyCode.A)) {
			a = true;
		} else if (Input.GetKey (KeyCode.S)) {
			s = true;
		} else if (Input.GetKey (KeyCode.D)) {
			d = true;
		}

		if (shift && w) {
			controlCamera.transform.Translate (Vector3.forward * translationSpeed * Time.deltaTime, Space.World);
		} else if (shift && s) {
			controlCamera.transform.Translate (Vector3.back * translationSpeed * Time.deltaTime, Space.World);
		} else if (shift && a) {
			controlCamera.transform.Translate (Vector3.left * translationSpeed * Time.deltaTime, Space.World);
		} else if (shift && d) {
			controlCamera.transform.Translate (Vector3.right * translationSpeed * Time.deltaTime, Space.World);
		}

		shift = w = a = s = d = false;

		//Control translation of camera
		if(Input.GetKey(KeyCode.W)){
			controlCamera.transform.Translate(Vector3.forward * translationSpeed * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.S)){
			controlCamera.transform.Translate(Vector3.back * translationSpeed * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.A)){
			controlCamera.transform.Translate(Vector3.left * translationSpeed * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.D)){
			controlCamera.transform.Translate(Vector3.right * translationSpeed * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.E)){
			controlCamera.transform.Translate(Vector3.up * translationSpeed * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.Q)){
			controlCamera.transform.Translate(Vector3.down * translationSpeed * Time.deltaTime);
		} else if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W)){
			controlCamera.transform.Translate(Vector3.down * translationSpeed * Time.deltaTime, Space.World);
		} else if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S)){
			controlCamera.transform.Translate(Vector3.down * translationSpeed * Time.deltaTime, Space.World);
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");
		if (scroll > 0) {
			controlCamera.transform.Rotate (Vector3.left * mouseScrollSpeed * scroll, Space.Self);
		} else if (scroll < 0) {
			controlCamera.transform.Rotate (Vector3.right * mouseScrollSpeed * -scroll, Space.Self);
		}

		//Mouse rotation of the GameObject
		if (Input.GetMouseButton (0)) {
			tmpCube.transform.Rotate(new Vector3 (Input.GetAxis ("Mouse Y"), Input.GetAxis ("Mouse X"), 0) * mouseSpeed * Time.deltaTime, Space.Self);
		}
		//Close the scene
		else if (Input.GetKey(KeyCode.Escape)) //Click ESC
		{
			foreach (GameObject obj in allObjects) 
				obj.SetActive (true);
			Camera.main.cullingMask = 1 << 0;
			Camera.main.transform.rotation = myCamRot1;
			Camera.main.transform.position = myCamPos1;
			controlCamera.GetComponent<Controller> ().enabled = true;
			SceneManager.UnloadSceneAsync ("Object_View");
		}
		else if (Input.GetKey(KeyCode.Z))
		{
			tmpCube.transform.rotation = myCubeRot;
			tmpCube.transform.position = new Vector3 (0, 0, 0);
			Camera.main.transform.rotation = myCamRot1;
			Camera.main.transform.position = new Vector3(0,0,-3);
		}
	}
}
