using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] HpBar hpBar;

    Apostle Apostle;

    private void Awake()
    {
        EventBus.Subscribe<TakeDamage>(UpdateHPBar);
        EventBus.Subscribe<CleanUpEffects>(UpdateHPBar);
    }

    public void SetHudData(Apostle apostle)
    {
        Apostle = apostle;

        nameText.text = apostle.ApostleBase.Name;
        levelText.text = "Lvl " + apostle.Level;
        hpBar.SetHp((float)apostle.CurrentHP / apostle.MaxHp);
    }

    public void UpdateHPBar(EventData eventData)
    {
        hpBar.SetHp((float)Apostle.CurrentHP / Apostle.MaxHp);
    }

    // public void UpdateHPBar()
    // {
    //     hpBar.SetHp((float)_apostle.currentHP / _apostle.MaxHp);
    // }


}
