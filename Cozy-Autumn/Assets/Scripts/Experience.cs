using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour, IUpdate
{
    float xpPoints;
    bool initialized = false;
    Transform player;
    public float xpRange;
    [SerializeField] SpriteRenderer sr;

    public void OnUpdate()
    {
        if (player != null)
        {
            Vector3 dir = player.transform.position - transform.position;
            if (dir.magnitude <= xpRange)
            {
                transform.position += dir.normalized * Time.deltaTime * 10;
            }
        }
    }

    public void SetXpAmmount(float xp, Vector3 scaleSize, bool isBoss = false)
    {
        xpPoints = xp;
        transform.localScale = scaleSize;
        if (isBoss) sr.color = Color.cyan;
        else sr.color = Color.white;

        if (!initialized)
        {
            player = GameManager.Instance.player.transform;
            UpdateManager.Instance.AddUpdate(this);
            xpRange = GameManager.Instance.data.XpRange();
            initialized = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<PlayerController>().AddExperience(xpPoints);

            GameManager.Instance.objectPool.ReturnToPool(transform);
        }
    }
}
