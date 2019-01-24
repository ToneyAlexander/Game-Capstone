using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioFadeIn : MonoBehaviour
{
    [SerializeField]
    private float fadeTime;

    [SerializeField]
    private float maxVolume;

    private float startTime;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        if (fadeTime == 0)
        {
            fadeTime = 5;
        }
        if(maxVolume == 0)
        {
            maxVolume = .5f;
        }
        startTime = Time.time;
        //DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(source.volume < .9 * maxVolume)
        {
            float t = (Time.time - startTime)/fadeTime;
            source.volume = t * t * (3 - 2 * t) * maxVolume;
        }
        else
        {
            source.volume = maxVolume;
            Destroy(this);
        }
    }
}
