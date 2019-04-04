using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioFadeOut : MonoBehaviour
{
    [SerializeField]
    private float fadeTime;

    [SerializeField]
    private float maxVolume;

    private float startTime;
    private AudioSource source;

    public bool start;

    private bool first = true;

    void Awake()
    {
        start = false;
        source = GetComponent<AudioSource>();
        if (fadeTime == 0)
        {
            fadeTime = 5;
        }
        if(maxVolume == 0)
        {
            maxVolume = .5f;
        }
        //DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (start)
        {
            if (first)
            {
                startTime = Time.time;
                first = false;
            }
            if (source.volume > .05f)
            {
                float t = (Time.time - startTime) / fadeTime;
                source.volume = (1 - t * t * (3 - 2 * t)) * maxVolume;
            }
            else
            {
                source.volume = 0;
                Destroy(this);
            }
        }
    }
}
