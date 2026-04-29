using UnityEngine;
using UnityEngine.SceneManagement; // For scene reload or next level
using UnityEngine.UI; // For optional UI message

public class WinScript : MonoBehaviour
{
    public int totalItems = 0;
    private int collectedItems = 0;
    public Text winText;


    void Start()
    {
        if (totalItems == 0)
        {
            totalItems = GameObject.FindGameObjectsWithTag("Collectible").Length;

        }

        if (winText != null)
        {
            winText.gameObject.SetActive(false);
        }

    }
    public void CollectItem()
    {
        collectedItems++;

        if (collectedItems >= totalItems)
        {
            TriggerWin();
        }
    }

    private void TriggerWin()
    {
        Debug.Log("You Win!");

        if (winText != null)
        {
            winText.text = "You Win!";
            winText.gameObject.SetActive(true);
        }

    }
}
