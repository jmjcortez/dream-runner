﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            AudioManager.instance.PlaySFX(2);
            
            StartCoroutine(LevelManager.instance.HandleGameOver(1));
        }
    }
}
