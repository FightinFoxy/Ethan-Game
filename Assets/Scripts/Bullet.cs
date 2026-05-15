using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private AudioClip shootSound;


    private float lifetimeTimer = 0f;
    
    void start(){
        // Play audio when shot created
        AudioManager.Instance.PlaySound(shootSound);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        lifetimeTimer += Time.deltaTime;
        if (lifetimeTimer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
    
    // Called When Bullet touches another collider
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != LayerMask.NameToLayer("Enemy"))
            return; //Ignore collisons that aren't enemies
        
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }
    }

}
