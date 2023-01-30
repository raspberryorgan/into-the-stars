using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagedText : MonoBehaviour
{
    public TMP_Text text;
    public Canvas canvas;
        

    public void SetDamage(float damage)
    {
        if (canvas == null) canvas = FindObjectOfType<Canvas>();
        text.transform.SetParent(canvas.transform);
        text.text = "-"  + damage.ToString("00.0");

        StartCoroutine(move());
    }

    public IEnumerator move()
    {

        for (int i = 0; i < 50; i++)
        {
            transform.position += Vector3.up * Time.deltaTime*10;
            yield return new WaitForSecondsRealtime(0.01f);
        }

        GameManager.Instance.objectPool.ReturnToPool(this);
    }
   
}
