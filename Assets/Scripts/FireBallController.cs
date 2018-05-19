using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{

    private float dieInSeconds = 5f; //Delay de destrucción de la bala si no colisiona con nada

    //-- Instaciación de un Objeto --
    public void InstantiateProjectile(GameObject origin, Vector3 destiny, float speed)
    {
        //Instanciación de objeto
        GameObject fireball = Instantiate(this.gameObject, origin.transform.position, Quaternion.identity);
        //El proyectil también se rota hacia esa posición
        fireball.transform.LookAt(destiny);
        //Se aplica la fuerza de desplazamiento
        fireball.GetComponent<Rigidbody>().AddForce(origin.transform.forward * 1000f);
        //Se destruye el objeto a los 5 segundos
        Die(fireball, dieInSeconds);
    }

    //-- Destrucción de la FireBall --
    private void Die(GameObject fireball, float dieInSeconds)
    {
        //Metodo preparado por si se quiere meter una explosión o algo

        //Destrucción de la FireBall
        Destroy(fireball, dieInSeconds);
    }

    //-- Colisiones con la FireBall como trigger --
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Wall")
            Die(this.gameObject, 0f);
    }

}