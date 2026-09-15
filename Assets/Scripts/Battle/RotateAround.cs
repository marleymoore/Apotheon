using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject centreDial;
    [SerializeField] float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(centreDial.transform.position, new Vector3(0, 0, 1), Time.deltaTime * rotateSpeed);
    }
}
