using System;
using UnityEngine;

public class JumpVectorView : MonoBehaviour
{
    [SerializeField] private float _radius = 1.5f;
    [SerializeField] private FrogController _frogController;

    private void Update()
    {
        if (_frogController.JumpPower == Vector2.zero)
        {
            transform.position = _frogController.transform.position;
        }
        else
        {
            var length = _frogController.JumpPower.magnitude;
            float angle = Vector2.Angle(_frogController.JumpPower, Vector2.right);

            float x = _frogController.transform.position.x + Mathf.Cos(Mathf.Deg2Rad * angle) * _radius * length * 0.06f;
            float y = _frogController.transform.position.y + Mathf.Sin(Mathf.Deg2Rad * angle) * _radius * length * 0.06f;

            transform.position = new Vector3(x, y, 0);
        }
    }
}