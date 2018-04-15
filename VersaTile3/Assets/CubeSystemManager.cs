using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class CubeSystemManager : MonoBehaviour {
	public CubeSystemManager DefaultInstance{
		get{ return ThisInstance; }
	}
	private static CubeSystemManager ThisInstance = null;

	void Awake(){
		if (ThisInstance != null) {
			DestroyImmediate (gameObject);
			return;
		}

		ThisInstance = this;
		DontDestroyOnLoad (gameObject);
	}
	public _Cube Seed;
	public List<_Cube> CubeSet;
	public List<_Glue> Glues;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			DisplayGlues ();
		}
	}

	void DisplayGlues(){
		for (int i = 0; i < Glues.Count; i++) {
			Debug.Log (Glues [i].label + Glues[i].strength);
		}
	}
	public List<string> GetListOfLabels(){
		List<string> labels = new List<string> ();
		for (int i = 0; i < Glues.Count; i++) {
			labels.Add(Glues [i].label);
		}

		return labels;
	}
}

[System.Serializable]
public class _Color{
	public float red;
	public float green;
	public float blue;
	public float alpha;
	public _Color(){
		red = 0;
		green = 0;
		blue = 0;
		alpha = 0.5f;
	}

	public void setColor(float r, float g, float b, float a){
		red = r;
		green = g;
		blue = b;
		//alpha = a;
	}
	public Color getColor(){
		Color color = new Color ();
		color.r = red;
		color.b = blue;
		color.g = green;
		color.a = alpha;
		return color;
	}
}
[System.Serializable]
public class _Cube{
	public string name;
	public _Glue Front;
	public _Glue  Back;
	public _Glue  Left;
	public _Glue  Right;
	public _Glue  Top;
	public _Glue  Bottom;


	public _Color colour;

	public _Cube(){
		name = "NULL";
		colour = new _Color();
		Front = new _Glue ();
		Back = new _Glue ();
		Right = new _Glue ();
		Left = new _Glue ();
		Top = new _Glue ();
		Bottom = new _Glue ();
	}

	public _Cube(string n,Color c, _Glue f, _Glue b, _Glue r, _Glue l, _Glue t, _Glue bo){
		//colour.
		name = n;
		colour = new _Color();
		colour.setColor(c.r,c.g,c.b,c.a);
		Front = f;
		Back = b;
		Right = r;
		Left = l;
		Top = t;
		Bottom = bo;
	}
}

[System.Serializable]
public class _Glue{
	public string label; 
	public int strength;

	public _Glue(){
		label = "none";
		strength = 0; 
	}
	public _Glue(string l, int s){
		label = l;
		strength = s;
	}
}

[System.Serializable]
public class SavingAndLoadingClass{
	public _Cube Seed;
	public List<_Cube> CubeSet;
	public List<_Glue> Glues;

	public SavingAndLoadingClass(){

	}
	public SavingAndLoadingClass( List<_Cube> c, List<_Glue> g, _Cube s){
		CubeSet = c;
		Glues = g;
		Seed = s;
	}
	public void setData( List<_Cube> c, List<_Glue> g, _Cube s){
		CubeSet = c;
		Glues = g;
		Seed = s;
	}
	public SavingAndLoadingClass getData(){
		return this;
	}
}
	