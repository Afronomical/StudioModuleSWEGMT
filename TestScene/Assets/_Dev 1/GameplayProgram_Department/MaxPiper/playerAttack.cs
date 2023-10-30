using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public int damage = 1;
    public GameObject hitBox;
    public float attackDelayStart = 0.5f;
    private float attackDelay;
    private Vector2 mousePos;
    private GameObject enemyTarg;
    private AICharacter AiEnemy;
    private bool canHit = true;
    

    private Animator animator;
    private PlayerAnimationController animationController;

    public static List<GameObject> enemyList = new List<GameObject>();

    //enter collision, detects if has enemy tag, if true set enemy to attacking var
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Villager") || (other.tag == "Hunter"))
        {
            if (!enemyList.Contains(other.gameObject))
            {
                enemyList.Add(other.gameObject);
            }
            
        }
    }

    //exit clears enemy target
    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.tag == "Villager") || (other.tag == "Hunter"))
        {
            enemyList.Remove(other.gameObject);
            Debug.Log(other.name);
        }
    }


    //damages the enemy
    void damageEnemy()
    {
        foreach (GameObject obj in enemyList)
        {
            if (obj.TryGetComponent(out AICharacter AiEnemy))
            {
                AiEnemy.health -= damage;
                Debug.Log(enemyTarg.name);
                AudioManager.Manager.PlaySFX("NPC_TakeDamage");
                //enemyTarg.GetComponentInChildren<AI_AnimationController>().ChangeAnimationState(AI_AnimationController.AnimationStates.Hurt);
            }

            

        }

        //for (int i = enemyList.Count -1; i >= 0; i--)
        //{
        //    GameObject o = enemyList[i];
        //    o.
        //}

        //if (enemyTarg != null)
        //{
            
        //    Debug.Log(enemyTarg.name);
        //    AiEnemy.health -= damage;
        //    AudioManager.Manager.PlaySFX("NPC_TakeDamage");
        //}


    }


    void Start()
    {
        attackDelay = attackDelayStart;

        animator = GetComponent<Animator>();
        animationController = GetComponent<PlayerAnimationController>();

        //enemyHealth = AiEnemy.health;
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
                animationController.ChangeAnimationState(PlayerAnimationController.AnimationStates.SlashAttack);
                AudioManager.Manager.PlaySFX("PlayerAttack");
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
