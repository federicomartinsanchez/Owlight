using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlController : MonoBehaviour {  public float moveTime = 1.0f;
    float owlAltittude = 5.0f;
    private Transform owlProjection;
    Camera cam;

    void Start () {
        cam = Camera.main;
		//transform.position = new Vector3 (transform.position.x, owlAltittude,transform.position.x );
        owlProjection = transform.GetChild(0).transform;
    }
    
    void Update () {
        MovePosToPointer();
		CallWizardToMove();
        //transform.forward = 
	}

    void MovePosToPointer() {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
			if (hit.transform.tag == "flyingplane"){
            StartCoroutine(LerpPosition(hit.point, moveTime));
			}
            Vector3 vectorA = transform.position - hit.point;
            if(vectorA.magnitude > 0.1f)
                transform.LookAt(hit.point);
        }
    }
	public void CallWizardToMove()
	{


		if (Input.GetMouseButtonDown(0))
		{
			WizardController.Instance.MoveTo(owlProjection.transform.position);
		}
        if (Input.GetMouseButtonDown(1))
		{
			WizardController.Instance.TriggerAttackAnimation(transform.position);
		}
	}
    private IEnumerator LerpPosition(Vector3 newPos, float time){
        float elapsedTime = 0.0f;
        Vector3 startPos = transform.position;
        while (elapsedTime < time) {
            transform.position = Vector3.Lerp(startPos, newPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
    }
}
