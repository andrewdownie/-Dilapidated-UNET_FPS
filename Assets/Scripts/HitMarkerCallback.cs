using UnityEngine;
using System.Collections;

public class HitMarkerCallback : MonoBehaviour {

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip hitMarkerSound;


	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
    public void ConfirmHit()
    {
        audioSource.PlayOneShot(hitMarkerSound);
        Debug.Log("Hit marker confimed");
        //show hit marker gui here
    }
}
