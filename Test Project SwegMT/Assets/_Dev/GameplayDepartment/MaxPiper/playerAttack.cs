using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{


    private Vector2 mousePos;
    private GameObject enemyTarg;

    //enter collision, detects if has enemy tag, if true set enemy to attacking var
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("enemy") == true)
        {
            enemyTarg = other.gameObject;
        }
    }

    //exit clears enemy target
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
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
        transform.rotation = Quaternion.Euler(0, 0, angle); //swap "angle" with another 0 if cam is another angle

        //calls damage enemy when LMB is pressed
        if (Input.GetKey(KeyCode.Mouse0))
        {

            damageEnemy();
        }
    }
}
