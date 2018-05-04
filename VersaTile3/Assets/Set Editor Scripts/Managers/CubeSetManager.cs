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
	public Toggle discrete_counts;
	public InputField temperature;
	/*This function is used to give the dropdowns the proper 
	 * data the need to display. Which is the labels of all 
	 * glues in the glue list, "Glues"
	 */ 
	public List<string> GetListOfLabels(){
		List<string> labels = new List<string> ();
		for (int i = 0; i < Glues.Count; i++) {
			if (!labels.Contains (Glues [i].label.text)) {
				labels.Add (Glues [i].label.text);
			} 
			if (!labels.Contains (Glues [i].label2.text)) {
				labels.Add (Glues [i].label2.text);
			} 
		}
		return labels;
	}
}
	

//***************************************************************************
//***************************************************************************
// CUBE CLASS
//***************************************************************************
/*The "Cube" class has a name and directions of the faces of a cube. Each
 * face is an integer, which corresponds to a the position of a "Glue" in
 * "Glues", which is the list of glues.
 */ 
[System.Serializable]
public class Cube{
	public string name;
	public int Front;
	public int Back;
	public int Left;
	public int Right;
	public int Top;
	public int Bottom;
	public int count;

	public Color colour;

	public Cube(){
		count = -1;
		name = "Default";
	}

	/*When a glue has been deleted, the "Glue Manager" for that deleted glue
	 * will call the function to let the cubes know that that glue should be 
	 * deleted. And thus, if any of the faces corresponds to that glue, we set
	 * them to 0, which is the position for the defautl glue "none".
	*/
	public void glueHasBeenDeleted(int g){
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

//***************************************************************************
//***************************************************************************
// GLUE CLASS
//***************************************************************************
/*The "Glue" class contains inputfields label and strength.
 */ 
[System.Serializable]
public class Glue{
	public CubeEditorManager cem;
	public InputField label;  
	public InputField label2; 
	public InputField strength;

	public Glue(){
		
	}

	//setup the on change listner for the "label" input field
	public void setup(){
		label.GetComponent<InputField> ().onValueChanged.AddListener (delegate {
			ValueChangeCheck_LABEL ();
		});
		label2.GetComponent<InputField> ().onValueChanged.AddListener (delegate {
			ValueChangeCheck_LABEL ();
		});
	}


	/*This function is used to update dropdowns when the label of the glue
	 * changes.
	 */
	public void ValueChangeCheck_LABEL()
	{
		cem.dropdownFront.ClearOptions();
		cem.dropdownFront.AddOptions(cem.setManager.GetListOfLabels());
		cem.dropdownBack.ClearOptions();
		cem.dropdownBack.AddOptions(cem.setManager.GetListOfLabels());
		cem.dropdownRight.ClearOptions();
		cem.dropdownRight.AddOptions(cem.setManager.GetListOfLabels());
		cem.dropdownLeft.ClearOptions();
		cem.dropdownLeft.AddOptions(cem.setManager.GetListOfLabels());
		cem.dropdownTop.ClearOptions();
		cem.dropdownTop.AddOptions(cem.setManager.GetListOfLabels());
		cem.dropdownBottom.ClearOptions();
		cem.dropdownBottom.AddOptions(cem.setManager.GetListOfLabels());
	}

}