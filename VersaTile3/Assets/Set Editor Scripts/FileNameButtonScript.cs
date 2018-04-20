using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FileNameButtonScript : MonoBehaviour {
	/*This script is for the File Name Buttons which act
	 * as a selectors when loading. Whenever you click on 
	 * one of these the text will turn blue, meaning that 
	 * that's the file you want to load.
	 */ 
	public CubeEditorManager CEM;

	public void selected(){
		Debug.Log ("Its working...");
		if (CEM.LoadORSave)
			ifLoad ();
		else
			ifSave ();
	}
	public void resetTextColor(){
		foreach ( GameObject f in GameObject.FindGameObjectsWithTag("FileButton")){
			f.transform.Find ("Text").GetComponent<Text> ().color = Color.black;
		}
	}
	public void ifSave(){
		resetTextColor ();
		transform.Find ("Text").GetComponent<Text> ().color = Color.blue;
		CEM.Save_InputField.text = transform.Find ("Text").GetComponent<Text> ().text.Replace(".data", "");

	}
	public void ifLoad(){
		resetTextColor ();
		transform.Find ("Text").GetComponent<Text> ().color = Color.blue;
		CEM.LoadFile = transform.Find ("Text").GetComponent<Text> ().text;
	}
		
}
