using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

    [SerializeField]
    private float curHealth = 100, maxHealth = 100;

    [SerializeField]
    private HideGameObject hideGameObject;

    [SerializeField]
    private AudioClip zombieDie;

    [SerializeField]
    private ParticleSystem bloodSplatter;
    [SerializeField]
    private ParticleSystem deathSplatter;


    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void TakeDamage(float amount, Vector3 hitLocation, Quaternion hitRotation)
    {
        curHealth = Mathf.Clamp(curHealth - amount, 0, maxHealth);

        if(curHealth == 0)
        {
            hideGameObject.Hide();
            audioSource.PlayOneShot(zombieDie);
            Destroy(gameObject, 5);
            Instantiate(deathSplatter, new Vector3(hitLocation.x, 1, hitLocation.z), Quaternion.Euler(-90, 0, 0));
            Instantiate(deathSplatter, new Vector3(hitLocation.x, 0.8f, hitLocation.z), Quaternion.Euler(0, 0, 0));
            Instantiate(deathSplatter, new Vector3(hitLocation.x, 0.8f, hitLocation.z), Quaternion.Euler(0, 90, 0));
            Instantiate(deathSplatter, new Vector3(hitLocation.x, 0.8f, hitLocation.z), Quaternion.Euler(0, 180, 0));
            Instantiate(deathSplatter, new Vector3(hitLocation.x, 0.8f, hitLocation.z), Quaternion.Euler(0, 270, 0));
            
        }
        Instantiate(bloodSplatter, hitLocation, hitRotation);
        

        
    }


}
