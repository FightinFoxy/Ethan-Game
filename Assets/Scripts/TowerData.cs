using UnityEngine;

[CreateAssetMenu(fileName = "NewTower", menuName = "Towers/Tower Data")]
public class TowerData : ScriptableObject
{
    public string towerName;
    public int cost;
    public GameObject prefab;
    public Sprite icon;
    public KeyCode hotkey;
    public string description;

}
