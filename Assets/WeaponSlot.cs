using UnityEngine;
using System.Collections;

public class WeaponSlot : MonoBehaviour {
    [SerializeField]
    private Player player;

    [SerializeField]
    private KeyCode dropWeaponKey = KeyCode.E;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(dropWeaponKey))
        {
            Gun gun = GetComponentInChildren<Gun>();

            if(gun != null)
            {
                gun.transform.parent = null;
                gun.DropGun();
            }
        }
	}


    public Player Player
    {
        get { return player; }
    }
}
