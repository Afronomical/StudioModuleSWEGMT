using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertedState : StateBaseClass
{
    private AIAnimationController animController;
    private Animator anim; 
    private float alertedTime = 0.75f;  // How long the state will last
    

    private void Start()
    {
        animController = transform.GetComponentInChildren<AIAnimationController>();
        anim = transform.GetComponentInChildren<Animator>();
        character = GetComponent<AICharacter>();
        character.isMoving = false;
        character.isAttacking = true;
        character.knowsAboutPlayer = true;
        
        StartCoroutine(Alerted());
    }

    public override void UpdateLogic()
    {

    }

    private IEnumerator Alerted()
    {
       
       
        GameObject go = Instantiate(character.exclamationMark, 
            new Vector3(character.transform.position.x, character.transform.position.y + 1, character.transform.position.z), 
            Quaternion.identity, transform.Find("Sprite"));
           
        yield return new WaitForSeconds(alertedTime);

        Destroy(go);
       

        if (character.characterType == AICharacter.CharacterTypes.Boss)
            GetComponent<BossStateMachineController>().ChangePhase();

        character.isAttacking = false;
        character.ChangeState(AICharacter.States.None);  // Return to normal states
    }
        
        
       
}
