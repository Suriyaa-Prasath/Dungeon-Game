using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class GameManager : MonoBehaviour
    {
        public int coinsCounter = 0;
        public int totalCoins = 0;

        public GameObject playerGameObject;
        private PlayerController player;
        public GameObject deathPlayerPrefab;
        public Text coinText;

        AudioManager audioManager;

        private void Awake()
        {
            audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); // Find player by tag
            GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
            totalCoins = coins.Length;

            Debug.Log("Total coins in level: " + totalCoins);
        }

        private void Update()
        {
            coinText.text = $"{coinsCounter} / {totalCoins}";

            if (player.deathState)
            {
                Debug.Log("Player died!");
                audioManager.PlaySFX(audioManager.death);
                playerGameObject.SetActive(false);
                GameObject deathPlayer = Instantiate(deathPlayerPrefab, playerGameObject.transform.position, playerGameObject.transform.rotation);
                deathPlayer.transform.localScale = playerGameObject.transform.localScale;
                player.deathState = false;
                Invoke("ReloadLevel", 3);
            }
        }

        private void ReloadLevel()
        {
           
            Application.LoadLevel(Application.loadedLevel); 
        }

       
        public void IncrementCoinsCounter()
        {
            coinsCounter++;
            Debug.Log("Coin collected! Total coins: " + coinsCounter);
        }
    }
}
