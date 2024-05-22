using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    AudioClip _goalSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.PlaySE(_goalSound);
        InGameManager.Instance.OnGoal();
    }
}
