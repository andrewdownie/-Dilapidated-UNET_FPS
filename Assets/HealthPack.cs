using UnityEngine;
using System.Collections.Generic;

public class HealthPack : MonoBehaviour {
    [SerializeField]
    private float healAmount = 80f;


    [SerializeField]
    private Renderer[] renderers;

    [SerializeField]
    private BoxCollider[] boxColliders;


    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip healSound;

    void OnTriggerEnter(Collider coll)
    {
        
        /// TODO: this needs to be changed to add to the players health pack slot if the slot it empty.
        if(coll.tag == "Player")
        {
            I_Health health = (I_Health)coll.GetComponent<Combat>();
            health.Heal(healAmount);
            Destroy(gameObject, healSound.length + 1f);

            audioSource.PlayOneShot(healSound);

            foreach(Renderer r in renderers)
            {
                r.enabled = false;
            }

            foreach(BoxCollider bc in boxColliders)
            {
                bc.enabled = false;
            }
        }


    }
}
