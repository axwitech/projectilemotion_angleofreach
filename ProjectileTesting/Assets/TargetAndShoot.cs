using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAndShoot : MonoBehaviour
{
    public Rigidbody projectile;
    public float projectileSpeed;
    public float firerate;
    private float nextfire;

    private GameObject enemy;
    private float gravity = Physics.gravity.y;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("enemy");

    }


    void Update()
    {
        if (Time.time >= nextfire)
        {
            nextfire = Time.time + (1 / firerate);
            float distance = enemy.transform.position.x - transform.position.x;
            Vector3 directionalVector = enemy.transform.position - transform.position;

            //float theta = 0.5f*Mathf.Asin((gravity * distance) / (projectileSpeed * projectileSpeed));
            float theta = Mathf.Atan(v2 - Mathf.Sqrt(v4 - gravity * (gravity * x2 + 2 * y * v2)) / gravity * x);
            //theta = Mathf.Rad2Deg * theta;

            Vector3 releaseVector = (Quaternion.AngleAxis(theta, Vector3.forward) * directionalVector).normalized;
            Debug.DrawRay(transform.position, releaseVector*5, Color.cyan, 0.5f);
           
            Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            instantiatedProjectile.velocity = releaseVector * projectileSpeed;
        }
    }

}
