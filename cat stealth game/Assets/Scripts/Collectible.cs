using UnityEngine;

public class Collectible : MonoBehaviour
{

    public float rotationSpeed = 30f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        
    }

    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            Collected();
        }
    }

    void Collected()
    {
        AudioManager.instance.PlayPaperSound();
        Destroy(gameObject);
        GameManager.Instance.GainedCollectible();
    }
}
