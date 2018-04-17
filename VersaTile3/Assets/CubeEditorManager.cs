using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class CubeEditorManager : MonoBehaviour {

	public GameObject File_Menu_Panel;
	public GameObject Cube_Menu_Panel;
	public GameObject Glues_Menu_Panel;

	public GameObject Save_AND_Load_BlackScreen_Panel;
	public GameObject Save_Menu_Panel;
	public GameObject Load_Menu_Panel;

	public GameObject GlueList_Panel;
	public GameObject CubeList_Panel;

	public GameObject Glue_Panel_Prefab;
	public GameObject Cube_Panel_Prefab;
	public CubeSetManager setmanger;

	public GameObject FileNamePrefab;
	public GameObject Load_File_Panel;
	public InputField Save_InputField;
	public string LoadFile = "";
	void  Start(){

		Cube_Menu_Panel.SetActive (true);
		Glues_Menu_Panel.SetActive (true);
		Save_Menu_Panel.SetActive (true);
		Load_Menu_Panel.SetActive (true);
		Save_AND_Load_BlackScreen_Panel.SetActive (true);

		File_Menu_Panel.SetActive (true);
		Cube_Menu_Panel.SetActive (false);
		Glues_Menu_Panel.SetActive (false);
		Save_Menu_Panel.SetActive (false);
		Load_Menu_Panel.SetActive (false);
		Save_AND_Load_BlackScreen_Panel.SetActive (false);

		Load_From_CSM ();
		Load_File_Names ();
	}

	public void File_Button_Pressed(){
		File_Menu_Panel.SetActive (true);
		Cube_Menu_Panel.SetActive (false);
		Glues_Menu_Panel.SetActive (false);
	}
	public void Cube_Button_Pressed(){
		File_Menu_Panel.SetActive (false);
		Cube_Menu_Panel.SetActive (true);
		Glues_Menu_Panel.SetActive (false);
	}
	public void Glues_Button_Pressed(){
		File_Menu_Panel.SetActive (false);
		Cube_Menu_Panel.SetActive (false);
		Glues_Menu_Panel.SetActive (true);
	}
	public void Enter_Save_Menu(){
		Save_AND_Load_BlackScreen_Panel.SetActive (true);
		Save_Menu_Panel.SetActive (true);
	}
	public void Exit_Save_Menu(){
		Save_AND_Load_BlackScreen_Panel.SetActive (false);
		Save_Menu_Panel.SetActive (false);
	}
	public void Enter_Load_Menu(){
		Save_AND_Load_BlackScreen_Panel.SetActive (true);
		Load_Menu_Panel.SetActive (true);
	}
	public void Exit_Load_Menu(){
		Save_AND_Load_BlackScreen_Panel.SetActive (false);
		Load_Menu_Panel.SetActive (false);

		LoadFile = "";
	}

	public void AddGlue(){
		GameObject tmp = (GameObject)Instantiate (Glue_Panel_Prefab);
		tmp.GetComponent<GlueData> ().Cube_Menu_Panel = Cube_Menu_Panel;
		tmp.GetComponent<GlueData> ().setmanager = setmanger;
		//tmp.GetComponent<GlueData>().glue = new Glue ();
		tmp.transform.SetParent (GlueList_Panel.transform);
		tmp.GetComponent<GlueData> ().glue.setup ();
		tmp.GetComponent<GlueData> ().glue.Cube_Menu_Panel = Cube_Menu_Panel;
		tmp.GetComponent<GlueData> ().glue.setmanager = setmanger;
		 setmanger.Glues.Add (tmp.GetComponent<GlueData> ().glue);

		Dropdown.OptionData newData = new Dropdown.OptionData ();
		newData.text = tmp.GetComponent<GlueData> ().glue.label.text;
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Front").GetComponent<Dropdown> ().options.Add (newData);
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Back").GetComponent<Dropdown> ().options.Add (newData);
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Right").GetComponent<Dropdown> ().options.Add (newData);
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Left").GetComponent<Dropdown> ().options.Add (newData);
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Top").GetComponent<Dropdown> ().options.Add (newData);
		Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Bottom").GetComponent<Dropdown> ().options.Add (newData);
	}

	public void AddCube(){
		GameObject tmp = (GameObject)Instantiate (Cube_Panel_Prefab);

		tmp.GetComponent<Cube_Button_Script> ().setmanager = setmanger;
		tmp.GetComponent<Cube_Button_Script> ().cube = new Cube();
		tmp.transform.Find ("Cube_Name_Panel").Find ("Text").GetComponent<Text> ().text = tmp.GetComponent<Cube_Button_Script> ().cube.name;

		tmp.GetComponent<Cube_Button_Script>().Cube_Menu = Cube_Menu_Panel.GetComponent<Cube_Menu_Script>();
		tmp.transform.SetParent (CubeList_Panel.transform);
		setmanger.CubeSet.Add (tmp.GetComponent<Cube_Button_Script> ().cube);
		//tmp.GetComponent<DeleteGlue> ().glue = new Glue(tmp.transform.Find ("Label").GetComponent<InputField>(), tmp.transform.Find ("Strength").GetComponent<InputField>());
		//setmanger.Glues.Add (tmp.GetComponent<DeleteGlue> ().glue);
	}

	public void Load_From_CSM(){
		CubeSystemManager csm = GameObject.FindGameObjectWithTag ("CSM").GetComponent<CubeSystemManager> ();
		CubeSetManager cSETm = setmanger.GetComponent<CubeSetManager> ();
		for (int i = 1; i < csm.Glues.Count; i++) {
			GameObject tmp = (GameObject)Instantiate (Glue_Panel_Prefab);
			tmp.GetComponent<GlueData> ().Cube_Menu_Panel = Cube_Menu_Panel;
			tmp.GetComponent<GlueData> ().setmanager = setmanger;
			tmp.transform.SetParent (GlueList_Panel.transform);

			tmp.GetComponent<GlueData> ().setup (csm.Glues[i].label, csm.Glues[i].strength);
			tmp.GetComponent<GlueData> ().glue.setup ();
			tmp.GetComponent<GlueData> ().glue.Cube_Menu_Panel = Cube_Menu_Panel;
			tmp.GetComponent<GlueData> ().glue.setmanager = setmanger;
			setmanger.Glues.Add (tmp.GetComponent<GlueData> ().glue);

			Dropdown.OptionData newData = new Dropdown.OptionData ();
			newData.text = tmp.GetComponent<GlueData> ().glue.label.text;
			Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Front").GetComponent<Dropdown> ().options.Add (newData);
			Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Back").GetComponent<Dropdown> ().options.Add (newData);
			Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Right").GetComponent<Dropdown> ().options.Add (newData);
			Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Left").GetComponent<Dropdown> ().options.Add (newData);
			Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Top").GetComponent<Dropdown> ().options.Add (newData);
			Cube_Menu_Panel.transform.Find ("Glue_Panel").Find ("DropDown_Panel").Find ("Dropdown_Bottom").GetComponent<Dropdown> ().options.Add (newData);
		}

		GameObject tmp1 = (GameObject)Instantiate (Cube_Panel_Prefab);

		tmp1.GetComponent<Cube_Button_Script> ().setmanager = setmanger;
		tmp1.GetComponent<Cube_Button_Script> ().cube = new Cube();
		tmp1.GetComponent<Cube_Button_Script> ().cube.name = csm.Seed.name;
		tmp1.GetComponent<Cube_Button_Script> ().cube.colour = csm.Seed.colour.getColor();
		for (int j = 0; j < cSETm.Glues.Count; j++) {
			if (csm.Seed.Front.label == cSETm.Glues [j].label.text)
				tmp1.GetComponent<Cube_Button_Script> ().cube.Front = j;
			if (csm.Seed.Back.label == cSETm.Glues [j].label.text)
				tmp1.GetComponent<Cube_Button_Script> ().cube.Back = j;
			if (csm.Seed.Right.label == cSETm.Glues [j].label.text)
				tmp1.GetComponent<Cube_Button_Script> ().cube.Right = j;
			if (csm.Seed.Left.label == cSETm.Glues [j].label.text)
				tmp1.GetComponent<Cube_Button_Script> ().cube.Left = j;
			if (csm.Seed.Top.label == cSETm.Glues [j].label.text)
				tmp1.GetComponent<Cube_Button_Script> ().cube.Top = j;
			if (csm.Seed.Bottom.label == cSETm.Glues [j].label.text)
				tmp1.GetComponent<Cube_Button_Script> ().cube.Bottom = j;
		}

		tmp1.transform.Find ("Cube_Name_Panel").Find ("Text").GetComponent<Text> ().text = tmp1.GetComponent<Cube_Button_Script> ().cube.name;
		tmp1.GetComponent<Cube_Button_Script>().Cube_Menu = Cube_Menu_Panel.GetComponent<Cube_Menu_Script>();
		tmp1.transform.SetParent (CubeList_Panel.transform);
		setmanger.CubeSet.Add (tmp1.GetComponent<Cube_Button_Script> ().cube);

		for (int i = 0; i < csm.CubeSet.Count; i++) {
			GameObject tmp = (GameObject)Instantiate (Cube_Panel_Prefab);

			tmp.GetComponent<Cube_Button_Script> ().setmanager = setmanger;
			tmp.GetComponent<Cube_Button_Script> ().cube = new Cube();
			tmp.GetComponent<Cube_Button_Script> ().cube.name = csm.CubeSet [i].name;
			tmp.GetComponent<Cube_Button_Script> ().cube.colour = csm.CubeSet [i].colour.getColor();
			for (int j = 0; j < cSETm.Glues.Count; j++) {
				if (csm.CubeSet [i].Front.label == cSETm.Glues [j].label.text)
					tmp.GetComponent<Cube_Button_Script> ().cube.Front = j;
				if (csm.CubeSet [i].Back.label == cSETm.Glues [j].label.text)
					tmp.GetComponent<Cube_Button_Script> ().cube.Back = j;
				if (csm.CubeSet [i].Right.label == cSETm.Glues [j].label.text)
					tmp.GetComponent<Cube_Button_Script> ().cube.Right = j;
				if (csm.CubeSet [i].Left.label == cSETm.Glues [j].label.text)
					tmp.GetComponent<Cube_Button_Script> ().cube.Left = j;
				if (csm.CubeSet [i].Top.label == cSETm.Glues [j].label.text)
					tmp.GetComponent<Cube_Button_Script> ().cube.Top = j;
				if (csm.CubeSet [i].Bottom.label == cSETm.Glues [j].label.text)
					tmp.GetComponent<Cube_Button_Script> ().cube.Bottom = j;
			}

			tmp.transform.Find ("Cube_Name_Panel").Find ("Text").GetComponent<Text> ().text = tmp.GetComponent<Cube_Button_Script> ().cube.name;
			tmp.GetComponent<Cube_Button_Script>().Cube_Menu = Cube_Menu_Panel.GetComponent<Cube_Menu_Script>();
			tmp.transform.SetParent (CubeList_Panel.transform);
			setmanger.CubeSet.Add (tmp.GetComponent<Cube_Button_Script> ().cube);
		}
	}

	public void Load_To_CSM(){
		CubeSystemManager cSYSm = GameObject.FindGameObjectWithTag ("CSM").GetComponent<CubeSystemManager> ();
		CubeSetManager cSETm = setmanger.GetComponent<CubeSetManager> ();
		cSYSm.Glues.Clear ();
		for (int i = 0; i < cSETm.Glues.Count; i++) {
			cSYSm.Glues.Add(new _Glue(cSETm.Glues[i].label.text, int.Parse(cSETm.Glues[i].strength.text)));
		}

		cSYSm.CubeSet.Clear ();
		for (int i = 1; i < cSETm.CubeSet.Count; i++) {
			_Glue g = new _Glue (cSETm.Glues [cSETm.CubeSet [i].Front].label.text, int.Parse (cSETm.Glues [cSETm.CubeSet [i].Front].strength.text));
			cSYSm.CubeSet.Add(new _Cube(cSETm.CubeSet[i].name, cSETm.CubeSet[i].colour, cSETm.CubeSet[i].Count,
				new _Glue (cSETm.Glues [cSETm.CubeSet [i].Front].label.text, int.Parse (cSETm.Glues [cSETm.CubeSet [i].Front].strength.text)),
				new _Glue (cSETm.Glues [cSETm.CubeSet [i].Back].label.text, int.Parse (cSETm.Glues [cSETm.CubeSet [i].Back].strength.text)),
				new _Glue (cSETm.Glues [cSETm.CubeSet [i].Right].label.text, int.Parse (cSETm.Glues [cSETm.CubeSet [i].Right].strength.text)),
				new _Glue (cSETm.Glues [cSETm.CubeSet [i].Left].label.text, int.Parse (cSETm.Glues [cSETm.CubeSet [i].Left].strength.text)),
				new _Glue (cSETm.Glues [cSETm.CubeSet [i].Top].label.text, int.Parse (cSETm.Glues [cSETm.CubeSet [i].Top].strength.text)),
				new _Glue (cSETm.Glues [cSETm.CubeSet [i].Bottom].label.text, int.Parse (cSETm.Glues [cSETm.CubeSet [i].Bottom].strength.text))));
		}

		cSYSm.Seed = new _Cube(cSETm.CubeSet[0].name, cSETm.CubeSet[0].colour, cSETm.CubeSet[0].Count,
			new _Glue (cSETm.Glues [cSETm.CubeSet [0].Front].label.text, int.Parse (cSETm.Glues [cSETm.CubeSet [0].Front].strength.text)),
			new _Glue (cSETm.Glues [cSETm.CubeSet [0].Back].label.text, int.Parse (cSETm.Glues [cSETm.CubeSet [0].Back].strength.text)),
			new _Glue (cSETm.Glues [cSETm.CubeSet [0].Right].label.text, int.Parse (cSETm.Glues [cSETm.CubeSet [0].Right].strength.text)),
			new _Glue (cSETm.Glues [cSETm.CubeSet [0].Left].label.text, int.Parse (cSETm.Glues [cSETm.CubeSet [0].Left].strength.text)),
			new _Glue (cSETm.Glues [cSETm.CubeSet [0].Top].label.text, int.Parse (cSETm.Glues [cSETm.CubeSet [0].Top].strength.text)),
			new _Glue (cSETm.Glues [cSETm.CubeSet [0].Bottom].label.text, int.Parse (cSETm.Glues [cSETm.CubeSet [0].Bottom].strength.text)));
	}

	public void Load_File_Names(){
		DirectoryInfo dir = new DirectoryInfo (Application.dataPath + "/Saved_Data");
		FileInfo[] info = dir.GetFiles ("*.data");
		foreach (FileInfo f in info) {
			GameObject tmp = (GameObject)Instantiate (FileNamePrefab);
			tmp.GetComponent<LoadButton_Script> ().CEM = this.GetComponent<CubeEditorManager> ();
			tmp.transform.Find ("Text").GetComponent<Text> ().color = Color.black;
			tmp.transform.Find ("Text").GetComponent<Text> ().text = f.Name;
			tmp.transform.SetParent (Load_File_Panel.transform);
		}
	}
	public void Load(){
		SavingAndLoadingClass slc = new SavingAndLoadingClass ();
		string filepath = Application.dataPath+"/Saved_Data/"+LoadFile;
		FileStream file;
		file = File.Open (filepath, FileMode.Open);
		BinaryFormatter bf = new BinaryFormatter ();
		slc = bf.Deserialize (file) as SavingAndLoadingClass;
		file.Close ();
		GameObject.FindGameObjectWithTag ("CSM").GetComponent<CubeSystemManager> ().Seed = slc.Seed;
		GameObject.FindGameObjectWithTag ("CSM").GetComponent<CubeSystemManager> ().CubeSet = slc.CubeSet;
		GameObject.FindGameObjectWithTag ("CSM").GetComponent<CubeSystemManager> ().Glues = slc.Glues;

		SceneManager.LoadScene ("Tile Editor Menu");
	}
	public void refreshLoad_File_Names(){
		foreach ( GameObject f in GameObject.FindGameObjectsWithTag("FileButton")){
			DestroyObject (f);
		}
		Load_File_Names ();
	}
	public void Save(){
		Load_To_CSM ();

		string filepath = Application.dataPath+"/Saved_Data";
		if (!Directory.Exists (filepath)) {
			Directory.CreateDirectory (filepath);

		} else {
			
		}

		filepath += "/" + Save_InputField.text+".data";

		Save (filepath);
	}
	public void Save(string path){
		FileStream file;
		file = File.Create (path);
		BinaryFormatter bf = new BinaryFormatter ();
		bf.Serialize (file, new SavingAndLoadingClass(GameObject.FindGameObjectWithTag ("CSM").GetComponent<CubeSystemManager> ().CubeSet,
			GameObject.FindGameObjectWithTag ("CSM").GetComponent<CubeSystemManager> ().Glues,
			GameObject.FindGameObjectWithTag ("CSM").GetComponent<CubeSystemManager> ().Seed));
		file.Close ();
	
		Enter_Load_Menu();
		refreshLoad_File_Names ();
		Exit_Load_Menu ();
		Exit_Save_Menu ();
	}

}
