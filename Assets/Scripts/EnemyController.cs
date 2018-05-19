using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour {

	public NavMeshAgent agent;
	public Transform Wizard;

	void Update () {
		agent.SetDestination(Wizard.position);
        if (Vector3.Distance(this.transform.position, Wizard.transform.position) > 10f)
        {
            //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
	}
	
	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.name == "Wizard" )
		{
			int wizardHealth = WizardController.Instance.takeDamage(1);
			UIManager.Instance.modifyLifeText(wizardHealth.ToString());

		}
	}
	private void Attack(){
		
	}
}
