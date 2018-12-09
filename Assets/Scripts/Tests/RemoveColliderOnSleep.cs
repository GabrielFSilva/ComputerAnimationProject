using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveColliderOnSleep : MonoBehaviour {

    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update () {
		if (rb.IsSleeping())
        {
            Destroy(rb);
            Destroy(GetComponent<BoxCollider>());
            Destroy(this);
        }
	}
}
