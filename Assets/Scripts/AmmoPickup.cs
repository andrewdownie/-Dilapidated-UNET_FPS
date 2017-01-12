using UnityEngine;
using System.Collections;

public class AmmoPickup : MonoBehaviour {

    [SerializeField]
    private HideGameObject hideGameObject;


    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip pickupSound;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            Player player = coll.GetComponent<Player>();


            player.AddAmmoPack();
            Destroy(gameObject, pickupSound.length + 1f);

            audioSource.PlayOneShot(pickupSound);
            hideGameObject.Hide();

        }


    }


}
