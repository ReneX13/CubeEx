using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Cube_Menu_Script : MonoBehaviour {
	public GameObject Cube_Stats;
	public CubeSetManager setmanager;
	public CUIColorPicker color;
	bool flag = true;
	// Use this for initialization
	void Start () {
		transform.Find ("Name").GetComponent<InputField>().onValueChanged.AddListener(delegate {ValueChangeCheck(); });

		Dropdown.OptionData newData = new Dropdown.OptionData ();
		newData.text = "none";
		setup ();


	
	}
			public void changeColor(){

			}
	public void setup(){
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Front").GetComponent<Dropdown> ().ClearOptions();
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Front").GetComponent<Dropdown> ().AddOptions (setmanager.GetListOfLabels());

		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Back").GetComponent<Dropdown> ().ClearOptions();
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Back").GetComponent<Dropdown> ().AddOptions (setmanager.GetListOfLabels());

		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Right").GetComponent<Dropdown> ().ClearOptions();
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Right").GetComponent<Dropdown> ().AddOptions (setmanager.GetListOfLabels());

		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Left").GetComponent<Dropdown> ().ClearOptions();
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Left").GetComponent<Dropdown> ().AddOptions (setmanager.GetListOfLabels());

		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Top").GetComponent<Dropdown> ().ClearOptions();
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Top").GetComponent<Dropdown> ().AddOptions (setmanager.GetListOfLabels());

		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Bottom").GetComponent<Dropdown> ().ClearOptions();
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Bottom").GetComponent<Dropdown> ().AddOptions (setmanager.GetListOfLabels());
	}
	// Update is called once per frame
	void Update () {
		if (flag) {
			//setup ();
			flag = false;
		}
		if (color.Color != Cube_Stats.GetComponent<Cube_Button_Script> ().cube.colour) {
			Debug.Log ("Color hs Changed!");
			Cube_Stats.GetComponent<Cube_Button_Script> ().cube.colour = color.Color;
		}
		
	}
	void ValueChangeCheck()
	{
		Cube_Stats.GetComponent<Cube_Button_Script> ().cube.name = transform.Find ("Name").GetComponent<InputField> ().text;
		Cube_Stats.transform.Find ("Cube_Name_Panel").Find ("Text").GetComponent<Text> ().text = transform.Find ("Name").GetComponent<InputField> ().text;

	}
	public void AddGlue(string glue){

	}
	public void SetCubeInfo(){
		transform.Find ("Name").GetComponent<InputField>().text = Cube_Stats.GetComponent<Cube_Button_Script>().cube.name;
		color.Color = Cube_Stats.GetComponent<Cube_Button_Script> ().cube.colour;
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Front").GetComponent<Dropdown> ().value
			= Cube_Stats.GetComponent<Cube_Button_Script> ().cube.Front;
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Back").GetComponent<Dropdown> ().value
			= Cube_Stats.GetComponent<Cube_Button_Script> ().cube.Back;
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Right").GetComponent<Dropdown> ().value
			= Cube_Stats.GetComponent<Cube_Button_Script> ().cube.Right;
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Left").GetComponent<Dropdown> ().value
			= Cube_Stats.GetComponent<Cube_Button_Script> ().cube.Left;
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Top").GetComponent<Dropdown> ().value
			= Cube_Stats.GetComponent<Cube_Button_Script> ().cube.Top;
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Bottom").GetComponent<Dropdown> ().value
			= Cube_Stats.GetComponent<Cube_Button_Script> ().cube.Bottom;
	}
	public void DELETE(){
		transform.Find ("Name").GetComponent<InputField> ().text = "";
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Front").GetComponent<Dropdown> ().value= 0;
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Back").GetComponent<Dropdown> ().value= 0;
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Right").GetComponent<Dropdown> ().value= 0;
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Left").GetComponent<Dropdown> ().value= 0;
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Top").GetComponent<Dropdown> ().value= 0;
		transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Bottom").GetComponent<Dropdown> ().value = 0;
		Destroy (Cube_Stats);
	}


	public void updateFront_Glue(){
		Cube_Stats.GetComponent<Cube_Button_Script> ().cube.Front 
			= transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Front").GetComponent<Dropdown> ().value;
	}
	public void updateBack_Glue(){
		Cube_Stats.GetComponent<Cube_Button_Script> ().cube.Back 
			= transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Back").GetComponent<Dropdown> ().value;
	}
	public void updateRight_Glue(){
		Cube_Stats.GetComponent<Cube_Button_Script> ().cube.Right 
			= transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Right").GetComponent<Dropdown> ().value;
	}
	public void updateLeft_Glue(){
		Cube_Stats.GetComponent<Cube_Button_Script> ().cube.Left
			= transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Left").GetComponent<Dropdown> ().value;
	}
	public void updateTop_Glue(){
		Cube_Stats.GetComponent<Cube_Button_Script> ().cube.Top
			= transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Top").GetComponent<Dropdown> ().value;
	}
	public void updateBottom_Glue(){
		Cube_Stats.GetComponent<Cube_Button_Script> ().cube.Bottom 
			= transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Bottom").GetComponent<Dropdown> ().value;
	}
}
