using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI atkSpeedText;
    [SerializeField] private TextMeshProUGUI rangeText;
    [SerializeField] private TextMeshProUGUI costText;
    public static SelectedItem Instance;
    private SpriteRenderer selectedItemRenderer;
    public ScriptableTurret selectItem;
    GameTile tile;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        selectedItemRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (selectItem != null)
        {
            selectedItemRenderer.sprite = selectItem.turretSprite;
            costText.text = $"Cost : {selectItem.cost}";
            atkText.text = $"Atk : {selectItem.attack}";
            atkSpeedText.text = $"Atk Speed : {selectItem.attackSpeed}";
            rangeText.text = $"Range : {selectItem.range}";

        }

    }

}
