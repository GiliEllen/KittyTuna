using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class CalicoCat : PlayableCharacter
{
        protected override void Start()
    {
        base.Start(); 
    }

    public override void SpecialAbility()
    {
        // TODO: ability to trip the toddler trap
        Debug.Log("CalicoCat uses special ability: trip the toddler.");
    }

    public override void OnMovement(InputValue value)
    {
        base.OnMovement(value); 
    }
}


