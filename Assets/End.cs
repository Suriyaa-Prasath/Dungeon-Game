using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public Sprite doorClosedSprite;
    public Sprite doorOpenSprite;
    private bool isOpen = false;
    private bool isPlayerNear = false;

    private SpriteRenderer spriteRenderer;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = doorClosedSprite;
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Return) && isOpen)
        {
            audioManager.PlaySFX(audioManager.door);
            //UnlockNewLevel();
            LoadNextLevel();

        }
    }

    public void Open()
    {
        if (!isOpen)
        {

            spriteRenderer.sprite = doorOpenSprite;
            isOpen = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;


        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

        }
    }

    void LoadNextLevel()
    {


        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

   // void UnlockNewLevel()
    //{
   //     if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
      //  {
        //    PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
          //  PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            //PlayerPrefs.Save();
        //}
   // }
}
