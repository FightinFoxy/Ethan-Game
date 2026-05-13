using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public GameObject prefab;
    public int pointCost;
    public float moveSpeed;
    public float maxHealth;
    public float attackDamage;
    public float attackRate;
    public int minimumWave;
    public int spawnWeight;
}
