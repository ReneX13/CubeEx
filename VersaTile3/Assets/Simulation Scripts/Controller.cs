using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {
	public float speed = 1.0f;
	public float rot_speed = 20.0f;
	public float mouseScrollSpeed = 15.0f;
	public float scroll = 0f;
	public Quaternion myCamRot;
	public Vector3 myCamPos;
	public int layerToShow = 30;

	public float minX = -360.0f;
	public float maxX = 360.0f;

	public float minY = -90.0f;
	public float maxY = 90.0f;

	public float sensX = 100.0f;
	public float sensY = 100.0f;

	public float rotationX = 0.0f;
	public float rotationY = 0.0f;

	public bool shift = false;
	public bool w = false;
	public bool s = false;
	public bool a = false;
	public bool d = false;

	// Use this for initialization
	void Start () {
		myCamRot = transform.rotation;
		myCamPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		//Control rotation of camera
		if (Input.GetKey (KeyCode.RightArrow)) {
			//transform.Rotate( Vector3.up * rot_speed * Time.deltaTime);
			MoveRotation (0.1f, 0f);
			transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
			rotationY = Mathf.Clamp (rotationY, minY, maxY);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			//transform.Rotate( Vector3.down * rot_speed * Time.deltaTime);
			MoveRotation (-0.1f, 0f);
			transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
			rotationY = Mathf.Clamp (rotationY, minY, maxY);
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			//transform.Rotate( Vector3.left * rot_speed * Time.deltaTime);
			MoveRotation (0f, 0.1f);
			transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
			rotationY = Mathf.Clamp (rotationY, minY, maxY);
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			//transform.Rotate( Vector3.right * rot_speed * Time.deltaTime);
			MoveRotation (0f, -0.1f);
			transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
			rotationY = Mathf.Clamp (rotationY, minY, maxY);
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
			transform.Translate (Vector3.forward * speed * Time.deltaTime, Space.World);
		} else if (shift && s) {
			transform.Translate (Vector3.back * speed * Time.deltaTime, Space.World);
		} else if (shift && a) {
			transform.Translate (Vector3.left * speed * Time.deltaTime, Space.World);
		} else if (shift && d) {
			transform.Translate (Vector3.right * speed * Time.deltaTime, Space.World);
		}

		shift = w = a = s = d = false;

		//Control translation of camera
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.S)) {
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.A)) {
			transform.Translate (Vector3.left * speed * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.D)) {
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.E)) {
			transform.Translate (Vector3.up * speed * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.Q)) {
			transform.Translate (Vector3.down * speed * Time.deltaTime);
		}

		//Change the rotation of the world by using the mouse scroll. INTERFERES WITH MOUSE ROTATION.
		/*float scroll = Input.GetAxis ("Mouse ScrollWheel");
		if (scroll > 0) {
			transform.RotateAround (Vector3.zero, Vector3.right, 10.0f * Time.deltaTime);
			MoveRotation (0f, 0.1f);
		} else if (scroll < 0) {
			transform.RotateAround (Vector3.zero, Vector3.left, 10.0f * Time.deltaTime);
			MoveRotation (0f, -0.1f);
		}*/

		//Change the rotation of the camera by using the mouse scroll.
		if (Input.GetAxis("Mouse ScrollWheel") != 0){
			scroll = Input.GetAxis ("Mouse ScrollWheel"); 
			MoveRotation (0f, scroll);
			Camera.main.transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
			rotationY = Mathf.Clamp (rotationY, minY, maxY);
		}
		//float scroll = Input.GetAxis("Mouse ScrollWheel");
		/*if (scroll > 0) {
		    transform.Rotate (Vector3.left * mouseScrollSpeed * scroll, Space.Self);
			MoveRotation (0f, scroll);
		} else if (scroll < 0) {
			transform.Rotate (Vector3.right * mouseScrollSpeed * -scroll, Space.Self);
			MoveRotation (0f,  -scroll);
		}*/

		//Reposition camera at the initial location
		if (Input.GetKey(KeyCode.Z)){
			Camera.main.transform.rotation = myCamRot;
			Camera.main.transform.position = myCamPos;
			ResetCamera ();
		}

		//Change the world rotation by using left-click + movement
		if (Input.GetMouseButton (0)) {
			rotationX += Input.GetAxis ("Mouse X") * sensX * Time.deltaTime;
			rotationY += Input.GetAxis ("Mouse Y") * sensY * Time.deltaTime;
			rotationY = Mathf.Clamp (rotationY, minY, maxY);
			Camera.main.transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
		}

		// Optional if you don't want freaky effects...
		//Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 5f, 120f);

		//Select a cube for viewing
		if (Input.GetMouseButtonDown(1)) //Right-click on cube
		{
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{  
				if (SceneManager.GetActiveScene () != SceneManager.GetSceneByName ("Object_View")) 
				{
					//Destroy(GameObject.Find("Cube Template"));
					GameObject cube_to_View = hit.collider.gameObject;
					cube_to_View.name = "cubeSelected";
					SceneManager.LoadScene ("Object_View", LoadSceneMode.Additive);
					Camera.main.cullingMask = 1 << layerToShow;
					Camera.main.transform.rotation = myCamRot;
					Camera.main.transform.position = myCamPos;
					ResetCamera ();
				}
			}
		}
	}

	public void ResetCamera(){
		rotationX = 0.0f;
		rotationY = 0.0f;
	}

	public void MoveRotation(float x, float y){
		rotationX += x * sensX * Time.deltaTime;
		rotationY += y * sensY * Time.deltaTime;
	}
}
