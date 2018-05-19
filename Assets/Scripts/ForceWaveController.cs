using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceWaveController : MonoBehaviour
{
    private float dieInSeconds = 1f; //Delay de destrucción de la bala si no colisiona con nada

    //-- Instaciación de un Objeto --
    public void InstantiateWave(GameObject origin)
    {
        //Instanciación de objeto
        GameObject wave = Instantiate(this.gameObject, origin.transform.position, Quaternion.identity);
        //Se destruye el objeto a los 5 segundos
        Die(wave, dieInSeconds);
    }

    //-- Destrucción de la ForceWave --
    private void Die(GameObject wave, float dieInSeconds)
    {
        //Destrucción de la ForceWave
        Destroy(wave, dieInSeconds);
    }

    //-- Colisiones con la FireBall como trigger --
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            //collision.transform.InverseTransformDirection(collision.transform.position - transform.position);
            /*Vector3 repulsionVector = new Vector3(
                                                    (collision.transform.position.x - this.transform.position.x)*3f,
                                                    0f,
                                                    (collision.transform.position.z + this.transform.position.z)*3f);
            collision.transform.position = collision.transform.InverseTransformDirection(repulsionVector);*/
            Vector3 repulsionVector = collision.transform.position;
            repulsionVector = repulsionVector.normalized;
            collision.GetComponent<Rigidbody>().AddForce(repulsionVector * 650f);
        }
    }
}
