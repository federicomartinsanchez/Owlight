using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public static UIManager Instance;
 
	// Use this for initialization
	public Text wizardLife;
    protected void Awake ()
    {
		Instance = this;
    }
	public void modifyLifeText(string text)
	{
		wizardLife.text = text; 
	}
}
