using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public string targetTagToDisable = "Platform"; 
    public string targetTagToDestroy = "Waste";
    private bool isPlayerInTrigger = false; 

    void Update()
    {
        
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.Return))
        {
            ActivateSwitch();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    void ActivateSwitch()
    {
       
        GameObject[] objectsToDisable = GameObject.FindGameObjectsWithTag(targetTagToDisable);
        foreach (GameObject obj in objectsToDisable)
        {
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = false;
            }
        }

       
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(targetTagToDestroy);
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }
}
