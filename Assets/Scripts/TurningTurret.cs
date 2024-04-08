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
    private void LookMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mousetransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        mousetransform.rotation = rotation;
    }
    void Start()
    {
        mousetransform = this.transform;
    }
    void Update()
    {
        LookMouse();
        //Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position,turret.range);
        //if (enemies != null)
        //{

        //}
    }
}
