using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadButton_Script : MonoBehaviour {

	public CubeEditorManager CEM;

	public void selected(){
		foreach ( GameObject f in GameObject.FindGameObjectsWithTag("FileButton")){
			f.transform.Find ("Text").GetComponent<Text> ().color = Color.black;
		}
		transform.Find ("Text").GetComponent<Text> ().color = Color.blue;

		CEM.LoadFile = transform.Find ("Text").GetComponent<Text> ().text;
	}
}
