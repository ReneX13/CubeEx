using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckGlueData : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void ValueChangeCheck()
	{
		Debug.Log ("YEAH!");
		Debug.Log(this.GetComponent<InputField> ().text);
	}
}
