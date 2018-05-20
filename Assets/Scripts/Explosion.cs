using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    //-- Instaciación de un Objeto --
    public void InstantiateExplosion(Vector3 origin, Vector3 destiny)
    {
        //Instanciación de objeto
        GameObject explosion = Instantiate(this.gameObject, origin, Quaternion.identity);
        //Look At de la explosión
        explosion.transform.LookAt(destiny);
        //Se destruye el objeto a los 5 segundos
        Destroy(explosion, 1f);
    }
}
