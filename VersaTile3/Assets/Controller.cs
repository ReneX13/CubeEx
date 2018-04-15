using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {
	public float speed = 1.0f;
	public float rot_speed = 20.0f;
	public float mouseScrollSpeed = 10f;
	public Quaternion myCamPos;
	public int layerToShow = 30;

	// Use this for initialization
	void Start () {
		myCamPos = transform.rotation;
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

		//Change the zoom of the camera by using the mouse scroll.
		Camera.main.fieldOfView -= Input.GetAxisRaw("Mouse ScrollWheel") * mouseScrollSpeed;
		// Optional if you don't want freaky effects...
		Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 5f, 120f);

		//Select a cube for viewing
		if (Input.GetMouseButtonDown(1)) //Click on cube
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
					Camera.main.transform.rotation = myCamPos;
				}
			}
		}
	}

	/*
	public void SaveCubeSelected(GameObject data){
		// Stream the file with a File Stream. (Note that File.Create() 'Creates' or 'Overwrites' a file.)
		FileStream file = File.Create(Application.persistentDataPath + "/CubeToView.dat");

		//Serialize to xml
		DataContractSerializer bf = new DataContractSerializer(data.GetType());
		MemoryStream streamer = new MemoryStream();

		//Serialize the file
		bf.WriteObject(streamer, data);
		streamer.Seek(0, SeekOrigin.Begin);

		//Save to disk
		file.Write(streamer.GetBuffer(), 0, streamer.GetBuffer().Length);

		// Close the file to prevent any corruptions
		file.Close();

		string result = XElement.Parse(Encoding.ASCII.GetString(streamer.GetBuffer()).Replace("\0", "")).ToString();
		Debug.Log("Serialized Result: " + result);
	}
	*/
}
