using UnityEngine;
using System.Collections.Generic;

public class HealthPack : MonoBehaviour {

    [SerializeField]
    private HideGameObject hideGameObject;


    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip pickupSound;

    void OnTriggerEnter(Collider coll)
    {
        
        /// TODO: this needs to be changed to add to the players health pack slot if the slot it empty.
        if(coll.tag == "Player")
        {
            Combat combat = coll.GetComponent<Combat>();


            if (combat.AddHealthPack())
            {
                Destroy(gameObject, pickupSound.length + 1f);

                audioSource.PlayOneShot(pickupSound);

                hideGameObject.Hide();
            }
            
        }


    }
}
