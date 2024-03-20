using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunType gunType;
    public Joystick joystick;
    public float offset;
    public GameObject bullet;
    public Transform shotPoint;
    public enum GunType{Default, Enemy}
    private float timeBtwShots;
    public float startTimeBtwShots;
    private float rotZ;
    private Vector3 difference;
    private controller player;
    public AudioSource laser;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<controller>();
    }
    void Update()
    {
        if(gunType == GunType.Default)
        {
            if(player.controlType == controller.ControlType.PC)
            {
                difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                rotZ = Mathf.Atan2(difference.y, difference.x)* Mathf.Rad2Deg;
            }
            else if(player.controlType == controller.ControlType.Android && Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f)
            {
                rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal)* Mathf.Rad2Deg;
            }
        }
        else if(gunType == GunType.Enemy)
        {
                difference =player.transform.position - transform.position;
                rotZ = Mathf.Atan2(difference.y, difference.x)* Mathf.Rad2Deg;
        }
        transform.rotation = Quaternion.Euler(0f,0f,rotZ + offset);
        if(timeBtwShots <=0){
            if(Input.GetMouseButton(0) && player.controlType == controller.ControlType.PC || gunType == GunType.Enemy)
            {
                Shoot();
            }
            else if(player.controlType == controller.ControlType.Android)
            {
                if(joystick.Horizontal != 0 || joystick.Vertical != 0)
                {
                    Shoot();
                }
            }
        }
        else{
            timeBtwShots -= Time.deltaTime;
        }
    }
    private void Shoot()
    {       
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        timeBtwShots = startTimeBtwShots; 
    }
}
