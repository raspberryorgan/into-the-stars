using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableScythe : Bullet
{

    public float rotationSpeed = 5;

    public override void OnUpdate()
    {
        base.OnUpdate();

        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }

}
