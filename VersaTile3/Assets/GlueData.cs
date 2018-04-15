using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GlueData : MonoBehaviour {
	public GameObject Cube_Menu_Panel;
	public GameObject Glue_Panel;
	public CubeSetManager setmanager;
	public Glue glue;
	int index;
	void Start(){
		//glue = new Glue ();
	}
	public void setup(string l, int s){
		glue.label.text = l;
		glue.strength.text = s.ToString ();
	}

	public void DELETE_GLUE(){
		index = setmanager.Glues.IndexOf (glue);
		for (int i = 0; i < setmanager.CubeSet.Count; i++) {
			setmanager.CubeSet [i].GLUE_HAS_BEEN_DELETED (index);
		}
		setmanager.Glues.Remove (glue);
		update_glues ();
		update_Current_Dropdowns ();
		Destroy (Glue_Panel);
	}
	public void ALTER_CUBES(){

	}
	public void update_glues(){

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
	public void update_Current_Dropdowns(){

		if (index == Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Front").GetComponent<Dropdown> ().value)
			Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Front").GetComponent<Dropdown> ().value = 0;
		
		if (index == Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Back").GetComponent<Dropdown> ().value)
			Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Back").GetComponent<Dropdown> ().value = 0;
		
		if (index == Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Left").GetComponent<Dropdown> ().value)
			Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Left").GetComponent<Dropdown> ().value = 0;
		
		if (index == Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Right").GetComponent<Dropdown> ().value)
			Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Right").GetComponent<Dropdown> ().value = 0;
		
		if (index == Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Top").GetComponent<Dropdown> ().value)
			Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Top").GetComponent<Dropdown> ().value = 0;
		if (index == Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Bottom").GetComponent<Dropdown> ().value)
			Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Bottom").GetComponent<Dropdown> ().value = 0;
	}


}
