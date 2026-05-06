using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource sfxSource;
    public AudioClip[] catSounds; // Drop your audio files here in the Inspector

    public AudioClip paperSound;
    public AudioClip honkSound;
    public static AudioManager instance;

    void Awake()
    {
        instance = this;
        sfxSource = GetComponent<AudioSource>();
    }


    public void PlayMeowSound()
    {
        if (catSounds.Length == 0) return;

        int randomIndex = Random.Range(0, catSounds.Length);

        sfxSource.PlayOneShot(catSounds[randomIndex]);
    }

    public void PlayPaperSound()
    {
        sfxSource.PlayOneShot(paperSound);
    }

    public void PlayHonkSound()
    {
        sfxSource.PlayOneShot(honkSound);
    }
}
