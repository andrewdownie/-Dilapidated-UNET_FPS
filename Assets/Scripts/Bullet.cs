using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private float initialBulletForce = 500;
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float bulletDamageAmount = 36;

    bool beingDestroyed;


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

            RaycastHit hit;
            Physics.Raycast(transform.position + transform.forward * -2, transform.forward, out hit);

            Zombie zombie = collision.gameObject.GetComponent<Zombie>();
            zombie.TakeDamage(bulletDamageAmount, hit.point, transform.position);

            hitMarkerCallback.ConfirmHit();
        }
    }

    public void SetHitMarkerCallBack(HitMarkerCallback callback)
    {
        hitMarkerCallback = callback;
    }


    void FixedUpdate()
    {
        if(transform.position.y < 1 && !beingDestroyed)
        {
            beingDestroyed = true;
            Destroy(gameObject, 120);
        }
    }
}
