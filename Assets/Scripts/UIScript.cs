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
                    float newspeed;
                    if (float.TryParse(this.GetComponent<InputField>().text, out newspeed))
                        script.speed = newspeed;
                }
            }
            else
            {
                FlyingObjectScript script = a.GetComponent<FlyingObjectScript>();
                if (script != null)
                {
                    float newPar;
                    if (float.TryParse(this.GetComponent<InputField>().text, out newPar))
                        switch (parameter)
                        {
                            case "bs":
                                script.bezierSpeed = (int)newPar;
                                break;
                            case "vfx":
                                script.vibrationFrequency.x = newPar;
                                break;
                            case "vfy":
                                script.vibrationFrequency.y = newPar;
                                break;
                            case "vfz":
                                script.vibrationFrequency.z = newPar;
                                break;
                            case "vax":
                                script.vibrationAmplitude.x = newPar;
                                break;
                            case "vay":
                                script.vibrationAmplitude.y = newPar;
                                break;
                            case "vaz":
                                script.vibrationAmplitude.z = newPar;
                                break;
                            case "pfx":
                                script.pulsationFrequency.x = newPar;
                                break;
                            case "pfy":
                                script.pulsationFrequency.y = newPar;
                                break;
                            case "pfz":
                                script.pulsationFrequency.z = newPar;
                                break;
                            case "pamaxx":
                                script.pulsationAmplitudeMax.x = newPar;
                                script.calculateDiff();
                                break;
                            case "pamaxy":
                                script.pulsationAmplitudeMax.y = newPar;
                                script.calculateDiff();
                                break;
                            case "pamaxz":
                                script.pulsationAmplitudeMax.z = newPar;
                                script.calculateDiff();
                                break;
                            case "paminx":
                                script.pulsationAmplitudeMin.x = newPar;
                                script.calculateDiff();
                                break;
                            case "paminy":
                                script.pulsationAmplitudeMin.y = newPar;
                                script.calculateDiff();
                                break;
                            case "paminz":
                                script.pulsationAmplitudeMin.z = newPar;
                                script.calculateDiff();
                                break;
                            case "rsx":
                                script.rotationSpeed.x = newPar;
                                break;
                            case "rsy":
                                script.rotationSpeed.y = newPar;
                                break;
                            case "rsz":
                                script.rotationSpeed.z = newPar;
                                break;
                            case "rmaxx":
                                script.rotationMax.x = newPar;
                                break;
                            case "rmaxy":
                                script.rotationMax.y = newPar;
                                break;
                            case "rmaxz":
                                script.rotationMax.z = newPar;
                                break;
                            case "rminx":
                                script.rotationMin.x = newPar;
                                break;
                            case "rminy":
                                script.rotationMin.y = newPar;
                                break;
                            case "rminz":
                                script.rotationMin.z = newPar;
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
