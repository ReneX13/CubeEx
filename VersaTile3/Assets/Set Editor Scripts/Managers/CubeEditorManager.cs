using UnityEngine;
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


	public GameObject Load_File_Panel;
	public InputField Save_InputField;
	public string LoadFile = "";

	public GameObject GlueListPanel;
	public GameObject CubeSetPanel;




	public CubeSetManager setManager;
	public CubeMenuScript cubeMenuScript;

	//Prefabs
	public GameObject GluePrefab;
	public GameObject CubeButtonPrefab;
	public GameObject FileNamePrefab;


	/*These are the dropdown classes from the dropdowns in the Cube Menu 
      they are used to select the glues for a cube.
	*/
	public Dropdown dropdownFront;
	public Dropdown dropdownBack;
	public Dropdown dropdownRight;
	public Dropdown dropdownLeft;
	public Dropdown dropdownTop;
	public Dropdown dropdownBottom;

	/*If true then you are using it in the "Load" Menu.
	  If false then you are using it in the "Save" Menu,
	  and you don't all you want to do is see the list of 
	  of saved files.
	*/
	public bool LoadORSave;

	void  Start(){
		//LoadFileNames ();
		//LoadFromCSM ();
		//LoadFileNames ();
	}

//***************************************************************************
//***************************************************************************
// MENU NAVIGATING FUNCTIONS; 
// USED TO ACTIVATE AND DEACTIVE GUI TO ACCESS THE DIFFERENT MENUS
//***************************************************************************
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
		LoadORSave = false;
		reLoadFileNames ();
	}
	public void Exit_Save_Menu(){
		Save_AND_Load_BlackScreen_Panel.SetActive (false);
		Save_Menu_Panel.SetActive (false);
	}
	public void Enter_Load_Menu(){
		Save_AND_Load_BlackScreen_Panel.SetActive (true);
		Load_Menu_Panel.SetActive (true);
		LoadORSave = true;
		reLoadFileNames ();
	}
	public void Exit_Load_Menu(){
		Save_AND_Load_BlackScreen_Panel.SetActive (false);
		Load_Menu_Panel.SetActive (false);

		LoadFile = "";
	}

	public void QUIT(){
		Application.Quit ();
	}
	public void RUN_SCENE(){
		transform.GetComponent<CubeEditorManager> ().LoadToCSM ();
		Application.LoadLevel ("1");
	}

//***************************************************************************
//***************************************************************************
// FUNCTIONS USED TO ADD NEW CUBES AND GLUES TO THE "CUBE SET MANAGER"
//***************************************************************************
	public void AddGlue(){
	/* Create a GluePrefab that is added to the Glue List in the 
	 * Glue Menu Panel. They hold the inputfields for entering
	 * data, as well as a button with an 'X' to delete the glue.
	 * 
	 * A script is attached to them could GlueManager...
	 */
		GameObject tmp = (GameObject)Instantiate (GluePrefab);
		tmp.GetComponent<GlueManager> ().cem = this;
		tmp.transform.SetParent (GlueListPanel.transform);
		setManager.Glues.Add (tmp.GetComponent<GlueManager> ().glue);

	 /*The new glue that has been added to the list of glues
       must be added to the dropdown menus so that they can be
	   selected in the Cube Menu
	  */
		Dropdown.OptionData newData = new Dropdown.OptionData ();
		newData.text = tmp.GetComponent<GlueManager> ().glue.label.text;
		dropdownFront.options.Add (newData);
		dropdownBack.options.Add (newData);
		dropdownRight.options.Add (newData);
		dropdownLeft.options.Add (newData);
		dropdownTop.options.Add (newData);
		dropdownBottom.options.Add (newData);
	}
	public void AddCube(){
	/* Create a CubeButtonPrefab Prefab that is added to the Cube List in the 
	 * Cube Set Panel. They are buttons corresponding to some cube in the 
	 * list. When clicked, they update the Cube Menu with its "Cube" information
	 * allowing it to be edited
	 * 
	 * A script is attached to them called GlueButtonScript...
	 */
		GameObject tmp = (GameObject)Instantiate (CubeButtonPrefab);
		tmp.GetComponent<CubeButtonScript> ().cem = this;
		tmp.GetComponent<CubeButtonScript> ().cube = new Cube();
		tmp.transform.Find ("Cube Name").Find ("Text").GetComponent<Text> ().text = tmp.GetComponent<CubeButtonScript> ().cube.name;
		tmp.transform.SetParent (CubeSetPanel.transform);
		setManager.CubeSet.Add (tmp.GetComponent<CubeButtonScript> ().cube);
	}

