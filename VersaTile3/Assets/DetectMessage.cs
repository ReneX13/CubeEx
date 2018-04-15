using UnityEngine;
using System.Collections;

public class DetectMessage : MonoBehaviour {

	void MessageDectionTest(){
		this.GetComponent<Renderer> ().material.SetColor ("_Color", Color.blue);
	}
}
