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

    //ELIMINAR ESTO DEL SCRIPT
    public GameObject spawnPoint;
    //HASTA AQUI

    private Animator WizardAnimator;
    private Vector3 owlPosition;
    private bool isAttacking = false;
    private NavMeshAgent agent; // El mago es un NavMeshAgent
    private Vector3 belowOwl; // Donde apunta el mago hacia el búho pero a la altura de enemigos
    private bool isFireballCD = false; // CD de la habilidad FireBall
    private bool isForcewaveCD = false; // CD de la habilidad ForceWave
    private float fireballCD = 1f; // Delay de CD de la habilidad FireBall
    private float forcewaveCD = 1f; // Delay de CD de la habilidad ForceWave
    private float fireballSpeed = 15f; // Velocidad de la FireBall
    private int totalLife = 6;
    private int actualLife;
    private int actualMana;
    public PotionSwitcher potionSwitcher;

    private void Awake()
    {
        Instance = this;
    }

    private void Start ()
    {
        //Inicialización de componentes
        actualLife = totalLife;
        actualMana = 5;
        potionSwitcher.activatePotion(actualMana);
        agent = GetComponent<NavMeshAgent>();
        WizardAnimator = GetComponent<Animator>();
        belowOwl = Vector3.zero;
	}

    public void addMana()
    {
        if (actualMana<5)
        potionSwitcher.activatePotion(actualMana++);
    }
	
	private void Update ()
    {
        //Actualiza posición del búho pero a la altura del bastón del mago
        belowOwl = new Vector3(owl.transform.position.x, spawnProjectile.transform.position.y, owl.transform.position.z);

        //LLAMADAS A ACCIONES QUE HAY QUE CAMBIAR PORQUE VIENEN DE FUERA
        // if (Input.GetMouseButtonDown(1))
        // {
        //    // Attack(1, belowOwl);
        // }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack(2, belowOwl);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            spawnPoint.GetComponent<SpawnController>().SpawnEnemies(spawnPoint.transform.position, 5);
        }
        //BORRAR HASTA AQUÍ
        if (agent.velocity.magnitude > 0.6)
            WizardAnimator.SetBool("IsWalking",true);
        else 
            WizardAnimator.SetBool("IsWalking",false);
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
                if (!isForcewaveCD && GetMana() == 5)
                    StartCoroutine(ForceWave());
                break;
        }
    }

    //-- Dispara animación FireBall --
    public void TriggerAttackAnimation(Vector3 destiny)
    {
            //LookAt del mago y del bastón hacia la posición de ataque
            owlPosition = destiny;
            transform.LookAt(belowOwl);
            WizardAnimator.SetTrigger("AttackProyectile");
            agent.isStopped = true;
            Debug.Log(destiny);
            isAttacking = true;
            
    }
    public void StopAttack()
    {
        isAttacking = false;
    }

    //-- Evento animación FireBall --
    public void AttackProjectileEvent()
    {
        Attack(1, belowOwl);
    }

    //-- Quitar vida al mago --
    public int takeDamage(int Damage)
    {
        actualLife -= Damage;

        UIManager.Instance.ActualiceLife(actualLife);

        if (actualLife == 0)
            Debug.Log("ha muerto");

        return actualLife;
    }

    //-- Movimiento del mago --
    public void MoveTo(Vector3 destiny)
    {

        //Setea posición de destino del NavMeshAgent
        if (!isAttacking){
            agent.isStopped = false;
            agent.SetDestination(destiny);
        }
        
    }
    
    //-- Proyectil de bola de fuego --
    private IEnumerator FireProjectile(Vector3 destiny)
    {
        //LookAt del mago y del bastón hacia la posición de ataque
        //this.transform.LookAt(belowOwl);
        spawnProjectile.transform.LookAt(belowOwl);
        //Cooldown e Instanciación de la FireBall
        isFireballCD = true;
        fireball.GetComponent<FireBallController>().InstantiateProjectile(spawnProjectile, owlPosition, fireballSpeed);
        yield return new WaitForSeconds(fireballCD);
        isFireballCD = false;
        potionSwitcher.activatePotion(actualMana);

    }

    //-- Onda de empuje desde el mago --
    private IEnumerator ForceWave()
    {
        //Cooldown e Instanciación de la ForceWave
        isForcewaveCD = true;
        actualMana = 0;
        potionSwitcher.activatePotion(actualMana);
        //UIManager.Instance.UseAllMana();
        forcewave.GetComponent<ForceWaveController>().InstantiateWave(this.gameObject);
        yield return new WaitForSeconds(forcewaveCD);
        isForcewaveCD = false;
    }

    //-- Get del mana --
    public int GetMana()
    {
        return actualMana;
    }

    //-- Metodo de recoger objetos --
}
