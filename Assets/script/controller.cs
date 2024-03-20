using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class controller : MonoBehaviour
{
    public Text textHP;
    public Image BarHP;
    public ControlType controlType;
    public Joystick joystick;
    public enum ControlType{PC,Android};
    public float speed;
    public int health;
    public int MaxHealth;
    public GameObject chit;
    public chit chitTimer;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelosity;
    private Animator anim;
    private bool facingRight = true;
    private bool KeyButtonPushed;
    public GameObject KeyIcon;
    public GameObject wallEffect;
    void Start()
    {
        textHP.text = "HP: " + health;
        rb =  GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(controlType == ControlType.PC){
            joystick.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if(controlType == ControlType.PC)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else if(controlType == ControlType.Android)
        {
            moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        }
        moveVelosity = moveInput.normalized * speed;
        if(moveInput.x == 0f){
            anim.SetBool("istrigget", false);
        }
        else{
            anim.SetBool("istrigget", true);
        }
        if(!facingRight && moveInput.x > 0){
            flip();
        }
        else if(facingRight && moveInput.x < 0){
            flip();
        }
    }
    void FixedUpdate() {
        rb.MovePosition(rb.position + moveVelosity * Time.fixedDeltaTime);    
    }
    private void flip(){
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x*= -1;
        transform.localScale = Scaler;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("potion"))
        {
            ChangeHealth(2);
            Destroy(other.gameObject);
        }
        if(other.CompareTag("chit"))
        {   
            if(!chit.activeInHierarchy)
            {
                chit.SetActive(true); 
                chitTimer.gameObject.SetActive(true);
                chitTimer.isCooldown = true;
                Destroy(other.gameObject);
            }   
            else
            {
                chitTimer.ResetTimer();
            }    
        }
        if(other.CompareTag("Key"))
        {
            KeyIcon.SetActive(true);
            Destroy(other.gameObject);
        }
    }
    public void onKeyButtonDown()
    {
        KeyButtonPushed = !KeyButtonPushed;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Door") && KeyButtonPushed && KeyIcon.activeInHierarchy)
        {
            Instantiate(wallEffect, other.transform.position, Quaternion.identity);
            KeyIcon.SetActive(false);
            other.gameObject.SetActive(false);
            KeyButtonPushed = false;  
        }
    }
    public void ChangeHealth(int healthValue){
        if(!chit.activeInHierarchy || chit.activeInHierarchy && healthValue > 0)
        {

            health += healthValue;
            if(health > MaxHealth)
            {
                health = MaxHealth;
            }
            textHP.text = "HP: " + health;
            BarHP.fillAmount = (float)health / (float)MaxHealth;
            if(health <= 0)
            {
                GameOver();
            }
        }
    }
    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } 
}
