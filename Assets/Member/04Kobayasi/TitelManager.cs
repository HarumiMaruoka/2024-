using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitelManager : MonoBehaviour
{
    [SerializeField]
    GameObject _startPanel;
    [SerializeField]
    GameObject _selectPanel;
    // Start is called before the first frame update
    void Start()
    {
        _startPanel.SetActive(true);
        _selectPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            _startPanel.SetActive(true);
            selectScenesPanel.SetActive(false);
        }
    }
}
