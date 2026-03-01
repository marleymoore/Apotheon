using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{

    
    //player movement
    public Rigidbody2D pBody;
    public float pMoveSpeed = 1f;
    Vector2 pDirection;
    public EncounterList currentEncounterList;
    //[SerializeField] bool encounterEnable = false;

    private float interval = 2f;

    //private bool isMoving = false;


    // Update is called once per frame
    void Update()
    {
        //player movement using input manager 
        PlayerMove();
     
    }


    void PlayerMove()
    {
        pDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        pBody.velocity = pDirection * pMoveSpeed;

       // isMoving = pBody.velocity.magnitude > 0 ? true : false;
       //
       // if(isMoving == true && encounterEnable == true)
       // {
       //     Encounter encounter = new Encounter(Random.Range(1, 101), 2f);
       //     EventBus.Raise(encounter);
       // }
    }

    /* BEGIN ENCOUNTER
     *
     * On first contact with "encounter" hitbox begin EncounterCheck coroutine.
     * 
     */
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Encounter"))
        {
            currentEncounterList = collision.GetComponent<EncounterList>();

            StopCoroutine(EncounterCheck());
           // Debug.Log("previous routine stopped");
            StartCoroutine(EncounterCheck());
        }
    }
    /* EXIT ENCOUNTER:
     * 
     * Upon leaving the encounter collider stop the encounter chance for player.
     * 
     */
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Encounter"))
        { 

            StopCoroutine(EncounterCheck());
            Debug.Log("Encounter chance stopped!");
            currentEncounterList = null;
        }
    }

    /*COROUTINE FOR ENCOUNTER
     * 
     * Raises encounter event to event bus for other classes to observe.
     * 
     */
    IEnumerator EncounterCheck()
    {
        yield return new WaitForSeconds(interval);

        Encounter encounter = new Encounter(Random.Range(1, 10), currentEncounterList);


        EventBus.Raise(encounter);
        
    }
}
