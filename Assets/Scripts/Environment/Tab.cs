using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tab : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.CompareTag("Player")) {
            AudioManager.instance.PlaySFX(1);

            LevelManager.instance.doubleJumpsAvailable += 1;
            LevelManager.instance.UpdateDoubleJumpText();
        
            Destroy(gameObject);
        }
    }
}
