using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{

    public GameObject hitBox;
    private Vector2 mousePos;
    private GameObject enemyTarg;

    //enter collision, detects if has enemy tag, if true set enemy to attacking var
    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag == "enemy")
        {
            enemyTarg = other.gameObject;
        }
    }

    //exit clears enemy target
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            enemyTarg = null;
            
        }
    }


    //damages the enemy (damage not yet implemented)
    void damageEnemy()
    {
        if (enemyTarg != null)
        {
            Debug.Log(enemyTarg.name);
            //damage enemyTarg
        }


    }


    void Start()
    {

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

            damageEnemy();
        }
    }
}