using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class GrayCat : PlayableCharacter
{
    protected override void Start()
    {
        base.Start(); 
    }
    public override void SpecialAbility()
    {
        // TODO: meow ability put the barking dog trap to sleep
        Debug.Log("grayCat uses special ability: Meow to put the dog to sleep.");
    }

    public override void OnMovement(InputValue value)
    {
        base.OnMovement(value);
    }

}
