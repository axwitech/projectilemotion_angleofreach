

I've been playing around with Unity and trying to simulate projectile motion.

This post will answer to the simple question: at what angle do I need to shoot from point A so that my projectile lands in a point B.

This example does not take in account air resistance or height from which the projectile was shot or what height it needs to land to.

For the the impatient ones, here is the code and how it looks in practice (it's all done in Update):

[projectile_motion_gif](https://imgur.com/a/3Wd7jIx)
```csharp
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


            float theta = 0.5f*Mathf.Asin((gravity * distance) / (projectileSpeed * projectileSpeed));
            Vector3 releaseVector = (Quaternion.AngleAxis(theta * Mathf.Rad2Deg, -Vector3.forward) * directionalVector).normalized;
            Debug.DrawRay(transform.position, releaseVector*5, Color.cyan, 0.5f);
           
            Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            instantiatedProjectile.velocity = releaseVector * projectileSpeed;
        }
    }

}
```
Info taken:
https://en.wikipedia.org/wiki/Projectile_motion#Angle_of_reach


Lets review the Update function line by line

```csharp
        if (Time.time >= nextfire)
        {
            nextfire = Time.time + (1 / firerate);
```

This is to make sure that there is some sense to how fast the projectile is shot, not mandatory but be ready to get spammed
```csharp
float distance = enemy.transform.position.x - transform.position.x;
```
the distance to the target. "d" in the angle of reach formula

```csharp
Vector3 directionalVector = enemy.transform.position - transform.position;
```

a vector that points to a direction of a target, used later and explained later

```csharp
float theta = 0.5f*Mathf.Asin((gravity * distance) / (projectileSpeed * projectileSpeed));
```
the Angle of reach formula itself that gives us the theta (angle at which the projectile needs to be launched)

Gravity:
```csharp
private float gravity = Physics.gravity.y;
```

Velocity:
```csharp
public float projectileSpeed;
```

```csharp
Vector3 releaseVector = (Quaternion.AngleAxis(theta * Mathf.Rad2Deg, -Vector3.forward) * directionalVector).normalized;
```

Now that we have the angle we need to "apply" (very crudely put) that angle to a directional vector. This is done by using Quaternion.AngleAxis function. Please note the minus -Vector3.forward. This is due to unity using left hand physics rule instead of the regular right hand.

```csharp
Debug.DrawRay(transform.position, releaseVector*5, Color.cyan, 0.5f);
```

Debugging to make sure that the line is pointing towards the correct 

```csharp
Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            instantiatedProjectile.velocity = releaseVector * projectileSpeed;
```

Final step. We instantiate the prefab and give it a release vector times the speed that we want to launch it. 
