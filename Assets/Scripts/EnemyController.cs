using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour {

	public NavMeshAgent agent; 

	private Transform Wizard;

    private void Start()
    {
        Wizard = WizardController.Instance.transform;
    }

    void Update ()
    {
		agent.SetDestination(Wizard.position);
	}

    //-- Ataque de los enemigos --
	private void Attack()
    {
		
	}

    //-- Colisión de los enemigos --
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wizard")
        {
            int wizardHealth = WizardController.Instance.takeDamage(1);
        }
    }

    //-- Instaciación de un Enemigo --
    public void InstantiateEnemy(Vector3 spawnPoint)
    {
        //Instanciación de objeto
        GameObject wave = Instantiate(this.gameObject, spawnPoint, Quaternion.identity);
    }

    //-- Muerte de los Goblins --
    public void Die()
    {
        UIManager.Instance.ActualiceScore();
        Destroy(this.gameObject);
    }

}
