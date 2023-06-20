using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Weapon";
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent == null)
        {
            transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
        }
    }
}
