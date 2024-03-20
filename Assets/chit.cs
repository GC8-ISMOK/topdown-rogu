using UnityEngine;
using UnityEngine.UI;

public class chit : MonoBehaviour
{
    public float cooldown;
    [HideInInspector]public bool isCooldown;
    private Image shieldImage;
    private controller player; 
    void Start()
    {
        shieldImage = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<controller>();
        isCooldown = true;
    }
    void Update()
    {
        if(isCooldown){
            shieldImage.fillAmount -= 1/ cooldown * Time.deltaTime;
            if(shieldImage.fillAmount <= 0)
            {
                shieldImage.fillAmount = 1;
                isCooldown = false;
                player.chit.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
    public void ResetTimer() {
        shieldImage.fillAmount = 1;    
    }
}
