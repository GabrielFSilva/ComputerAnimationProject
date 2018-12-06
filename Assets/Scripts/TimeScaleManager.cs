using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeScaleManager : MonoBehaviour
{
    #region Singleton
    private static TimeScaleManager _instance;
    public static TimeScaleManager GetInstance()
    {
        if (_instance == null)
        {
            GameObject GO = new GameObject("TimeScaleManager");
            GO.AddComponent<TimeScaleManager>();
            _instance = GO.GetComponent<TimeScaleManager>();
            DontDestroyOnLoad(GO);
        }
        return _instance;
    }
    #endregion
    
    public TextMeshProUGUI timeScaleLabel;

    public void Awake()
    {
        GameObject GO = Instantiate(Resources.Load<GameObject>("TimeScaleCanvas"));
        GO.transform.SetParent(transform);
        GO.transform.localScale = Vector3.one;
        timeScaleLabel = GO.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
    }
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Time.timeScale = 0f;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Time.timeScale = 0.1f;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Time.timeScale = 0.25f;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            Time.timeScale = 0.5f;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            Time.timeScale = 1f;
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
            Time.timeScale += 0.05f;
        if ((Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)) && Time.timeScale >= 0.05f)
            Time.timeScale -= 0.05f;

        timeScaleLabel.SetText("Time Scale: " + Time.timeScale.ToString("n2"));
    }
}
