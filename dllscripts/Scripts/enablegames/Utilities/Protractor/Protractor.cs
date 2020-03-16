﻿using UnityEngine;
using System.Collections;

public class Protractor : MonoBehaviour {
	public float radius = 0.05f;
	public float scale = 0.7f;
	public Vector3 textOffset;
	public string label;
	public string[] DOFslabel;


	public GameObject joint;
	public bool flipped = false;
	public bool horizPies = false;
	private Transform parent,child;


	// Material used for the connecting lines
	public Material lineMat;

	GameObject[] pieGameObjects;
	GameObject[] textGameObjects;

	//public Material textMat;
	//public Font textFont;
	public Vector3 textScale;
	//public string unitName;
	public bool draw = false;

//	TextMesh textMesh;
//	PieChartMesh mPieChart;
	float[] mData;
	public float delay=0.1f;
	public Material mainMaterial;
	public Material[] materials;
	public int segments;
	public int DOFs;
	private int randomBuffer;
	private bool initted=false;
	// Use this for initialization
	void Start () {
		 parent = joint.transform.parent;
		 child = joint.transform.GetChild (0);
		materials = new Material[segments];
		mData = new float[2];

		Color32 color=new Color32((byte)Random.Range(0,256),(byte)Random.Range(0,256),(byte)Random.Range(0,256),(byte)255);
		for (int i=0; i<segments; i++) {
			materials[i]= new Material(mainMaterial);

			materials [i].color = new Color32 ((byte)(color.r / (i + 1.0f)), (byte)(color.g / (i + 1.0f)),(byte) (color.b / (i + 1.0f)),(byte)255);
			//Debug.LogError(materials[i].color);
		}
		PieChartMesh mPieChart;

		Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
//		GameObject textObject = new GameObject();
		this.pieGameObjects = new GameObject[DOFs];
		this.textGameObjects = new GameObject[DOFs];
		for (int i = 0; i < DOFs; i++) {
			// Make a gameobject that we will put the ring on
			// And then put it as a child on the gameobject that has this Command and Control script

			this.textGameObjects [i] = new GameObject ();
			this.textGameObjects [i].name = "Angle text#" + i;
			this.textGameObjects [i].transform.parent = this.gameObject.transform;
			if (horizPies)
				this.textGameObjects [i].transform.localPosition =  new Vector3 ((float) i * scale, 0, 0)+ textOffset;
			else
				this.textGameObjects [i].transform.localPosition =  new Vector3 (0, (float) i* scale, 0)+ textOffset;
			TextMesh textMesh;
			GameObject textObject = new GameObject();
			textMesh = textObject.AddComponent<TextMesh> ();
			textMesh.font = ArialFont;//textFont;
			textMesh.color = Color.yellow;
			textMesh.text = "Angle pie chart";
			textObject.transform.parent = this.textGameObjects [i].transform;
			Vector3 textLocalScale = textScale;
			textLocalScale.x = -textLocalScale.x;
			textObject.transform.localScale = textLocalScale;
			textObject.transform.localPosition =  new Vector3 ((float) 0, 0, 0);

			this.pieGameObjects [i] = new GameObject ();
			this.pieGameObjects [i].name = "Pie #" + i;
			this.pieGameObjects [i].transform.parent = this.gameObject.transform;
			if (horizPies)
				this.pieGameObjects [i].transform.localPosition =  new Vector3 ((float) i* scale, 0.3f, 0);
			else
				this.pieGameObjects [i].transform.localPosition =  new Vector3 (0, (float) i * scale + 0.3f, 0);
			//Make up to 3 pie charts for each of the 3 joint dimenstions (i.e. abduction, elevation, etc.) or (saginal plane, etc.)

			mPieChart = this.pieGameObjects [i].AddComponent<PieChartMesh>() as PieChartMesh;
			//mPieChart = this.gameObject.AddComponent<PieChartMesh>() as PieChartMesh;
			if (mPieChart != null)
			{
				mPieChart.Init(mData, 100, 0, 100, materials,delay);
				//mData = GenerateRandomValues(segments);
				mData [0] = 50.0f;
				mData [1] = 45.0f;
				mPieChart.Draw(mData);
			}
				
			initted = true;
		}


	}

	//
	float SignedProjectedAngle(Vector3 a, Vector3 b, Vector3 planeNormal){
		Vector3 projA = a - planeNormal * Vector3.Dot (a,planeNormal);
		projA.Normalize ();
		return SignedAngle (projA, b, planeNormal);
	}

	//signed angle between two vectors with sign determined by planeNormal (a.b is positive when aXb is in planenormal direction
	/*
	 * Use cross product of the two vectors to get the normal of the plane formed by the two vectors. 
	 * Then check the dotproduct between that and the original plane normal to see if they are facing the same direction.
	 */
	float SignedAngle(Vector3 a, Vector3 b, Vector3 planeNormal){
		float vcos = Vector3.Dot (a, b);  
		Vector3 cross = Vector3.Cross (a, b);
		float angle = Mathf.Atan2 (Vector3.Dot (cross, planeNormal), vcos);
		return angle;
	}

