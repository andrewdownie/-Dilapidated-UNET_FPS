using UnityEngine;
using System.Collections;

public class WeaponSlot : MonoBehaviour {
    [SerializeField]
    private Player player;

    [SerializeField]
    private KeyCode dropWeaponKey = KeyCode.E;

    [SerializeField]
    private Gun currentlyEquippedGun;

	// Use this for initialization
	void Start () {
        currentlyEquippedGun = GetComponentInChildren<Gun>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(dropWeaponKey))
        {
            if(currentlyEquippedGun != null)
            {
                currentlyEquippedGun.transform.parent = null;
                currentlyEquippedGun.DropGun();
            }
        }
	}


    public Player Player
    {
        get { return player; }
    }
}
