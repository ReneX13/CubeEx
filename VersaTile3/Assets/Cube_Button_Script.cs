using UnityEngine;
using System.Collections;

public class Cube_Button_Script : MonoBehaviour {

	public Cube_Menu_Script Cube_Menu;
	public Cube cube;
	public CubeSetManager setmanager;
	public void SELECT(){
		Cube_Menu.Cube_Stats = this.gameObject;
		Cube_Menu.SendMessage ("SetCubeInfo");
	}
}
