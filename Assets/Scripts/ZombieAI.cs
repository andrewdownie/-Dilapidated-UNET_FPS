using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour {
    [SerializeField]
    private ZombieAIState aiState = ZombieAIState.idle;

    [SerializeField]
    private LayerMask playerLayerMask;

    [SerializeField]
    private float castRadius = 7;

    [SerializeField]
    private float moveSpeed = 4;

    [SerializeField]
    private float attackSpeed = 1;
    [SerializeField]
    private float attackDamage = -10;


    [SerializeField]
    private AudioClip[] zombieAttackSounds;

    [SerializeField]
    private Combat target;


    private bool readyToAttack;


    private Rigidbody rigid;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {

        rigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        readyToAttack = true;
	}
	

    void Update()
    {


        if(aiState == ZombieAIState.idle || aiState == ZombieAIState.wandering)
        {
            RaycastHit hitInfo;

            Physics.SphereCast(transform.position, castRadius, transform.forward, out hitInfo, castRadius, playerLayerMask);

            if(hitInfo.transform != null)
            {
                target = hitInfo.transform.gameObject.GetComponent<Combat>();
                aiState = ZombieAIState.chasing;
            }
        }



        if(aiState == ZombieAIState.chasing)
        {
            rigid.MovePosition(Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime));

            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToTarget > castRadius * 1.4f)
            {
                aiState = ZombieAIState.idle;
            }
            else if(distanceToTarget < 2 && readyToAttack)
            {
                StartCoroutine(Attack());
            }
        }
        
    }

    void PlayAttackSound()
    {
        int rnd = Random.Range(0, zombieAttackSounds.Length);
        audioSource.PlayOneShot(zombieAttackSounds[rnd]);
    }

    IEnumerator Attack()
    {
        target.ChangeHealth(attackDamage);
        PlayAttackSound();
        readyToAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        readyToAttack = true;
    }
}

public enum ZombieAIState
{
    idle,
    wandering,
    chasing,
    dying,
}