using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSwitcher : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        transform.GetChild(0).gameObject.SetActive(true);
        for (int i = 1; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
	}

    public void activatePotion(int num) {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(num).gameObject.SetActive(true);
    }

}
