using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Turret")]
public class ScriptableTurret : ScriptableObject
{
    public int id;
    public string name;
    public Sprite turretSprite;

    public int attack;
    public int attackSpeed;
    public int range;
    //public LineRenderer lineRenderer;
}
