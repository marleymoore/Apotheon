using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] GameObject health;

   
    public void SetHp(float hpNormalised)
    {
        health.transform.localScale = new Vector3(hpNormalised, 1f);
    }
}
