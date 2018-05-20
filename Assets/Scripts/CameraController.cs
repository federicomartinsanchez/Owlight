using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Wizard;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - Wizard.transform.position;
    }
    
    void LateUpdate()
    {
        transform.position = Wizard.transform.position + offset;
    }
}
