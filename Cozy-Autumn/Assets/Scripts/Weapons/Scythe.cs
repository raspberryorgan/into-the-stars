using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : Weapon
{
    void Start()
    {
        
    }

    public override void Shoot(PlayerController player)
    {       
        StartCoroutine(rotatexd());
    }

    IEnumerator rotatexd()
    {
        for (int i = 0; i < 360; i++)
        {
            transform.Rotate(Vector3.forward);

            if( i == 120)
            {
                var enemies = Physics2D.CircleCastAll(transform.position, 2, Vector2.up,2,enemyMask);
                for (int j = 0; j < enemies.Length; j++)
                {
                    enemies[j].transform.GetComponent<Leaf>().TakeDamage(baseDamage);
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public override void AddNewBullet(PlayerController player)
    {

    }

    protected override void OnLevelUp(int level)
    {

    }
}
