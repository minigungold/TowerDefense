using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Turret")]
public class ScriptableTurret : ScriptableObject
{
    public int id;
    public string name;
    public Sprite turretSprite;
    public float cost;

    public float attack;
    public float attackSpeed;
    public float range;
    public LineRenderer lineRenderer;
}
