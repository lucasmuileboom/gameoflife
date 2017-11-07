using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Renderer _renderer;
    private bool alive = false;

    public bool Alive
    {
        get { return alive; }
        set
        {
            alive = value;
            if (alive)
            {
                _renderer.material.color = Color.red;
            }
            else if (!alive)
            {
                _renderer.material.color = Color.white;
            }
        }
    }
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
    public void ToggleAlive()
    {
        Alive = !Alive;
    }
}
