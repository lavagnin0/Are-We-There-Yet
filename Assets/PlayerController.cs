using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public WheelCollider[] wheelColliders = new WheelCollider[4];
	public Transform[] tyreMeshes = new Transform[4];
	public float maxTorque = 50.0f;
	private Rigidbody m_rigidbody;
	public Transform centerOfMass;
	public int jumpHeight = 1;
	public float horizontalSpeed = 0.15f;
	public float horizontalInput;
	public float forwardInput;
	public float speed;

	void Start()
	{
		m_rigidbody = GetComponent<Rigidbody>();
		//m_rigidbody.centerOfMass = centerOfMass.localPosition;
	}

	void OnCollisionEnter(Collision c)
	{
		string n = c.gameObject.name;
        if (n == "RespawnPlane" || n == "CylinderR" || n == "CylinderL")
		{
			respawn();
        }
    }

	void respawn()
    {
		// respawn to the beginning
		transform.position = new Vector3(0, 0.3f, 0);
		transform.rotation = Quaternion.Euler(0, 0, 0);
	}


    void FixedUpdate()
	{
		float steer = Input.GetAxis("Horizontal");
		float fixedAngel = steer * 45f;

		float acceleration = Input.GetAxis("Vertical");
		for (int i = 0; i < 4; i++)
		{
			wheelColliders[i].motorTorque = acceleration * maxTorque;
		}
		horizontalInput = Input.GetAxis("Horizontal");
		forwardInput = Input.GetAxis("Vertical");

		transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
		transform.Rotate(Vector3.up, Time.deltaTime * horizontalSpeed * horizontalInput);
	}
}
