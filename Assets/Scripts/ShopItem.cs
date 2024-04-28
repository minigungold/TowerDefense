using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject hoverImage;
    [SerializeField] Button button;
    public SpriteRenderer turretRenderer;
    public SelectedItem item;
    public ScriptableTurret turret;
    public bool isSelected;
    private GameTile tile;

    [SerializeField] private int turretIndex = 0;

    private void Start()
    {
        tile = GetComponent<GameTile>();
        if (tile != null)
        {
            turretRenderer.sprite = turret.turretSprite;
            turretRenderer.enabled = true;
        }
    }
    public void buttonClicked()
    {
        item.selectItem = turret;
        hoverImage.SetActive(true);


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverImage.SetActive(true);
        isSelected = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverImage.SetActive(false);
        isSelected = false;
    }
}
