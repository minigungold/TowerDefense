using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurningTurret : MonoBehaviour
{
    public int id;
    private Transform mousetransform;
    public ScriptableTurret turret;
    private GameTile tile;
    private float range;
    private Player player;

    void Start()
    {
        mousetransform = this.transform;
    }
    void Update()
    {
        if (SelectedItem.Instance.selectItem != null)
        {
            range = SelectedItem.Instance.selectItem.range;
        }
        if (Time.timeScale > 0.1f)
        {

            foreach (var enemy in Enemy.allEnemies)
            {

                if (Vector3.Distance(mousetransform.position, enemy.transform.position) < range)
                {
                    Vector2 direction = enemy.transform.position - mousetransform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    mousetransform.rotation = rotation;
                    break;
                }
            }
        }
        //LookMouse();
        //Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, turret.range);
        //if (enemies != null)
        //{

        //}
    }

}
