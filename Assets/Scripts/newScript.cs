using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newScript : MonoBehaviour
{
	public WheelCollider[] wheelColliders = new WheelCollider[4];
	public Transform[] tyreMeshes = new Transform[4];
	public float maxTorque = 50.0f;
	private Rigidbody m_rigidbody;
	public Transform centerOfMass;
	public int jumpHeight = 1;
	public float horizontalSpeed = 0.15f;

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

		if (Input.GetKeyDown("space"))
		{
			m_rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(Vector3.right * horizontalSpeed);
		}

		if (Input.GetKey(KeyCode.LeftArrow))
        {
			transform.Translate(Vector3.left * horizontalSpeed);
		}
	}
}
