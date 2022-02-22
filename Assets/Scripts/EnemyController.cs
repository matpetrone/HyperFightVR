using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public HealthBar_Enemy healthbar;
    public Dissolver dissolver;
    private Animator animator;
    private AudioSource hitAudio;

    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 3;
    public float timeBeetweenAttacks;
    public float sightRange = 80f;
    public float dissolveSpeed = 2f;

    private string playerWeapon = "PlayerKatana";
    private float attackRange;
    private int runningAnimation;
    private int standingAnimation;
    private int searchingAnimation;
    private int deadAnimation;
    private int hitAnimation;
    private int attack1Animation;
    private int attack2Animation;
    private int attack3Animation;
    private int noted;
    private int victoryAnimation;
    private bool alreadyNoted;
    private bool alreadyAttacked;
    private bool gameAlive;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        hitAudio = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
        attackRange = agent.stoppingDistance;
        alreadyNoted = false;
        gameAlive = true;

        runningAnimation = Animator.StringToHash("running");
        standingAnimation = Animator.StringToHash("standing");
        searchingAnimation = Animator.StringToHash("waiting");
        deadAnimation = Animator.StringToHash("dead");
        hitAnimation = Animator.StringToHash("hit");
        attack1Animation = Animator.StringToHash("attack1");
        attack2Animation = Animator.StringToHash("attack2");
        attack3Animation = Animator.StringToHash("attack3");
        noted = Animator.StringToHash("noted");
        victoryAnimation = Animator.StringToHash("gameover");
    }

    // Update is called once per frame
    void Update()
    {
        //Navigation
        float distance = Vector3.Distance(target.position, transform.position);

        if ((distance > sightRange) && gameAlive)
        {
            float id = UnityEngine.Random.Range(0f, 10f);
            animator.SetFloat(searchingAnimation, id);
        }

        if ((distance <= sightRange) && gameAlive)
        {
            if (!alreadyNoted)
            {
                animator.SetTrigger(noted);
                alreadyNoted = true;           
            }

            agent.SetDestination(target.position);
            FaceTarget();
            animator.SetBool(runningAnimation, true);

            if (distance <= attackRange - 2f)
            {
                animator.SetBool(standingAnimation, true);
                AttackPlayer();
            }
            else
                animator.SetBool(standingAnimation, false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == playerWeapon)
        {
            animator.SetTrigger(hitAnimation);
            hitAudio.Play();
            TakeDamage();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, agent.stoppingDistance);
    }


    public void TakeDamage()
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }

    public void AttackPlayer()
    {
        if (!alreadyAttacked)
        {
            float attackType = UnityEngine.Random.Range(0f, 10f);

            if (attackType <= 3.3f)
                animator.SetTrigger(attack2Animation);
            
            else if (3.3f < attackType && 6.6f >= attackType)
                animator.SetTrigger(attack1Animation);
            
            else
                animator.SetTrigger(attack3Animation);
            alreadyAttacked = true;
            StartCoroutine("ResetAttack");
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(timeBeetweenAttacks);
        alreadyAttacked = false;
    }

    public void Dead()
    {
        
        animator.SetBool(deadAnimation, true);
        agent.isStopped = true;
        gameAlive = false;
        dissolver.Dissolve();
    }

    public void Won()
    {
        animator.SetBool(victoryAnimation, true);
        agent.isStopped = true;
        gameAlive = false;

    }

    
}
