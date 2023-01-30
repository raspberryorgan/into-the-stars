using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour, IUpdate
{
    float goldAmmount;

    bool initialized = false;
    Transform player;
    float collectionRange;
    public void OnUpdate()
    {
        if (player != null)
        {
            Vector3 dir = player.transform.position - transform.position;
            if (dir.magnitude <= collectionRange)
            {
                transform.position += dir.normalized * Time.deltaTime * 10;
            }
        }
    }
    public void SetGoldAmmount(float gold)
    {
        goldAmmount = gold;


        if (!initialized)
        {
            player = GameManager.Instance.player.transform;
            UpdateManager.Instance.AddUpdate(this);
            collectionRange = GameManager.Instance.data.XpRange();
            initialized = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<PlayerController>().AddGold(goldAmmount);

            GameManager.Instance.objectPool.ReturnToPool(transform);
        }
    }

}
