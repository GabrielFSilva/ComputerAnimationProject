using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimulationManager : MonoBehaviour {

    public List<Rigidbody> projectiles;
    public Vector3 throwForce;

	// Use this for initialization
	void Start () {
        TimeScaleManager.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Rigidbody rb in projectiles)
            { 
                rb.isKinematic = false;
                rb.AddForce(throwForce, ForceMode.Impulse);
            }
        }
       
    }
}
