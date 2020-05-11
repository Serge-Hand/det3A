using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	[SerializeField] float speed = 1;

	void LateUpdate()
	{
		transform.Translate( new Vector3 (Input.GetAxis("Horizontal")*speed, 0, Input.GetAxis("Vertical")*speed), Space.World );
	}
}
