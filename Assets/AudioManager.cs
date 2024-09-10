using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("_____Audio Source_____")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("_____Audio Clip_____")]
    public AudioClip Background;
    public AudioClip death;
    public AudioClip shoot;
    public AudioClip breakwall;
    public AudioClip walk;
    public AudioClip Hit;
    public AudioClip zombiedeath;

    private void Start()
    {
        musicSource.clip = Background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
