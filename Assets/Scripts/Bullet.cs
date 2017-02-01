using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private float initialBulletForce = 500;
    [SerializeField]
    private Vector2 forceMultiplierRange = new Vector2(0.5f, 1.5f);
    [SerializeField][Range(0, float.MaxValue)]
    private float accuracyModifier = 0.5f;

   // [SerializeField]
    //private LayerMask layerMask;

    [SerializeField]
    private float bulletDamageAmount = 36;

    bool beingDestroyed;


    private HitMarkerCallback hitMarkerCallback;

    private Rigidbody rigid;
    
	void Start () {
        rigid = GetComponent<Rigidbody>();

        Vector3 force;
        force = transform.forward * initialBulletForce * Random.Range(forceMultiplierRange.x, forceMultiplierRange.y);

        float maxAccuracy = 1 - accuracyModifier;
        float minAccuracy = maxAccuracy / 10;

        force += transform.right * Random.Range(0, maxAccuracy) * RandPosNeg();
        force += transform.up * Random.Range(0, maxAccuracy) * RandPosNeg();


        rigid.AddForce(force, ForceMode.Impulse);
        

        Destroy(this, 4);
    }

    float RandPosNeg()
    {
        int rand = Random.Range(0, 2);

        if(rand == 0)
        {
            return -1;
        }
        return 1;
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
