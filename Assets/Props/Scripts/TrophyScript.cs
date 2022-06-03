using UnityEngine;

public class TrophyScript : MonoBehaviour
{
    private float _rotationSpeed = 50;
    private float _hoverAmplitude = 2;
    private float _hoverFrequency = 2;
    private float _trophyOffset = 5;
    private float _yPos;
    private Vector3 _newTrophyPosition;

    void Update()
    {
        //trophy rotation animation
        transform.Rotate(Vector3.up * (_rotationSpeed * Time.deltaTime));

        //trophy hover animation
        _newTrophyPosition = transform.position;
        _yPos = _hoverAmplitude * Mathf.Sin(Time.time * _hoverFrequency);
        _newTrophyPosition = new Vector3(_newTrophyPosition.x, _yPos + _trophyOffset, _newTrophyPosition.z);
        transform.position = _newTrophyPosition;
    }
}
