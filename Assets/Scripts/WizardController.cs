using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WizardController : MonoBehaviour
{
    public static WizardController Instance;

    public Camera cam;
    public GameObject owl;
    public GameObject fireball; // Prefab de la FireBall
    public GameObject forcewave; // Prefab de la ForeWave
    public GameObject spawnProjectile; // Punta del bastón de donde spawnean ataques
    public int _health = 10;

    private NavMeshAgent agent; // El mago es un NavMeshAgent
    private Vector3 belowOwl; // Donde apunta el mago hacia el búho pero a la altura de enemigos
    private bool isFireballCD = false; // CD de la habilidad FireBall
    private bool isForcewaveCD = false; // CD de la habilidad ForceWave
    private float fireballCD = 1f; // Delay de CD de la habilidad FireBall
    private float forcewaveCD = 1f; // Delay de CD de la habilidad ForceWave
    private float fireballSpeed = 15f; // Velocidad de la FireBall

    private void Awake()
    {
        Instance = this;
    }

    private void Start ()
    {
        //Inicialización de componentes
        UIManager.Instance.modifyLifeText("10");
        agent = GetComponent<NavMeshAgent>();
        belowOwl = Vector3.zero;
	}
	
	private void Update ()
    {
        //Actualiza posición del búho pero a la altura del bastón del mago
        belowOwl = new Vector3(owl.transform.position.x, spawnProjectile.transform.position.y, owl.transform.position.z);

        //LLAMADAS A ACCIONES QUE HAY QUE CAMBIAR PORQUE VIENEN DE FUERA
        if (Input.GetMouseButtonDown(1))
        {
            Attack(1, belowOwl);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack(2, belowOwl);
        }
        //BORRAR HASTA AQUÍ
	}

    //-- Quitar vida al mago --
    public int takeDamage(int Damage)
    {
        _health -= Damage;

        return _health;
    }

    //-- Movimiento del mago --
    public void MoveTo(Vector3 destiny)
    {
        //Setea posición de destino del NavMeshAgent
        agent.SetDestination(destiny);
    }
    
    //-- Método genérico de habilidades --
    public void Attack (int attack, Vector3 destiny)
    {
        //Diferentes tipos de ataque
        switch (attack)
        {
            case 1:
                if (!isFireballCD)
                    StartCoroutine(FireProjectile(destiny));
                break;
            case 2:
                if (!isForcewaveCD)
                    StartCoroutine(ForceWave());
                break;
        }
    }

    //-- Proyectil de bola de fuego --
    private IEnumerator FireProjectile(Vector3 destiny)
    {
        //LookAt del mago y del bastón hacia la posición de ataque
        this.transform.LookAt(belowOwl);
        spawnProjectile.transform.LookAt(belowOwl);
        //Cooldown e Instanciación de la FireBall
        isFireballCD = true;
        fireball.GetComponent<FireBallController>().InstantiateProjectile(spawnProjectile, destiny, fireballSpeed);
        yield return new WaitForSeconds(fireballCD);
        isFireballCD = false;
    }

    //-- Onda de empuje desde el mago --
    private IEnumerator ForceWave()
    {
        //Cooldown e Instanciación de la ForceWave
        isForcewaveCD = true;
        forcewave.GetComponent<ForceWaveController>().InstantiateWave(this.gameObject);
        yield return new WaitForSeconds(forcewaveCD);
        isForcewaveCD = false;
    }
}
