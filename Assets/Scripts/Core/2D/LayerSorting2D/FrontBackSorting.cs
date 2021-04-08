using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * This class offers the most simple sorting.
 * Whether the character's sprite is aoove or below the sprite is determined by the character's y position in comparison to the host's sprite
 */
public class FrontBackSorting : IFrontBackSorting
{
    // Start is called before the first frame update
    void Start()
    {
        if (useSelfAsHost)
        {
            host = this.transform;
        }
    }
    /**
     * This function return whether the host's sprite should be above the character's sprite
     */
    public override bool IsAboveCharacter(Vector3 characterPos)
    {
        return characterPos.y > host.position.y;
    }
    /**
    * This function return whether the host's sprite should be below the character's sprite
    */
    public override bool IsBelowCharacter(Vector3 characterPos)
    {
        return characterPos.y < host.position.y;
    }
}
