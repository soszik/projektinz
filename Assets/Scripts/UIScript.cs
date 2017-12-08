using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
    public string parameter;
    private Dropdown dropdown;

    public void OnChange()
    {
        foreach (var a in GameObject.FindGameObjectsWithTag("Grupa " + dropdown.options[dropdown.value].text))
        {
            if (parameter == "speed")
            {
                RingScript script = a.GetComponent<RingScript>();
                if (script != null)
                {
                    script.speed = float.Parse(this.GetComponent<InputField>().text);
                }
            }
            else
            {
                FlyingObjectScript script = a.GetComponent<FlyingObjectScript>();
                if (script != null)
                {
                    switch (parameter)
                    {
                        case "bs":
                            script.bezierSpeed = Int32.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "vfx":
                            script.vibrationFrequency.x = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "vfy":
                            script.vibrationFrequency.y = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "vfz":
                            script.vibrationFrequency.z = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "vax":
                            script.vibrationAmplitude.x = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "vay":
                            script.vibrationAmplitude.y = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "vaz":
                            script.vibrationAmplitude.z = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "pfx":
                            script.pulsationFrequency.x = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "pfy":
                            script.pulsationFrequency.y = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "pfz":
                            script.pulsationFrequency.z = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "pamaxx":
                            script.pulsationAmplitudeMax.x = float.Parse(this.GetComponent<InputField>().text);
                            script.calculateDiff();
                            break;
                        case "pamaxy":
                            script.pulsationAmplitudeMax.y = float.Parse(this.GetComponent<InputField>().text);
                            script.calculateDiff();
                            break;
                        case "pamaxz":
                            script.pulsationAmplitudeMax.z = float.Parse(this.GetComponent<InputField>().text);
                            script.calculateDiff();
                            break;
                        case "paminx":
                            script.pulsationAmplitudeMin.x = float.Parse(this.GetComponent<InputField>().text);
                            script.calculateDiff();
                            break;
                        case "paminy":
                            script.pulsationAmplitudeMin.y = float.Parse(this.GetComponent<InputField>().text);
                            script.calculateDiff();
                            break;
                        case "paminz":
                            script.pulsationAmplitudeMin.z = float.Parse(this.GetComponent<InputField>().text);
                            script.calculateDiff();
                            break;
                        case "rsx":
                            script.rotationSpeed.x = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "rsy":
                            script.rotationSpeed.y = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "rsz":
                            script.rotationSpeed.z = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "rmaxx":
                            script.rotationMax.x = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "rmaxy":
                            script.rotationMax.y = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "rmaxz":
                            script.rotationMax.z = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "rminx":
                            script.rotationMin.x = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "rminy":
                            script.rotationMin.y = float.Parse(this.GetComponent<InputField>().text);
                            break;
                        case "rminz":
                            script.rotationMin.z = float.Parse(this.GetComponent<InputField>().text);
                            break;
                    }
                }
            }
        }
    }
    

    // Use this for initialization
    void Start () {
        dropdown = GameObject.FindGameObjectWithTag("drop").GetComponent<Dropdown>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
