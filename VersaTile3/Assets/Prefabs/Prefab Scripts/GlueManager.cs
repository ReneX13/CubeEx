using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GlueManager: MonoBehaviour {

	public CubeEditorManager cem;
	public Glue glue;

	void Start(){
		glue.setup();
		glue.cem = cem;
	}

	/*This function called when loading back from the CubeSystemManager*/
	/*public void setGlue(string l, int s){
		glue.label.text = l;
		glue.strength.text = s.ToString ();
	}*/

	//new setGlue
	public void setGlue(string l, string r, int s){
		glue.label.text = l;
		glue.label2.text = r;
		glue.strength.text = s.ToString ();
	}

	/*This is the function called when clicking the button with 
	 * the "X" label, on one of the Glues from the Glue list.
	 * It makes sure to update everything properly when deleting
	 * a glue.
	 */ 
	public void DeleteGlue(){
		int index = cem.setManager.Glues.IndexOf (glue);
		for (int i = 0; i < cem.setManager.CubeSet.Count; i++) {
			cem.setManager.CubeSet [i].glueHasBeenDeleted (index);
		}

		cem.setManager.Glues.Remove (glue);
		cem.updateDropdowns ();
		cem.cubeMenuScript.SetCubeInfo ();
		Destroy (gameObject);
	}


}