//***************************************************************************
//***************************************************************************
// FUNCTIONS USED TO TRANSFER DATA FROM THE "CUBE SET MANAGER" 
// AND THE "CUBE SYSTEM MANAGER"
//***************************************************************************
	public void LoadFromCSM(){
		/*Get the "Cube System Manager"*/
		CubeSystemManager csm = GameObject.FindGameObjectWithTag ("CSM").GetComponent<CubeSystemManager> ();
		setManager.discrete_counts.isOn = csm.discrete_counts_flag;
		setManager.temperature.text	= csm.temperature.ToString();
		/*The extract glue list from "Cube System Manager", and instert
		 * into the "Glue" list from the Cube Set Manager.
		 * The glue at position 0 is a default glue with label 'none'
		 * and strength of 0, therefore we start with extract at position 1.
		 */
		for (int i = 1; i < csm.Glues.Count; i++) {
			GameObject tmp = (GameObject)Instantiate (GluePrefab);
			tmp.GetComponent<GlueManager> ().cem = this;
			tmp.transform.SetParent (GlueListPanel.transform);
	
			tmp.GetComponent<GlueManager> ().setGlue(csm.Glues[i].label, csm.Glues[i].strength);
			tmp.GetComponent<GlueManager> ().glue.setup ();
			setManager.Glues.Add (tmp.GetComponent<GlueManager> ().glue);
		}
		updateDropdowns();

		/*Extract the seed from the "CubeSystemManager"
		 * and add it to the ""CubeSet" from the "CubeSetManager".
		 */
		GameObject tmpSeed = (GameObject)Instantiate (CubeButtonPrefab);
		tmpSeed.GetComponent<CubeButtonScript> ().cem = this;
		tmpSeed.GetComponent<CubeButtonScript> ().cube = new Cube();
		tmpSeed.GetComponent<CubeButtonScript> ().cube.name = csm.Seed.name;
		tmpSeed.GetComponent<CubeButtonScript> ().CubeName.text = csm.Seed.name;
		tmpSeed.GetComponent<CubeButtonScript> ().cube.colour = csm.Seed.colour.getColor();
		tmpSeed.GetComponent<CubeButtonScript> ().cube.count = csm.Seed.count;
		for (int j = 0; j < setManager.Glues.Count; j++) {
			if (csm.Seed.Front.label == setManager.Glues [j].label.text)
				tmpSeed.GetComponent<CubeButtonScript> ().cube.Front = j;
			if (csm.Seed.Back.label == setManager.Glues [j].label.text)
				tmpSeed.GetComponent<CubeButtonScript> ().cube.Back = j;
			if (csm.Seed.Right.label == setManager.Glues [j].label.text)
				tmpSeed.GetComponent<CubeButtonScript> ().cube.Right = j;
			if (csm.Seed.Left.label == setManager.Glues [j].label.text)
				tmpSeed.GetComponent<CubeButtonScript> ().cube.Left = j;
			if (csm.Seed.Top.label == setManager.Glues [j].label.text)
				tmpSeed.GetComponent<CubeButtonScript> ().cube.Top = j;
			if (csm.Seed.Bottom.label == setManager.Glues [j].label.text)
				tmpSeed.GetComponent<CubeButtonScript> ().cube.Bottom = j;
		}
		tmpSeed.transform.SetParent (CubeSetPanel.transform);
		setManager.CubeSet.Add (tmpSeed.GetComponent<CubeButtonScript> ().cube);


		/*Extract the "CubeSet" from the "CubeSystemManager" and insert them
		 * into the "CubeSet" from the "CubeSetManager".
		 */
		for (int i = 0; i < csm.CubeSet.Count; i++) {
			GameObject tmpCube = (GameObject)Instantiate (CubeButtonPrefab);

			tmpCube.GetComponent<CubeButtonScript> ().cem = this;
			tmpCube.GetComponent<CubeButtonScript> ().cube = new Cube();
			tmpCube.GetComponent<CubeButtonScript> ().cube.name = csm.CubeSet [i].name;
			tmpCube.GetComponent<CubeButtonScript> ().CubeName.text = csm.CubeSet [i].name;
			tmpCube.GetComponent<CubeButtonScript> ().cube.colour = csm.CubeSet [i].colour.getColor();
			tmpCube.GetComponent<CubeButtonScript> ().cube.count = csm.CubeSet [i].count;
			for (int j = 0; j < setManager.Glues.Count; j++) {
				if (csm.CubeSet [i].Front.label == setManager.Glues [j].label.text)
					tmpCube.GetComponent<CubeButtonScript> ().cube.Front = j;
				if (csm.CubeSet [i].Back.label == setManager.Glues [j].label.text)
					tmpCube.GetComponent<CubeButtonScript> ().cube.Back = j;
				if (csm.CubeSet [i].Right.label == setManager.Glues [j].label.text)
					tmpCube.GetComponent<CubeButtonScript> ().cube.Right = j;
				if (csm.CubeSet [i].Left.label == setManager.Glues [j].label.text)
					tmpCube.GetComponent<CubeButtonScript> ().cube.Left = j;
				if (csm.CubeSet [i].Top.label == setManager.Glues [j].label.text)
					tmpCube.GetComponent<CubeButtonScript> ().cube.Top = j;
				if (csm.CubeSet [i].Bottom.label == setManager.Glues [j].label.text)
					tmpCube.GetComponent<CubeButtonScript> ().cube.Bottom = j;
			}
			tmpCube.transform.SetParent (CubeSetPanel.transform);
			setManager.CubeSet.Add (tmpCube.GetComponent<CubeButtonScript> ().cube);
		}
	}
	public void LoadToCSM(){
		CubeSystemManager cSYSm = GameObject.FindGameObjectWithTag ("CSM").GetComponent<CubeSystemManager> ();
		//CubeSetManager setManager = setManager.GetComponent<CubeSetManager> ();
		cSYSm.discrete_counts_flag = setManager.discrete_counts.isOn;
		cSYSm.temperature = int.Parse(setManager.temperature.text);

		cSYSm.Glues.Clear ();
		for (int i = 0; i < setManager.Glues.Count; i++) {
			cSYSm.Glues.Add(new _Glue(setManager.Glues[i].label.text, int.Parse(setManager.Glues[i].strength.text)));
		}

		cSYSm.CubeSet.Clear ();
		for (int i = 1; i < setManager.CubeSet.Count; i++) {
			_Glue g = new _Glue (setManager.Glues [setManager.CubeSet [i].Front].label.text, int.Parse (setManager.Glues [setManager.CubeSet [i].Front].strength.text));
			cSYSm.CubeSet.Add(new _Cube(setManager.CubeSet[i].name, setManager.CubeSet[i].colour, setManager.CubeSet[i].count,
				new _Glue (setManager.Glues [setManager.CubeSet [i].Front].label.text, int.Parse (setManager.Glues [setManager.CubeSet [i].Front].strength.text)),
				new _Glue (setManager.Glues [setManager.CubeSet [i].Back].label.text, int.Parse (setManager.Glues [setManager.CubeSet [i].Back].strength.text)),
				new _Glue (setManager.Glues [setManager.CubeSet [i].Right].label.text, int.Parse (setManager.Glues [setManager.CubeSet [i].Right].strength.text)),
				new _Glue (setManager.Glues [setManager.CubeSet [i].Left].label.text, int.Parse (setManager.Glues [setManager.CubeSet [i].Left].strength.text)),
				new _Glue (setManager.Glues [setManager.CubeSet [i].Top].label.text, int.Parse (setManager.Glues [setManager.CubeSet [i].Top].strength.text)),
				new _Glue (setManager.Glues [setManager.CubeSet [i].Bottom].label.text, int.Parse (setManager.Glues [setManager.CubeSet [i].Bottom].strength.text))));
		}

		cSYSm.Seed = new _Cube(setManager.CubeSet[0].name, setManager.CubeSet[0].colour, setManager.CubeSet[0].count,
			new _Glue (setManager.Glues [setManager.CubeSet [0].Front].label.text, int.Parse (setManager.Glues [setManager.CubeSet [0].Front].strength.text)),
			new _Glue (setManager.Glues [setManager.CubeSet [0].Back].label.text, int.Parse (setManager.Glues [setManager.CubeSet [0].Back].strength.text)),
			new _Glue (setManager.Glues [setManager.CubeSet [0].Right].label.text, int.Parse (setManager.Glues [setManager.CubeSet [0].Right].strength.text)),
			new _Glue (setManager.Glues [setManager.CubeSet [0].Left].label.text, int.Parse (setManager.Glues [setManager.CubeSet [0].Left].strength.text)),
			new _Glue (setManager.Glues [setManager.CubeSet [0].Top].label.text, int.Parse (setManager.Glues [setManager.CubeSet [0].Top].strength.text)),
			new _Glue (setManager.Glues [setManager.CubeSet [0].Bottom].label.text, int.Parse (setManager.Glues [setManager.CubeSet [0].Bottom].strength.text)));
	}

