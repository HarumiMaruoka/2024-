using UnityEngine;

public class AudioManager : SingletonMonoBehavior<AudioManager>
{
    [SerializeField] private AudioSource _bgmAudioSource;
    [SerializeField] private AudioSource _seAudioSource;
    [SerializeField] private AudioClip _titleBGM;
    [SerializeField] private AudioClip[] _stageBGM;

    protected override void OnAwake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBGM(BGMType bgmType)
    {
        if (bgmType == BGMType.Title) _bgmAudioSource.clip = _titleBGM;
        else _bgmAudioSource.clip = _stageBGM[(int)bgmType - 1];
        
        _bgmAudioSource.Play();
    }

    public void PlaySE(AudioClip se)
    {
        _seAudioSource.PlayOneShot(se);
    }
}

public enum BGMType
{
    Title,
    StageBGM1,
    StageBGM2,
    StageBGM3
}
