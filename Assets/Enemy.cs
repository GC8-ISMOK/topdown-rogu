using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private float timebtwAttack;
    public float starttimebtwAttack;
    public float health;
    public float MaxHealth;
    public float speed;
    public int damage;
    public bool playerNotInRoom;
    private bool stopped;
    public GameObject Bar;
    private controller player;
    private Animation anim;
    private AddRoom room;
    public float healthPro;
    public float stopTime;
    public Quaternion f;
    public float score = 0;
    public Text tx;

    private void Start() 
    {
        player = FindObjectOfType<controller>();
        room = GetComponentInParent<AddRoom>();
    }
    private void Update() 
    {
        if(health <=0)
        {
            Destroy(gameObject);
            room.enemies.Remove(gameObject);
            score++;
            tx.text = "score: " + score;
        }
        if(player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        if(!stopped)
        {
            transform.position =  Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Bar.transform.eulerAngles = new Vector3(transform.rotation.x, -1f * (90 - (100f * ((float)health / MaxHealth)) * (90f / 100f)), transform.rotation.z);
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(timebtwAttack <= 0)
            {
                player.health -= damage;
                timebtwAttack = starttimebtwAttack;
            }
            else
            {
                timebtwAttack -=  Time.deltaTime;
            }
        }
    }
}
