using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public ModeEngine ME;
	public float rotationspeed = 1.7f;
	public Vector3 rotation;
	public float cameramovespeed = 0.3f;
	
	// Update is called once per frame
	void Start () {
		Vector3 rotation = transform.rotation.eulerAngles;
	}

	void Update () {
		if (Input.GetKey (KeyCode.RightArrow) && ME.Mode == "Camera Mode") {
			rotation.y += rotationspeed;
			transform.rotation = Quaternion.Euler (rotation);
		}
		if (Input.GetKey (KeyCode.LeftArrow) && ME.Mode == "Camera Mode") {
			rotation.y -= rotationspeed;
			transform.rotation = Quaternion.Euler (rotation);
		}
		if (Input.GetKey (KeyCode.UpArrow) && ME.Mode == "Camera Mode") {
			rotation.x -= rotationspeed;
			transform.rotation = Quaternion.Euler (rotation);
		}
		if (Input.GetKey (KeyCode.DownArrow) && ME.Mode == "Camera Mode") {
			rotation.x += rotationspeed;
			transform.rotation = Quaternion.Euler (rotation);
		}
		if (Input.GetKey (KeyCode.W) && ME.Mode == "Camera Mode") {
			transform.position += transform.forward * cameramovespeed;
		}
		if (Input.GetKey (KeyCode.A) && ME.Mode == "Camera Mode") {
			transform.position -= transform.right	 * cameramovespeed;
		}
		if (Input.GetKey (KeyCode.S) && ME.Mode == "Camera Mode") {
			transform.position -= transform.forward * cameramovespeed;
		}
		if (Input.GetKey (KeyCode.D) && ME.Mode == "Camera Mode") {
			transform.position += transform.right * cameramovespeed;
		}
	}
}