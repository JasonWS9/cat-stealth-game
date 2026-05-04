using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For optional UI message

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int collectiblesFound;
    public int totalCollectibles;
    public TextMeshProUGUI winText;

    private void Start()
    {
        if (winText != null)
        {
            winText.gameObject.SetActive(false); 
            Time.timeScale = 1;
        }

    }
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainedCollectible()
    {
        collectiblesFound ++;
        if (collectiblesFound >= totalCollectibles)
        {
            Debug.Log("all collectibles found");
            TriggerWin();
        }
    }
    private void TriggerWin()
    {
        Debug.Log("You Win!");
        LoadScene("Ending 2 (Win)");
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game");
        Application.Quit();
    }
}
