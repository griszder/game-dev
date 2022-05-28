using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Costbar : MonoBehaviour {
    public static Costbar Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI ironText;
    
    public Costbar() {
        Instance = this;
    }
    public void Enable(int wood, int stone, int iron) {
        woodText.text  = wood == 0 ? "" : wood.ToString();
        stoneText.text = stone == 0 ? "" : stone.ToString();
        ironText.text  = iron == 0 ? "" : iron.ToString();
        
        gameObject.SetActive(true);
    }
    
    public void Disable() {
        gameObject.SetActive(false);
    }
}