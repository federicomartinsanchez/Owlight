using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionController : MonoBehaviour {

	private void OnTriggerEnter(Collider collider) {
		
		if (collider.tag == "Wizard"){
            WizardController.Instance.addMana();
        }
	}
}
