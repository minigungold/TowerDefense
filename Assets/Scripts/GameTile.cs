using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] SpriteRenderer hoverRenderer;
    [SerializeField] private List<TurningTurret> turrets;
    [SerializeField] private SpriteRenderer spawnRenderer, turretRenderer, flagRenderer;
    public SpriteRenderer wallRenderer;
    [SerializeField] private Color color;
    [SerializeField] private LineRenderer lineRenderer;

    private TurningTurret turret;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool canAttack = true;
    private float range;
    private float atk;
    private float atkSpeed;

    private Vector3 moveDirection;

    public GameManager GM { get; internal set; }
    public int X { get; internal set; }
    public int Y { get; internal set; }
    public bool IsBlocked { get; set; }
    public bool isAPath = false;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.SetPosition(0, transform.position);

        spriteRenderer = GetComponent<SpriteRenderer>();
        turretRenderer.enabled = false;
        //turretRenderers
        originalColor = spriteRenderer.color;
    }
    private void Update()
    {
        if (this.wallRenderer.enabled == true)
        {
            IsBlocked = true;
        }
        if (turretRenderer.enabled && canAttack)
        {
            Enemy target = null;
            foreach (var enemy in Enemy.allEnemies)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < range) // Range
                {
                    target = enemy;
                    break;
                }
            }

            if (target != null)
            {
                StartCoroutine(AttackCoroutine(target));
            }
        }
    }
    IEnumerator AttackCoroutine(Enemy target)
    {
        target.GetComponent<Enemy>().Attack(atk);
        canAttack = false;
        lineRenderer.SetPosition(1, target.transform.position);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
        yield return new WaitForSeconds(1f / atkSpeed); // Attack Speed
        canAttack = true;
    }

    internal void TurnGray()
    {
        //spriteRenderer.color = Color.gray;
        originalColor = spriteRenderer.color;
    }

    public void OnPointerEnter(PointerEventData eventData) //Hover
    {
        hoverRenderer.enabled = true;
        GM.TargetTile = this;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverRenderer.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData) //On click
    {
        ScriptableTurret item = SelectedItem.Instance.selectItem;

        if (item != null && wallRenderer.enabled == false)
        {
            turretRenderer.enabled = !turretRenderer.enabled;
            IsBlocked = turretRenderer.enabled;
            //Changer les propriétés pour celles de l'item sélectionné
            turretRenderer.sprite = item.turretSprite;
            //lineRenderer = item.lineRenderer;
            range = item.range;
            atk = item.attack;
            atkSpeed = item.attackSpeed;

            //Si le lineRenderer de l'item n'existe pas, ne pas le changer
            if (item.lineRenderer != null) { lineRenderer.colorGradient = item.lineRenderer.colorGradient; }


        }

        /*if (turret.turret == null)
        //{
        //    turret.turret = SelectedItem.Instance.selectItem;
        //    foreach (TurningTurret t in turrets)
        //    {
        //        if (t.id == SelectedItem.Instance.selectItem.id)
        //        {
        //            t.gameObject.SetActive(true);
        //            return;
        //        }
        //    }
        //}*/
    }
    internal void SetEnemySpawn()
    {
        spawnRenderer.enabled = true;
    }

    internal void SetPath(bool isPath)
    {
        spriteRenderer.color = isPath ? Color.yellow : originalColor;
        if (GM.pathToGoal.Contains(this))
        {
            isAPath = true;
        }
        if (GM.TargetTile == this)
        {
            flagRenderer.enabled = true;
        }
    }


}
