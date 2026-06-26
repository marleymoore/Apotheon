using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class PlayerApostles : MonoBehaviour
{


    [SerializeField] public List<Apostle> apostles;

    private void Start()
    {
        foreach(var Apostle in apostles)
        {
            Apostle.Init();
        }
    }

    public Apostle GetApostle()
    {
        return apostles.Where(x => x.CurrentHP > 0).FirstOrDefault();
    }
    // [SerializeField] ApostleBase playerApostle;
    // [SerializeField] Sprite frontSprite;
    // [SerializeField] Sprite backSprite;
    // public ApostleBase PApostle { get { return playerApostle; } }
    // // Start is called before the first frame update
    // public Sprite BackSprite { get { return backSprite; } }
    //
    // public Sprite FrontSprite { get { return frontSprite; } }

}
