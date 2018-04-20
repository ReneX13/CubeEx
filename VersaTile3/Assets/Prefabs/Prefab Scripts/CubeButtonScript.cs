using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CubeButtonScript : MonoBehaviour {

	public CubeEditorManager cem;
	public Text CubeName;
	public Cube cube;

	/*This is the function called by the when a 
	 * "Cube Button" from the "Cube Set Panel" is pressed.
	 */
	public void SELECT(){
		cem.cubeMenuScript.cbs = this;
		cem.cubeMenuScript.SetCubeInfo();
	}
}
