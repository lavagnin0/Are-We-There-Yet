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
	public int jumpHeight = 4;
	public float horizontalSpeed = 50.0f;
	public float horizontalInput;
	public float forwardInput;
	public float speed = 1.0f;

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
		bool isGrounded = true;
		horizontalInput = Input.GetAxis("Horizontal");
		forwardInput = Input.GetAxis("Vertical");

		for (int i = 0; i < 4; i++)
		{
			wheelColliders[i].motorTorque = forwardInput * maxTorque;
			if (!wheelColliders[i].isGrounded)
			{
				isGrounded = false;
			}
		}
		
		if (Input.GetButton("Jump") && isGrounded)
		{
			m_rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
		}
		transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
		transform.Rotate(Vector3.up, Time.deltaTime * horizontalSpeed * horizontalInput);
	}
}
