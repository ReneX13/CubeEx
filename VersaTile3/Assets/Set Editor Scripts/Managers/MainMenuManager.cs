using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	public GameObject Right_Editor_Panel;
	public GameObject Left_Editor_Panel;
	public GameObject Main_Menu_Panel;

	void  Start(){

	}

	public void Editor_Button_Pressed(){
		Main_Menu_Panel.SetActive (false);
		Left_Editor_Panel.SetActive (true);
		Right_Editor_Panel.SetActive (true);
		Debug.Log ("Editor!");
	}
	public void Editor_Exit_Button_Pressed(){
		Main_Menu_Panel.SetActive (true);
		Left_Editor_Panel.SetActive (false);
		Right_Editor_Panel.SetActive (false);
		Debug.Log ("exit!");
	}
	public void QUIT(){
		Application.Quit ();
	}
	public void RUN_SCENE(){
		transform.GetComponent<CubeEditorManager> ().LoadToCSM ();
		Application.LoadLevel ("1");
	}
}
