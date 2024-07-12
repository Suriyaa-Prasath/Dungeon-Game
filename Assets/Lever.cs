using UnityEngine;

public class Lever : MonoBehaviour
{
    public Sprite leverUpSprite; // Lever up sprite
    public Sprite leverDownSprite; // Lever down sprite
    public Door door; // Reference to the Door script
    private bool isPulled = false;
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
        spriteRenderer.sprite = leverUpSprite; // Set initial sprite to lever up
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Return) && !isPulled)
        {
            audioManager.PlaySFX(audioManager.lever);
            PullLever();
        }
    }

    void PullLever()
    {
        // Change the sprite to lever down
        spriteRenderer.sprite = leverDownSprite;
        isPulled = true;

        // Open the door
        if (door != null)
        {
            door.Open();
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
}
