using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

    [SerializeField]
    private float initialBulletForce = 500;
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float bulletDamageAmount = 36;


    private HitMarkerCallback hitMarkerCallback;

    private Rigidbody rigid;
    
	void Start () {
        rigid = GetComponent<Rigidbody>();

        rigid.AddForce(transform.forward * initialBulletForce, ForceMode.Impulse);

        Destroy(this, 4);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Zombie")
        {
            Zombie zombie = collision.gameObject.GetComponent<Zombie>();
            zombie.TakeDamage(bulletDamageAmount, collision.transform.position, Quaternion.Inverse(collision.transform.rotation));

            hitMarkerCallback.ConfirmHit();
        }
    }

    public void SetHitMarkerCallBack(HitMarkerCallback callback)
    {
        hitMarkerCallback = callback;
    }


    void FixedUpdate()
    {
        if(transform.position.y < 1)
        {
            Destroy(gameObject, 120);
        }
    }
}