	// Update is called once per frame
	void Update () {
		if (!initted)
			return;
		Vector3 parentRot = joint.transform.position - parent.position;
		Vector3 boneDir = child.position -joint.transform.position;
		parentRot.Normalize ();
		boneDir.Normalize ();
		float vcos = Vector3.Dot (parentRot, boneDir);
		float rad = Mathf.Acos (vcos);
		float deg = rad * Mathf.Rad2Deg;
		//Debug.Log("Angle:" + rad + " radians (" + deg + " degrees)");	

		//parent orientation used to determine components of anglular motion of child relative to it
		/* for shoulder, green is in direction of bone, blue is up, and red in z direction away from camera*/
		Vector3 pVAPlaneNormal = parent.right;                  //R - clavical backward, normal to calc sign for vert abduction
		Vector3 pBoneDir = parent.up;               //G  - clavical bone dir
		Vector3 pHAPlaneNormal = parent.forward;   //B - clavical UP, rotation plane normal for horizontal abduction
		if (flipped) {
			pBoneDir = -pBoneDir;
			pVAPlaneNormal = -pVAPlaneNormal;
			pHAPlaneNormal = -pHAPlaneNormal;
		}
		//Debug.Log ("parentRot = " + parentRot + ", boneDir = " + boneDir);
		//Debug.Log ("VAplaneNormal = " + pVAPlaneNormal + ", pBoneDir = " + pBoneDir + ", pHAPlaneNormal = " + pHAPlaneNormal);

		//shoulder horiz Abduction (rotation in horizontal plane (UP normal vector)
		float horizAbduct= - SignedProjectedAngle(boneDir,pBoneDir,pHAPlaneNormal);
		if (flipped)
			horizAbduct = -horizAbduct;
		float degHA = horizAbduct * Mathf.Rad2Deg;
		if (flipped)
			horizAbduct = -horizAbduct;
		

		//pRotPlaneNormal = parent.right;             //R - clavical backward, normal to calc sign for vert abduction
		float vertAbduct= SignedAngle(boneDir,-pHAPlaneNormal,-pVAPlaneNormal);
		if (flipped)
			vertAbduct = -vertAbduct;
		float degVA = vertAbduct * Mathf.Rad2Deg;
		//if (Input.GetKeyDown ("z"))
		//	draw = !draw;
		//if (draw == true)


		TextMesh textMesh;

		if (DOFs == 1)
		{
			pHAPlaneNormal = Vector3.Cross (boneDir, pBoneDir);
			float simpleAngle= SignedAngle(boneDir,pBoneDir,pHAPlaneNormal);
			//if (flipped)
			//	simpleAngle = -simpleAngle;
			float degSA = simpleAngle * Mathf.Rad2Deg;
			//Debug.Log("V_ABD:" + vertAbduct + " radians (" + degVA + " degrees)");	
			textMesh = this.textGameObjects [0].transform.GetChild(0).GetComponent<TextMesh>();
			textMesh.text = label + " "  + degSA.ToString("F2") + " degrees";

			//Debug.Log ("drawing");
			//randomBuffer = Random.seed;
			//Random.seed = 10;
			//mData = GenerateRandomValues(segments);
			deg=degSA;
			if (deg < 0) {
				deg = -deg;
				this.pieGameObjects [0].transform.localScale = new Vector3 (-1.0f, 1.0f, 1.0f);
			} else {
				this.pieGameObjects [0].transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			}
			mData [0] = deg;
			mData [1] = 360.0f - deg;
			//mData [2] = 0;
			PieChartMesh mPieChart;
			mPieChart = this.pieGameObjects [0].GetComponent<PieChartMesh>();
			mPieChart.Draw(mData);
			//Random.seed = randomBuffer;
		}
		if (DOFs == 2)
		{
			//Debug.Log("H_ABD:" + horizAbduct + " radians (" + degHA + " degrees)");	

			textMesh = this.textGameObjects [1].transform.GetChild(0).GetComponent<TextMesh>();
			textMesh.text = label + DOFslabel[1] + " "  + degHA.ToString("F2") + " degrees"; //"H_ABD:" 
			//Debug.Log("V_ABD:" + vertAbduct + " radians (" + degVA + " degrees)");	
			textMesh = this.textGameObjects [0].transform.GetChild(0).GetComponent<TextMesh>();
			textMesh.text = label +DOFslabel[0]  + " "  + degVA.ToString("F2") + " degrees"; //"V_ABD:"

			//Debug.Log ("drawing");
			//randomBuffer = Random.seed;
			//Random.seed = 10;
			//mData = GenerateRandomValues(segments);
			deg=degVA;
			if (deg < 0) {
				deg = -deg;
				this.pieGameObjects [0].transform.localScale = new Vector3 (-1.0f, 1.0f, 1.0f);
			} else {
				this.pieGameObjects [0].transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			}
			mData [0] = deg;
			mData [1] = 360.0f - deg;
			//mData [2] = 0;
			PieChartMesh mPieChart;
			mPieChart = this.pieGameObjects [0].GetComponent<PieChartMesh>();
			mPieChart.Draw(mData);
			//Random.seed = randomBuffer;

			deg=degHA;
			//deg = 1.6f;
			if (deg < 0) {
				deg = -deg;
				this.pieGameObjects [1].transform.localScale = new Vector3 (-1.0f, 1.0f, 1.0f);
			} else {
				this.pieGameObjects [1].transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			}
			mData [0] = deg;
			mData [1] = 360.0f - deg;
			//mData [2] = 0;
			mPieChart = this.pieGameObjects [1].GetComponent<PieChartMesh>();
			mPieChart.Draw(mData);

		}
	}


}


