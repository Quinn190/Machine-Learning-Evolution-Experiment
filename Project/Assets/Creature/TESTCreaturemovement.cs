using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class TESTCreaturemovement : MonoBehaviour {
	Rigidbody rb;
	public int movespeed;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		Debug.Log(transform.forward);
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		float movement = Input.GetAxis("Horizontal");
		rb.AddForce(movement * movespeed * Vector3.forward);
	}}
