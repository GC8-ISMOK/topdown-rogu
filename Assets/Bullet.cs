using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    [SerializeField] bool enemyBullet;
    public GameObject eff;
    private void Start() {
        Invoke("DestroyBul", lifetime);    
    }
    private void Update() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position,transform.up, distance,whatIsSolid);
        if(hitInfo.collider != null){
            if(hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            if(hitInfo.collider.CompareTag("Player")&& enemyBullet)
            {
                hitInfo.collider.GetComponent<controller>().ChangeHealth(-damage);
            }
            DestroyBul();

        }
        transform.Translate(Vector2.up* speed * Time.deltaTime);    
    }
    public void DestroyBul(){
        GameObject var =  Instantiate(eff, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(var, 0.75f);
    }
}
