using UnityEngine;

public class ButtonSoundController : MonoBehaviour
{
    public void PlaySE(AudioClip clip)
    {
        AudioManager.Instance.PlaySE(clip);
    }
}
