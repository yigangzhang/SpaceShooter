﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float Htilt, Vtilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
		
	private float nextFire;

	void Update () 
	{
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
//		if (Input.GetKey ("joystick button 15") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			audio.Play ();
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		rigidbody.position = new Vector3 
		(
			Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
		);

//		rigidbody.rotation = Quaternion.Euler (rigidbody.velocity.z * Vtilt, 0.0f, 0.0f);
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -Htilt);
	}
}
