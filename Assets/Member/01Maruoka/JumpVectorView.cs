using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class JumpVectorView : MonoBehaviour
{
    [SerializeField] private float _radius = 1.5f;
    [SerializeField] private FrogController _frogController;

    private void Update()
    {
        float x;
        float y;

        if (_frogController.JumpPower == Vector2.zero)
        {
            x = _frogController.transform.position.x;
            y = _frogController.transform.position.y;
        }
        else
        {
            var angle = Vector2.Angle(_frogController.JumpPower, Vector2.right);

            x = _frogController.transform.position.x + Mathf.Cos(Mathf.Deg2Rad * angle) * _radius;
            y = _frogController.transform.position.y + Mathf.Sin(Mathf.Deg2Rad * angle) * _radius;
        }

        transform.position = new Vector3(x, y, 0);
    }
}