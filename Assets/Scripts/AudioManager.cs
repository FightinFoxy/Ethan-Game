using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        if(clip != null)
            audioSource.PlayOneShot(clip);
    }
}
