using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CubeMenuScript : MonoBehaviour {
	public CubeButtonScript cbs;
	public CubeEditorManager cem;
	public CUIColorPicker color;
	bool flag = true;

	// Use this for initialization
	void Start () {
		//transform.Find ("Name").GetComponent<InputField>().onValueChanged.AddListener(delegate {ValueChangeCheck_CubeName(); });
		//Dropdown.OptionData newData = new Dropdown.OptionData ();
		//newData.text = "none";
		//setup ();
	}

	public void setup(){
		cem.updateDropdowns ();
	}

	// Update is called once per frame
	void Update () {
		if (flag) {
			//setup ();
			flag = false;
		}
		/*if (color.Color != cbs.cube.colour) {
			Debug.Log ("Color hs Changed!");
			cbs.cube.colour = color.Color;
		}*/
		
	}

	/*Checks if the inputfield for the name of the  current selected cube
	 * that is being edited has changed. If it has it then updates the 
	 * cubes information.
	*/
	public void ValueChangeCheck_CubeName()
	{
		cbs.cube.name = transform.Find ("Name").GetComponent<InputField> ().text;
		cbs.CubeName.text = transform.Find ("Name").GetComponent<InputField> ().text;
	}
	public void ValueChangeCheck_Count(){
		cbs.cube.count = int.Parse(transform.Find ("CubeCount").GetComponent<InputField> ().text);
	}

	/*Set the information of the selected cube to be edited.
	 * This is called by the "CubeButtonScript".
	*/
	public void SetCubeInfo(){
		transform.Find ("Name").GetComponent<InputField>().text = cbs.cube.name;
		color.Color = cbs.cube.colour;
		transform.Find ("CubeCount").GetComponent<InputField> ().text = cbs.cube.count.ToString ();
		cem.dropdownFront.value = 0;//cbs.cube.Front;
		cem.dropdownBack.value = cbs.cube.Back;
		cem.dropdownRight.value = cbs.cube.Right;
		cem.dropdownLeft.value = cbs.cube.Left;
		cem.dropdownTop.value = cbs.cube.Top;
		cem.dropdownBottom.value = cbs.cube.Bottom;
	}

	/*This functions is used by the "delete" button in the 
	 * "Cube Menu". It resets the information until a 
	 * "Cube Button" is pressed, and the information 
	 * of that button is set up by the "SetCubeInfo()" function.
	*/
	public void DELETE(){
		transform.Find ("Name").GetComponent<InputField> ().text = "";
		cem.dropdownFront.value = 0;
		cem.dropdownBack.value = 0;
		cem.dropdownRight.value = 0;
		cem.dropdownLeft.value = 0;
		cem.dropdownTop.value = 0;
		cem.dropdownBottom.value = 0;
		Destroy (cbs.gameObject);
	}

//***************************************************************************
//***************************************************************************
// FUNCTIONS USED BY THE DROPDOWNS FOR OnValueChange
//***************************************************************************

	public void updateFrontGlue(){
		cbs.cube.Front = cem.dropdownFront.value;
	}
	public void updateBackGlue(){
		cbs.cube.Back = cem.dropdownBack.value;
	}
	public void updateRightGlue(){
		cbs.cube.Right = cem.dropdownRight.value;
	}
	public void updateLeftGlue(){
		cbs.cube.Left= cem.dropdownLeft.value;
	}
	public void updateTopGlue(){
		cbs.cube.Top= cem.dropdownTop.value;
	}
	public void updateBottomGlue(){
		cbs.cube.Bottom = cem.dropdownBottom.value;
	}
}
