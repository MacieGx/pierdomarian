using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Character character;

    // Start is called before the first frame update
    void Start()
    {
        character = Character.getInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if(!character.animator.GetBool("isJumping") && character.IsJumping())
        {
            character.animator.SetBool("isJumping", true);
        }

        if(character.animator.GetBool("isJumping") && !character.IsJumping())
        {
            character.animator.SetBool("isJumping", false);
        }
    }
}