//***************************************************************************
//***************************************************************************
// SAVING/LOADING FUNCTIONS
//***************************************************************************
	/*These functions are used to load up the list of files 
	 * in the Saved Data folder, where we save and load the 
	 * data files from.
	*/
	public void reLoadFileNames(){
		foreach ( GameObject f in GameObject.FindGameObjectsWithTag("FileButton")){
			DestroyObject (f);
		}
		LoadFileNames ();
	}
	public void LoadFileNames(){
		DirectoryInfo dir = new DirectoryInfo (Application.dataPath + "/Saved_Data");
		FileInfo[] info = dir.GetFiles ("*.data");
		foreach (FileInfo f in info) {
			GameObject tmp = (GameObject)Instantiate (FileNamePrefab);
			tmp.GetComponent<FileNameButtonScript> ().CEM = this.GetComponent<CubeEditorManager> ();
			tmp.transform.Find ("Text").GetComponent<Text> ().color = Color.black;
			tmp.transform.Find ("Text").GetComponent<Text> ().text = f.Name;
			tmp.transform.SetParent (Load_File_Panel.transform);
		}
	}
		
	/*The are the Loading Data functions
	 * what we will be loading is the data from the "Cube System Manager"
	*/

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

		Application.LoadLevel ("Tile Editor Menu");
	}


	/*The are the Saving Data functions
	 * what we will be saving is the data from the "Cube System Manager"
	*/
	public void Save(){
		LoadToCSM ();

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
		reLoadFileNames ();
		Exit_Load_Menu ();
		Exit_Save_Menu ();
	}

//***************************************************************************
//***************************************************************************
// OTHER FUNCTIONS
//***************************************************************************
	public void updateDropdowns(){
		dropdownFront.ClearOptions();
		dropdownFront.AddOptions(setManager.GetListOfLabels());
		dropdownBack.ClearOptions();
		dropdownBack.AddOptions(setManager.GetListOfLabels());
		dropdownRight.ClearOptions();
		dropdownRight.AddOptions(setManager.GetListOfLabels());
		dropdownLeft.ClearOptions();
		dropdownLeft.AddOptions(setManager.GetListOfLabels());
		dropdownTop.ClearOptions();
		dropdownTop.AddOptions(setManager.GetListOfLabels());
		dropdownBottom.ClearOptions();
		dropdownBottom.AddOptions(setManager.GetListOfLabels());
	}
}
