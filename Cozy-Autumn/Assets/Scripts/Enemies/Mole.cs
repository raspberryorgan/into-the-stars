using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Enemy
{
    moleStates currentState = moleStates.moving;

    Vector3 targetPos;

    float timer = 0;
    float timerDmg = 0;
    public float damageTime = 3f;
    public float damageRange = 1f;

    public ParticleSystem dealDamageParts;
    public void SetStateMove()
    {
        currentState = moleStates.moving;
        targetPos = target.position + Vector3.right * Random.Range(-5f, 5f) + Vector3.up * Random.Range(-5f, 5f);
        anim.Play("MoleMove");
    }
    public void SetStateGetIn()
    {
        currentState = moleStates.gettingIn;
        anim.Play("MoleIn");
    }
    public void SetStateGetOut()
    {
        currentState = moleStates.gettingOut;
        anim.Play("MoleOut");
    }
    public void SetStateIdle()
    {
        currentState = moleStates.damaging;
        anim.Play("MoleIdle");
    }
    public override void Move()
    {
        if(currentState == moleStates.moving)
        {
            Vector3 dir = (targetPos - transform.position);
            transform.position += dir.normalized * speed * Time.deltaTime;

            if(dir.magnitude <= 0.1f){
                SetStateGetOut();
            }
        }
        else if(currentState == moleStates.damaging)
        {
            timer += Time.deltaTime;
            if(timer >= damageTime)
            {
                timer = 0;
                SetStateGetIn();
            }

            timerDmg += Time.deltaTime;
            if(timerDmg >= 0.5f)
            {
                dealDamageParts.Play();
                timerDmg = 0;

                if ((target.position - transform.position).magnitude <= damageRange) 
                {
                    GameManager.Instance.player.TakeDamage(damage);                
                }
            }
        }
    }
}

public enum moleStates
{
    gettingOut,
    damaging,
    gettingIn,
    moving
}
