using UnityEngine;

public enum ControlScheme { gamepad, keyboard }

public class SceneSettings : MonoBehaviour
{
    [field: SerializeField] public ControlScheme activeControlScheme { get; private set; }
}

