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
    [SerializeField] private SpriteRenderer spawnRenderer, turretRenderer;
    private TurningTurret turret;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public GameManager GM { get; internal set; }

    public int X { get; internal set; }
    public int Y { get; internal set; }
    public bool IsBlocked { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //turretRenderers
        originalColor = spriteRenderer.color;
    }

    internal void TurnGray()
    {
        spriteRenderer.color = Color.gray;
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
        if (SelectedItem.Instance.selectItem != null)
        {
            turretRenderer.enabled = !turretRenderer.enabled;
            IsBlocked = true;
            turretRenderer.sprite = SelectedItem.Instance.selectItem.turretSprite;
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
    }


}
