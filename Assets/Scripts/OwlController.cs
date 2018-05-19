using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlController : MonoBehaviour {  public float moveTime = 1.0f;
    float owlAltittude = 5.0f;
    Camera cam;

    void Start () {
        cam = Camera.main;
		transform.position = new Vector3 (transform.position.x, owlAltittude,transform.position.x );
    }
    
    void Update () {
        MovePosToPointer();
		CallWizardToMove();
	}

    void MovePosToPointer() {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
			if (hit.transform.tag == "flyingplane"){
            StartCoroutine(LerpPosition(hit.point, moveTime));
			}
        }
    }
	public void CallWizardToMove()
	{
		if (Input.GetMouseButton(0))
		{
			WizardController.Instance.MoveTo(transform.position);
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
