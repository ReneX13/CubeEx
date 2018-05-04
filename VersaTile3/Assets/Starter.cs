using UnityEngine;
using System.Collections;

public class Starter : MonoBehaviour {
	public CubeEditorManager cem;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (this);
		Debug.Log ("Update!");
		cem.File_Menu_Panel.SetActive (true);
		cem.Cube_Menu_Panel.SetActive (false);
		cem.Glues_Menu_Panel.SetActive (false);
		cem.Save_Menu_Panel.SetActive (false);
		cem.Load_Menu_Panel.SetActive (false);
		cem.Save_AND_Load_BlackScreen_Panel.SetActive (false);

		cem.LoadFromCSM ();
		cem.LoadFileNames ();
	}
}
