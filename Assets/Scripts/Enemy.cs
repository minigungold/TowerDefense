using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject visual;
    public static HashSet<Enemy> allEnemies = new HashSet<Enemy>();
    private Stack<GameTile> path = new Stack<GameTile>();
    public float hp = 20;
    public float attack;
    public bool isDead;
    public float goldLoot;
    private Player player;

    public static event Action onDeath;
    private GameManager GM;


    private void Awake()
    {
        allEnemies.Add(this);
    }

    internal void SetPath(List<GameTile> pathToGoal)
    {
        path.Clear();
        foreach (GameTile tile in pathToGoal)
        {
            path.Push(tile);
        }
    }

    void Update()
    {
        if (path.Count > 0)
        {
            isDead = false;
            Vector3 destPos = path.Peek().transform.position;
            transform.position = Vector3.MoveTowards(transform.position, destPos, 2 * Time.deltaTime);

            if (Vector3.Distance(transform.position, destPos) < 0.01f)
            {
                path.Pop();
            }
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        onDeath?.Invoke();
        Debug.Log("AHHAHAHAHHHH JE MEURT");
        allEnemies.Remove(this);
        Destroy(gameObject);
    }

    internal void Attack(float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Die();
        }
        else
        {
            visual.transform.localScale *= 0.9f;
        }
    }
}