using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MipmapText : MonoBehaviour {

	Text txt;
	RectTransform rectTr;
	[SerializeField] float bigRadius = 5;
	[SerializeField] float radius = 3;

	void Start () {
		txt = GetComponent<Text> ();
		rectTr = gameObject.GetComponent<RectTransform> ();
	}

	void LateUpdate () {
		if ((transform.position - Camera.main.transform.position).magnitude > bigRadius) {
			rectTr.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, 115);
			rectTr.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 160);
			rectTr.localScale = new Vector3 (0.15f, 0.15f, 0.01f);
			txt.fontSize = 5;
		} else {
			if ((transform.position - Camera.main.transform.position).magnitude < radius) {
				rectTr.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, 3500);
				rectTr.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 5000);
				rectTr.localScale = new Vector3 (0.005f, 0.005f, 0.01f);
				txt.fontSize = 150;
			} else {
				rectTr.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, 350);
				rectTr.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 500);
				rectTr.localScale = new Vector3 (0.05f, 0.05f, 0.01f);
				txt.fontSize = 15;
			}
		}
		// fontsize 5 scale 0.15 0.15 ; w 115 h 160
	}
}
