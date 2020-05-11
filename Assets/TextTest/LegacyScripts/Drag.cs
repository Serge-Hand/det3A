using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

	LineRenderer line;

	//float magn;
	Vector3 pt;
	//bool flag = false;

	Vector3 camPos;

	void Start () {
		line = gameObject.AddComponent<LineRenderer> ();
		line.startWidth = 0.003f;

		line.startColor = Color.red;
		line.endColor = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (flag) {
			Debug.DrawLine (transform.position, pt);
			Debug.DrawRay(camPos, (pt - camPos).normalized, Color.red);
		}
		*/
	}

	void OnMouseDown()
	{
		line.enabled = true;
	}

	void OnMouseDrag()
	{
		pt = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1)); //правая точка

		//Debug.DrawLine (transform.position, pt);

		camPos = Camera.main.transform.position;
		//Debug.DrawRay(camPos, (pt - camPos).normalized, Color.red);

		line.SetPosition (0, (transform.position - camPos).normalized + camPos); //левая точка
		line.SetPosition (1, pt);

		RaycastHit hit;
		if(Physics.Raycast(camPos, (pt - camPos).normalized, out hit))
			Debug.Log(hit.collider.gameObject.name);

		//line.SetPosition (0, transform.position);
		//line.SetPosition (1, hit.point);
	}

	void OnMouseUp()
	{
		MeshRenderer mr;

		line.enabled = false;
		//flag = true;

		RaycastHit hit;
		if (Physics.Raycast (camPos, (pt - camPos).normalized, out hit))
		if (hit.collider.gameObject.tag.Equals ("Work")) {
			Debug.LogWarning (hit.collider.gameObject.name);
			mr = hit.collider.gameObject.GetComponent<MeshRenderer> ();
			mr.material.color = Color.red;
		}
	}
}
