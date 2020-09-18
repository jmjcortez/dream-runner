using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraController : MonoBehaviour
{
    public GameObject buildings;

    void Update()
    {
        transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z); 

        float buildingModifier;

        if (transform.position.x - buildings.transform.position.x > 25) {
            buildingModifier = 40f;
        } else {
            buildingModifier = 0.05f;
        }
        
        buildings.transform.position = new Vector3(buildings.transform.position.x + buildingModifier, buildings.transform.position.y, buildings.transform.position.z);

    }
}
