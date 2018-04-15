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
	public Quaternion myCamPos;
	public Quaternion myCubePos;
	public float speed = 70.0f;
	GameObject[] allObjects;
	GameObject tmpCube;
	public int layerToShow = 30;

	// Use this for initialization
	void Start () {
		allObjects = FindObjectsOfType<GameObject> ();
		myCamPos = transform.rotation;
		Camera.main.transform.position = new Vector3(0,0,-3);
		//Camera.main.transform.rotation = new Quaternion (0,0,0,0);
		SceneManager.SetActiveScene (SceneManager.GetSceneByName ("Object_View"));
		cubeToView = GameObject.Find ("cubeSelected");
		tmpCube = (GameObject)Instantiate (cubeToView, new Vector3(0,0,0), new Quaternion());
		myCubePos = tmpCube.transform.rotation;
		//tmpCube.layer = layerToShow;
		tmpCube.SetLayerRecursively(layerToShow);
		tmpCube.name = "SelectedCube";
		cubeToView.name = "CubeSeen";

		foreach (GameObject a in allObjects) 
		{
			if((a.name.StartsWith("Cube Template")) || (a.name.StartsWith("Button_Panel")) && (a.layer == 0))
				a.SetActive (false);
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
		if (Input.GetMouseButton (0)) 
		{
			tmpCube.transform.Rotate(new Vector3 (Input.GetAxis ("Mouse Y"), Input.GetAxis ("Mouse X"), 0) * speed * Time.deltaTime, Space.Self);
		}
		//Close the scene
		else if (Input.GetKey(KeyCode.Escape)) //Click ESC
		{
			foreach (GameObject a in allObjects) 
				a.SetActive (true);
			Camera.main.cullingMask = 1 << 0;
			//Camera.main.transform.rotation = new Quaternion (0,0,0,0);
			Camera.main.transform.rotation = myCamPos;
			Camera.main.transform.position = new Vector3(0,0,-3);
			SceneManager.UnloadSceneAsync ("Object_View");
		}
		else if (Input.GetKey(KeyCode.Z))
		{
			tmpCube.transform.rotation = myCubePos;
			tmpCube.transform.position = new Vector3 (0, 0, 0);
			Camera.main.transform.rotation = myCamPos;
			Camera.main.transform.position = new Vector3(0,0,-3);
		}
	}
}
