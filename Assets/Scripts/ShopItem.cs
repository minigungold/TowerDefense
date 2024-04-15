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
    public SpriteRenderer turretRenderer;
    public SelectedItem item;
    public ScriptableTurret turret;
    [SerializeField] GameObject hoverImage;
    [SerializeField] Button button;
    public bool isSelected;
    private GameTile tile;

    [SerializeField] private int turretIndex = 0;

    private void Start()
    {
        tile = GetComponent<GameTile>();
        turretRenderer.sprite = turret.turretSprite;
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
