using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : SingletonMonoBehavior<InGameManager>
{
    [SerializeField]
    GameObject _resultPanel;
    [SerializeField]
    GameObject _menuPanel;
    [SerializeField]
    BGMType _BGMType;
    bool _showMenu = false;
    // Start is called before the first frame update
    void Start()
    {
        _resultPanel.SetActive(false);
        _menuPanel.SetActive(false);
        AudioManager.Instance.PlayBGM(_BGMType);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _showMenu = !_showMenu;
            _menuPanel.SetActive(_showMenu);
            if (_showMenu) 
            { 
                Time.timeScale = 0f;
            }
            else Time.timeScale = 1f;
        }
           // if OnGoal();
    }
    public void OnGoal()
    {
        _resultPanel.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
