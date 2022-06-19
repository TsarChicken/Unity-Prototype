using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public readonly GameEvent<Vector2> onMove = new GameEvent<Vector2>();
    public readonly GameEvent<Vector2> onAim = new GameEvent<Vector2>();
    public readonly GameEvent onGravitySwitch = new GameEvent();
    public readonly GameEvent onInteract = new GameEvent();
    public readonly GameEvent onJump = new GameEvent();
    public readonly GameEvent onCrouch = new GameEvent();
    public readonly GameEvent onMelee = new GameEvent();
    public readonly GameEvent onEnvironmentGravity = new GameEvent();
    public readonly GameEvent onPlayerGravity = new GameEvent();
    public readonly GameEvent onFire = new GameEvent();
    public readonly GameEvent onTrajectory = new GameEvent();
    public readonly GameEvent onWeaponSwitch = new GameEvent();
    public readonly GameEvent onFireModeSwitch = new GameEvent();
    public readonly GameEvent onHighlight = new GameEvent();
    public readonly GameEvent onPause = new GameEvent();
}
