using UnityEngine;

public class Mine : MonoBehaviour
{

    [SerializeField] private float armDelay = 3f;
    [SerializeField] private float damage = 9999f;
    [SerializeField] private float triggerRadius = 0.8f;

    private enum MineState { Arming, Armed } 
    private MineState currentState = MineState.Arming;

    private float armTimer = 0f;
    private Renderer mineRenderer;

    [SerializeField] private Material armedMaterial;
    [SerializeField] private Material armingMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mineRenderer = GetComponent<Renderer>();
        if (armingMaterial != null)
            mineRenderer.material = armingMaterial;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == MineState.Arming)
        {
            armTimer += Time.deltaTime;
            if(armTimer >= armDelay)
            {
                currentState = MineState.Armed;
                if (armedMaterial != null)
                    mineRenderer.material = armedMaterial;
                Debug.Log("Mine armed!");
            }
        }
        else if (currentState == MineState.Armed)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, triggerRadius);
            foreach(Collider hit in hits)
            {
                EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    
                    //Free cell on grid
                    GridManager gridManager = FindFirstObjectByType<GridManager>();
                    if (gridManager != null)
                        gridManager.FreeCell(transform.position);
                    Debug.Log("Mine exploded!");
                    Destroy(gameObject);
                    return;
                }
            }
        }
        
    }
}
