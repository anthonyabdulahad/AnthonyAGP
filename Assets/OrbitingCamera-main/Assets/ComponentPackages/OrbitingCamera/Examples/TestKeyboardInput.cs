using UnityEngine;

public class TestKeyboardInput : MonoBehaviour
{
    Orbit orbit;

    void Start()
    {
        orbit = GetComponent<Orbit>();
    }

    void Update()
    {
        orbit.Rotate(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
