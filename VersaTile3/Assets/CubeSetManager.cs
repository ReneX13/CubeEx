using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class CubeSetManager : MonoBehaviour {
	
	public Cube Seed;
	public List<Cube> CubeSet;
	public List<Glue> Glues;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			DisplayGlues ();
		}
	}

	void DisplayGlues(){
		for (int i = 0; i < Glues.Count; i++) {
			Debug.Log (Glues [i].label.text + Glues[i].strength.text);
		}
	}
	public List<string> GetListOfLabels(){
		List<string> labels = new List<string> ();
		for (int i = 0; i < Glues.Count; i++) {
			labels.Add(Glues [i].label.text);
		}

		return labels;
	}
}
	
[System.Serializable]
public class Cube{
	public string name;
	public int Front;
	public int Back;
	public int Left;
	public int Right;
	public int Top;
	public int Bottom;

	public Color colour;

	public Cube(){
		name = "Default";
	}
	public void GLUE_HAS_BEEN_DELETED(int g){
		if (Front == g)
			Front = 0;
		if (Back == g)
			Back = 0;
		if (Right == g)
			Right = 0;
		if (Left == g)
			Left = 0;
		if (Top == g)
			Top = 0;
		if (Bottom == g)
			Bottom = 0;
	}
}

[System.Serializable]
public class Glue{
	public GameObject Cube_Menu_Panel;
	public CubeSetManager setmanager;
	public InputField label; 
	public InputField strength;

	public Glue(){
		
	}

	public void setup(){
		label.GetComponent<InputField> ().onValueChanged.AddListener (delegate {
			ValueChangeCheck_LABEL ();
		});
	}
	public void ValueChangeCheck_LABEL()
	{
		Debug.Log ("LABEL_CHANGE");
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Front").GetComponent<Dropdown> ().ClearOptions();
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Front").GetComponent<Dropdown> ().AddOptions(setmanager.GetListOfLabels());
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Back").GetComponent<Dropdown> ().ClearOptions();
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Back").GetComponent<Dropdown> ().AddOptions(setmanager.GetListOfLabels());
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Right").GetComponent<Dropdown> ().ClearOptions();
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Right").GetComponent<Dropdown> ().AddOptions(setmanager.GetListOfLabels());
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Left").GetComponent<Dropdown> ().ClearOptions();
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Left").GetComponent<Dropdown> ().AddOptions(setmanager.GetListOfLabels());
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Top").GetComponent<Dropdown> ().ClearOptions();
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Top").GetComponent<Dropdown> ().AddOptions(setmanager.GetListOfLabels());
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Bottom").GetComponent<Dropdown> ().ClearOptions();
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Bottom").GetComponent<Dropdown> ().AddOptions(setmanager.GetListOfLabels());

	}
}