using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents
{
    public static UnityAction<GameObject, float> characterHealed;
    public static UnityAction<GameObject, float> characterDamaged;
}
