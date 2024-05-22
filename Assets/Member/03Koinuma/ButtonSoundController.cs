using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundController : MonoBehaviour
{
    [SerializeField] private AudioClip _se;
    [SerializeField] private float _seVolume;
    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.PlaySE(_se, _seVolume));
    }
}
