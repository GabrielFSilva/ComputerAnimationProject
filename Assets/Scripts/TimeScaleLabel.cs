using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleLabel : MonoBehaviour {

    public Text label;
	// Update is called once per frame
	void Update () {
        label.text = "Time Scale = " + Time.timeScale.ToString();
	}
}
