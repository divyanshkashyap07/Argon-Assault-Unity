using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float leveLoadDelay = 1f;
    [SerializeField] GameObject deathFX;

    void OnTriggerEnter(Collider other)
    {
        //DeathMessage();
        //deathFX.SetActive(true);
        //Invoke("ReloadLevel", leveLoadDelay);
    }

    private void DeathMessage()
    {
        SendMessage("PlayerOnDeath");
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
