using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject _resultPanel;
    [SerializeField]
    GameObject _menuPanel;
    bool _showMenu = false;
    // Start is called before the first frame update
    void Start()
    {
        _resultPanel.SetActive(false);
        _menuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _showMenu = !_showMenu;
            _menuPanel.SetActive(_showMenu);
        }
           // if OnGoal();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnGoal();
    }
    void OnGoal()
    {
        _resultPanel.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
