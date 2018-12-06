using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public GameObject collisionPointPrefab;
    public bool freezeOnCollision;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Target")
            return;

        if (freezeOnCollision)
            Time.timeScale = 0f;
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            Debug.Log("Position:" + collision.contacts[i].point);
            Debug.Log("Impulse:" + collision.impulse);
            Debug.Log("Relative Velocity:" + collision.relativeVelocity);
            Debug.Log("------");
            Instantiate(collisionPointPrefab, collision.contacts[i].point, Quaternion.identity);
        }

        TriangleExplosion te = collision.gameObject.GetComponent<TriangleExplosion>();
        if (te)
            te.SetExplosion(transform.position, collision.impulse, true);
    }
}
