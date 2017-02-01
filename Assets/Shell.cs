using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

    [SerializeField]
    private Vector3 initialBulletForce = new Vector3(25, 25, 0);

    //[SerializeField]
    //private LayerMask layerMask;

    [SerializeField]
    private float destroyTimer = 4;


    private Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        

        rigid.AddForce( transform.rotation * (initialBulletForce * -1), ForceMode.Impulse);


        Destroy(this, destroyTimer);
    }

}
