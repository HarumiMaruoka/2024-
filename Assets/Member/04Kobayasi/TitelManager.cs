using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitelManager : MonoBehaviour
{
    [SerializeField]
    GameObject _startPanel;
    [SerializeField]
    GameObject _selectPanel;
    [SerializeField]
    BGMType _BGMType;
    // Start is called before the first frame update
    void Start()
    {
        _startPanel.SetActive(true);
        _selectPanel.SetActive(false);
        AudioManager.Instance.PlayBGM(_BGMType);
    }
    public void SelectScene(GameObject selectScenesPanel)
    {
        if (_selectPanel == selectScenesPanel)
        {
            _startPanel.SetActive(false );
            selectScenesPanel.SetActive(true);
        }
        else
        {
            selectScenesPanel.SetActive(true);
            _selectPanel.SetActive(false);
        }
    }
}
