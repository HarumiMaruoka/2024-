using System;
using UnityEngine;

public class SEPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip _se1;
    [SerializeField]
    private AudioClip _se2;
    [SerializeField]
    private AudioClip _se3;
    [SerializeField]
    private AudioClip _se4;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.Instance.PlaySE(_se1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            AudioManager.Instance.PlaySE(_se2);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            AudioManager.Instance.PlaySE(_se3);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            AudioManager.Instance.PlaySE(_se4);
        }
    }
}