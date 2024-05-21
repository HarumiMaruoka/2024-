using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gameObjects;

    public int GameObjectsCount => _gameObjects.Count;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _gameObjects.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _gameObjects.Remove(collision.gameObject);
    }
}