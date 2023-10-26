using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public int damage = 1;
    public GameObject hitBox;
    public float attackDelayStart = 0.3f;
    private float attackDelay;
    private Vector2 mousePos;
    private GameObject enemyTarg;
    private AICharacter AiEnemy;
    private bool canHit = true;
    

    //enter collision, detects if has enemy tag, if true set enemy to attacking var
    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if ((other.tag == "Villager") || (other.tag == "Hunter"))
        {
            enemyTarg = other.gameObject;
            AiEnemy = other.GetComponent<AICharacter>();
        }
    }

    //exit clears enemy target
    private void OnTriggerExit2D(Collider2D other)
    {
        enemyTarg = null;
        AiEnemy = null;



        if (other == enemyTarg)                            //((other.tag == "Villager") || (other.tag == "Hunter"))
        {
            enemyTarg = null;
            AiEnemy = null;
        }
    }


    //damages the enemy (damage not yet implemented)
    void damageEnemy()
    {
        
        
        
        if (enemyTarg != null)
        {

            Debug.Log(enemyTarg.name);
            AiEnemy.health -= damage;
        }


    }


    void Start()
    {
        attackDelay = attackDelayStart;
    }


    void Update()
    {

        //gets mous pos (0, 0 at centre screen)
        mousePos.x = Input.mousePosition.x - (Screen.width / 2);
        mousePos.y = Input.mousePosition.y - (Screen.height / 2);
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        hitBox.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //swap "angle" with another 0 if cam is another angle

        //calls damage enemy when LMB is pressed
        if (Input.GetKey(KeyCode.Mouse0))
        {
            
            if (canHit)
            {
                
                damageEnemy();
                canHit = false;
            }
            attackDelay -= Time.deltaTime;
            if (attackDelay <= 0)
            {
                canHit = true;
                attackDelay = attackDelayStart;
            }
            
        }
        

    }
}
