using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHoming : PowerUp
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void PickUp(GameObject owner)
    {
        // owner.activateHoming
        // quando il player sparer� finch� activateHoming � true
        // passa al bullet il fatto che deve essere homing
    }

    public void Homing()
    {
        // il bullet si fisser� allora come target il nemico pi� vicino
        // entro un raggio ragionevole
        // e nell'update
        // Vector3 distance = target.position - bullet.position
        // calcoliamo il vettore dal bullet al nemico target ogni frame 
        // e poi lo rotiamo
        // Quaternion rotationDir = Quaternion.LookRotation(distance)
        // Quaternion newRotation = Quaternion.Lerp(bullet.rotation, rotationDir, rotSpeed * Time.deltaTime);
        // Quaternion.RotateTowards(bullet.rotation, rotationDir, rotSpeed * Time.deltaTime)
    }
}
