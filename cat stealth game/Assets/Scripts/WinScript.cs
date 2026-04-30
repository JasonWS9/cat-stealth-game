using Unity.VectorGraphics;
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

        SceneManager.LoadSceneAsync("WinScreen");
    }

}

