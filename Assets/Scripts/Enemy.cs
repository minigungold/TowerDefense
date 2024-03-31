using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Stack<GameTile> path = new Stack<GameTile>();

    internal void SetPath(List<GameTile> pathToGoal)
    {
        path.Clear();
        foreach(GameTile tile in pathToGoal)
        {
            path.Push(tile);
        }
    }

    void Update()
    {
        if(path.Count > 0)
        {
            Vector3 destPos = path.Peek().transform.position;
            transform.position = Vector3.MoveTowards(transform.position,destPos, 2 * Time.deltaTime);

            if (Vector3.Distance(transform.position, destPos) < 0.01f)
            {
                path.Pop();
            }
        }
        else
        {
            Destroy(path.Pop());
        }
    }
}