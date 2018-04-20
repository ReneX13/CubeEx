using UnityEngine;

using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class AssemblyManager : MonoBehaviour {
	//This counter is to keep the program from going into an infinite loop while testing it.
	public int test_counter = 10;

	public Text play_pause_text;
	public bool play_pause_flag = false;
	public CubeSystemManager setManager;
	public GameObject CubePrefab;
	public List<Vector3> nextPositions;
	// Use this for initialization
	void Start () {
//Debugging Functions
		//CreateFirstCubeInList ();
		//CreateAllCubeInList();
		//TEST_1();

		//Test_2_initialize ();
		//isPositionEmpty (new Vector3 (0, 0, 0));
		setManager = GameObject.FindGameObjectWithTag ("CSM").GetComponent<CubeSystemManager>();
		intialize_Seed();
		play_pause_text.text = "play";

	}
	
	// Update is called once per frame
	void Update () {
		if (play_pause_flag) {
			//Test_2_update ();
			MyUpdate();
			//test_counter--;
		}


	}
	public void EDITOR_SCENE(){
		Application.LoadLevel ("Tile Editor Menu");
	}
	public void Play_Pause_Function(){
		if (play_pause_flag) {
			play_pause_text.text = "play";
			play_pause_flag = false;
		} else {
			play_pause_text.text = "pause";
			play_pause_flag = true;
		}
	}

//*****************************************************
//Debugging Functions
//*****************************************************

//TEST_3: Start using the cubes fromt he set manaager.
	public void intialize_Seed(){
		Vector3 pos = new Vector3 (0, 0, 0);
		GameObject tmpCube = (GameObject)Instantiate (CubePrefab, pos, new Quaternion ());
		tmpCube.GetComponent<Renderer> ().material.SetColor ("_Color", setManager.Seed.colour.getColor());
		tmpCube.transform.Find ("Front").GetComponent<TextMesh> ().text = setManager.Seed.Front.label;
		tmpCube.transform.Find ("Back").GetComponent<TextMesh> ().text = setManager.Seed.Back.label;
		tmpCube.transform.Find ("Left").GetComponent<TextMesh> ().text = setManager.Seed.Left.label;
		tmpCube.transform.Find ("Right").GetComponent<TextMesh> ().text = setManager.Seed.Right.label;
		tmpCube.transform.Find ("Top").GetComponent<TextMesh> ().text = setManager.Seed.Top.label;
		tmpCube.transform.Find ("Bottom").GetComponent<TextMesh> ().text = setManager.Seed.Bottom.label;

		Vector3 tmpPos = new Vector3 (1 + pos.x, pos.y, pos.z);
			nextPositions.Add (tmpPos);
		tmpPos = new Vector3 (pos.x - 1, pos.y, pos.z);
			nextPositions.Add (tmpPos);
		tmpPos = new Vector3 (pos.x, 1 + pos.y, pos.z);
			nextPositions.Add (tmpPos);
		tmpPos = new Vector3 (pos.x, pos.y - 1, pos.z);
			nextPositions.Add (tmpPos);
		tmpPos = new Vector3 (pos.x, pos.y, 1 + pos.z);
			nextPositions.Add (tmpPos);
		tmpPos = new Vector3 (pos.x, pos.y, pos.z - 1);
			nextPositions.Add (tmpPos);
	}

	/*This function check whethere there are any Vectors in nextPositions.
	 * If next Positions is no empty then keep trying to attach cubes.
	*/
	public void MyUpdate(){
		if(nextPositions.Count > 0){
			insertCube (nextPosition ());
		}
	}

	/*
	 * Reuturns a the glue label of a specified face, "dir",  from the Cube in position "pos".
	 * Call this only if you have checked if a cube does exists at position "pos".
	*/
	public string checkGlues(Vector3 pos, string dir){
		Collider[] hitColliders = Physics.OverlapSphere(pos, 0.1f);

		if (dir == "Front")
			return hitColliders [0].gameObject.transform.Find ("Back").GetComponent<TextMesh> ().text;
		else if (dir == "Back")
			return hitColliders [0].gameObject.transform.Find ("Front").GetComponent<TextMesh> ().text;
		else if (dir == "Left")
			return hitColliders [0].gameObject.transform.Find ("Right").GetComponent<TextMesh> ().text;
		else if (dir == "Right")
			return hitColliders [0].gameObject.transform.Find ("Left").GetComponent<TextMesh> ().text;
		else if (dir == "Top")
			return hitColliders [0].gameObject.transform.Find ("Bottom").GetComponent<TextMesh> ().text;
		else if (dir == "Bottom")
			return hitColliders [0].gameObject.transform.Find ("Top").GetComponent<TextMesh> ().text;
		else return "";
	}

	/*
	 *Retrive a Cube that can attach to the assembly at postion "pos". 
	 *Check each cube in order and keep track of the sum of glue strengths.
	 *This can be further imporved by checking the cubes in random order
	 *rather than in order, but keep track of the cubes that you have already checked 
	 *so that you do not check them again.
	 */
	public _Cube getCube(Vector3 pos){
		_Cube tmpCube = new _Cube ();
		Collider[] hitColliders = Physics.OverlapSphere(pos, 0.1f);
		int sumGlueStrength = 0;

		for (int j = 0; j < setManager.CubeSet.Count; j++) {
			tmpCube = setManager.CubeSet [j];
			if (tmpCube.current_count != 0 || !setManager.discrete_counts_flag) {
				if (!isPositionEmpty (new Vector3 (pos.x, pos.y, pos.z - 1))) {
					string front_glue = checkGlues (new Vector3 (pos.x, pos.y, pos.z - 1), "Front");
					if (tmpCube.Front.label == front_glue) {
						for (int i = 0; i < setManager.Glues.Count; i++) {
							if (setManager.Glues [i].label == front_glue) {
								sumGlueStrength += setManager.Glues [i].strength;
							}
						}
					}
				}
				if (!isPositionEmpty (new Vector3 (pos.x, pos.y, pos.z + 1))) {
					string back_glue = checkGlues (new Vector3 (pos.x, pos.y, pos.z + 1), "Back");
					if (tmpCube.Back.label == back_glue) {
						for (int i = 0; i < setManager.Glues.Count; i++) {
							if (setManager.Glues [i].label == back_glue) {
								sumGlueStrength += setManager.Glues [i].strength;
							}
						}
					}
				}
				if (!isPositionEmpty (new Vector3 (pos.x + 1, pos.y, pos.z))) {
					string right_glue = checkGlues (new Vector3 (pos.x + 1, pos.y, pos.z), "Right");
					if (tmpCube.Right.label == right_glue) {
						for (int i = 0; i < setManager.Glues.Count; i++) {
							if (setManager.Glues [i].label == right_glue) {
								sumGlueStrength += setManager.Glues [i].strength;
							}
						}
					}
				}
				if (!isPositionEmpty (new Vector3 (pos.x - 1, pos.y, pos.z))) {
					string left_glue = checkGlues (new Vector3 (pos.x - 1, pos.y, pos.z), "Left");
					if (tmpCube.Left.label == left_glue) {
						for (int i = 0; i < setManager.Glues.Count; i++) {
							if (setManager.Glues [i].label == left_glue) {
								sumGlueStrength += setManager.Glues [i].strength;
							}
						}
					}
				}
				if (!isPositionEmpty (new Vector3 (pos.x, pos.y + 1, pos.z))) {
					string top_glue = checkGlues (new Vector3 (pos.x, pos.y + 1, pos.z), "Top");
					if (tmpCube.Top.label == top_glue) {
						for (int i = 0; i < setManager.Glues.Count; i++) {
							if (setManager.Glues [i].label == top_glue) {
								sumGlueStrength += setManager.Glues [i].strength;
							}
						}
					}
				}
				if (!isPositionEmpty (new Vector3 (pos.x, pos.y - 1, pos.z))) {
					string bottom_glue = checkGlues (new Vector3 (pos.x, pos.y - 1, pos.z), "Bottom");
					if (tmpCube.Bottom.label == bottom_glue) {
						for (int i = 0; i < setManager.Glues.Count; i++) {
							if (setManager.Glues [i].label == bottom_glue) {
								sumGlueStrength += setManager.Glues [i].strength;
							}
						}
					}
				}
				Debug.Log (tmpCube.name + "  " + sumGlueStrength);
				if (sumGlueStrength >= setManager.temperature) {
					break;
				}
				sumGlueStrength = 0;
			}
		}
		if (sumGlueStrength >= setManager.temperature) {
			if (setManager.discrete_counts_flag && (tmpCube.count > 0))
				tmpCube.current_count--;
			return tmpCube;
		}
		else {
				//tmpCube.name = "NULL";
			return new _Cube ();
		}
		
	}

	/*Retrive a random position from nextPositions.
	*/
	public Vector3 nextPosition(){
		int index = Random.Range(0,nextPositions.Count);
		//Vector3 pos = nextPositions[index];
		//nextPositions.RemoveAt (index);
		Vector3 pos = nextPositions[0];
		nextPositions.RemoveAt (0);
		return pos;
	}

	/*Attempt to attach a Cube at this position of the Assembly.
	 * Positions with potentional success of attachment are kept 
	 * in nextPositions.
	 */
	public void insertCube(Vector3 pos){
		if (isPositionEmpty(pos)) {
			_Cube cube = getCube (pos);
			Debug.Log (cube.name);
			if (cube.name != "NULL") {
				GameObject tmpCube = (GameObject)Instantiate (CubePrefab, new Vector3 (pos.x, pos.y, pos.z), new Quaternion ());
				cube.colour.alpha = 0.5f;
				tmpCube.GetComponent<Renderer> ().material.SetColor ("_Color", cube.colour.getColor());
				tmpCube.transform.Find ("Front").GetComponent<TextMesh> ().text = cube.Front.label;
				tmpCube.transform.Find ("Back").GetComponent<TextMesh> ().text = cube.Back.label;
				tmpCube.transform.Find ("Left").GetComponent<TextMesh> ().text = cube.Left.label;
				tmpCube.transform.Find ("Right").GetComponent<TextMesh> ().text = cube.Right.label;
				tmpCube.transform.Find ("Top").GetComponent<TextMesh> ().text = cube.Top.label;
				tmpCube.transform.Find ("Bottom").GetComponent<TextMesh> ().text = cube.Bottom.label;


				Vector3 tmpPos = new Vector3 (0, 0, 0);

				tmpPos = new Vector3 (1 + pos.x, pos.y, pos.z);
				if (isPositionEmpty (tmpPos))
					nextPositions.Add (tmpPos);
				tmpPos = new Vector3 (pos.x - 1, pos.y, pos.z);
				if (isPositionEmpty (tmpPos))
					nextPositions.Add (tmpPos);
				tmpPos = new Vector3 (pos.x, 1 + pos.y, pos.z);
				if (isPositionEmpty (tmpPos))
					nextPositions.Add (tmpPos);
				tmpPos = new Vector3 (pos.x, pos.y - 1, pos.z);
				if (isPositionEmpty (tmpPos))
					nextPositions.Add (tmpPos);
				tmpPos = new Vector3 (pos.x, pos.y, 1 + pos.z);
				if (isPositionEmpty (tmpPos))
					nextPositions.Add (tmpPos);
				tmpPos = new Vector3 (pos.x, pos.y, pos.z - 1);
				if (isPositionEmpty (tmpPos))
					nextPositions.Add (tmpPos);
			}
		} else {

		}
	}

	/*Check if a Position is empty.
	*/
	public bool isPositionEmpty(Vector3 pos){

		/*Collider[] hitColliders = Physics.OverlapSphere(pos, 0.1f);
		int i = 0;
		while (i < hitColliders.Count)
		{
			hitColliders[i].SendMessage("MessageDectionTest");
			i++;
		}*/

		if (Physics.CheckSphere (pos, 0.001f)) {
			//Debug.Log ("Nope!");
			return false;
		} else {
			//Debug.Log ("Yup!");
			return true;
		}
	}

	//***************************************************************************
	//***************************************************************************
	// Debugging Functions
	//***************************************************************************
	public void CreateAllCubeInList(){
		for (int i = 0; i < setManager.CubeSet.Count; i++) {
			GameObject tmpCube = (GameObject)Instantiate (CubePrefab, new Vector3 (i, 0, 0), new Quaternion ());

			//SET COLOR
			tmpCube.GetComponent<Renderer> ().material.SetColor ("_Color", setManager.CubeSet [i].colour.getColor());

			//SET GLUE TEXT
			tmpCube.transform.Find ("Front").GetComponent<TextMesh> ().text = setManager.CubeSet [i].Front.label;
			tmpCube.transform.Find ("Back").GetComponent<TextMesh> ().text = setManager.CubeSet [i].Back.label;
			tmpCube.transform.Find ("Left").GetComponent<TextMesh> ().text = setManager.CubeSet [i].Left.label;
			tmpCube.transform.Find ("Right").GetComponent<TextMesh> ().text = setManager.CubeSet [i].Right.label;
			tmpCube.transform.Find ("Top").GetComponent<TextMesh> ().text = setManager.CubeSet [i].Top.label;
			tmpCube.transform.Find ("Bottom").GetComponent<TextMesh> ().text = setManager.CubeSet [i].Bottom.label;
		}
	}
	public GameObject CreateFirstCubeInList_test1(){
		GameObject tmpCube = (GameObject)Instantiate (CubePrefab, new Vector3(0,0,0), new Quaternion());

		//SET COLOR
		tmpCube.GetComponent<Renderer> ().material.SetColor ("_Color", setManager.CubeSet[0].colour.getColor());
		//SET GLUE TEXT
		tmpCube.transform.Find("Front").GetComponent<TextMesh>().text = setManager.CubeSet[0].Front.label;
		tmpCube.transform.Find("Back").GetComponent<TextMesh>().text = setManager.CubeSet[0].Back.label;
		tmpCube.transform.Find("Left").GetComponent<TextMesh>().text = setManager.CubeSet[0].Left.label;
		tmpCube.transform.Find("Right").GetComponent<TextMesh>().text = setManager.CubeSet[0].Right.label;
		tmpCube.transform.Find("Top").GetComponent<TextMesh>().text = setManager.CubeSet[0].Top.label;
		tmpCube.transform.Find("Bottom").GetComponent<TextMesh>().text = setManager.CubeSet[0].Bottom.label;

		return tmpCube;
	}

	public void CreateFirstCubeInList()	
	{
		GameObject tmpCube = (GameObject)Instantiate (CubePrefab, new Vector3(0,0,0), new Quaternion());

		//SET COLOR
		tmpCube.GetComponent<Renderer> ().material.SetColor ("_Color", setManager.CubeSet[0].colour.getColor());

		//SET GLUE TEXT
		tmpCube.transform.Find("Front").GetComponent<TextMesh>().text = setManager.CubeSet[0].Front.label;
		tmpCube.transform.Find("Back").GetComponent<TextMesh>().text = setManager.CubeSet[0].Back.label;
		tmpCube.transform.Find("Left").GetComponent<TextMesh>().text = setManager.CubeSet[0].Left.label;
		tmpCube.transform.Find("Right").GetComponent<TextMesh>().text = setManager.CubeSet[0].Right.label;
		tmpCube.transform.Find("Top").GetComponent<TextMesh>().text = setManager.CubeSet[0].Top.label;
		tmpCube.transform.Find("Bottom").GetComponent<TextMesh>().text = setManager.CubeSet[0].Bottom.label;
	}


}
