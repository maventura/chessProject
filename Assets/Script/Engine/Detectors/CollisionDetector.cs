using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionDetector : MonoBehaviour
{

    public List<Collider> colliders =  new List<Collider>();
	// Use this for initialization

    private Rigidbody _rigidBody;

    public void Init()
    {
        _rigidBody = this.gameObject.AddComponent<Rigidbody>();
        _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }

    void OnCollisionEnter(Collision c)
    {
        if (!colliders.Contains(c.gameObject.GetComponent<Collider>())) colliders.Add(c.gameObject.GetComponent<Collider>());
    }

    void OnCollisionExit(Collision c)
    {
        if (colliders.Contains(c.gameObject.GetComponent<Collider>())) colliders.Remove(c.gameObject.GetComponent<Collider>());
    }
}
