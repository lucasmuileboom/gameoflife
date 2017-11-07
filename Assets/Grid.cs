using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int X;
    [SerializeField] private int Y;
    [SerializeField] private GameObject cube;
    private List<Cell> changedcells;
    private Cell[,] cells;
    private int alifecount;
    private int count = 0;
    private Ray ray;
    private RaycastHit hit;

    private void Start()
    {
        changedcells = new List<Cell>();
        cells = new Cell[X, Y];
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                cells[i, j] = Instantiate(cube, new Vector3(i, j, 0), transform.rotation).GetComponent<Cell>();
            }
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                cells[(int)hit.transform.position.x, (int)hit.transform.position.y].ToggleAlive();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            game();
        }
    }
    private void game()
    {
        changedcells.Clear();
        for (int i = 1; i < Y - 1; i++)
        {
            for (int j = 1; j < X - 1; j++)
            {
                for (int n = -1; n < 2; n++)
                {
                    for (int m = -1; m < 2; m++)
                    {
                        count++;
                        if (cells[(int)i + n, (int)j + m].Alive && (n != 0 || m != 0))//kijken hoeveel er leven
                        {
                            alifecount++;
                        }
                        if (count == 9)//check voor verandering
                        {
                            if ((cells[i, j].Alive && alifecount == 2 )||alifecount == 3)
                            {
                                if (!cells[i, j].Alive)
                                {
                                    changedcells.Add(cells[i, j]);
                                }
                            }
                            else
                            {
                                if (cells[i, j].Alive)
                                {
                                    changedcells.Add(cells[i, j]);
                                }
                            }
                            count = 0;
                            alifecount = 0;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < changedcells.Count; i++)//verander cells
        {
            changedcells[i].ToggleAlive();
        }
    }
}
