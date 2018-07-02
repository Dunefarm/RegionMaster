using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintsShitInUpdate : MonoBehaviour {

    public string ShitToPrint = "Shit.";
	
        
    void Update () {
        print(ShitToPrint);
	}
}
