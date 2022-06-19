using UnityEngine;

public class PhysicsDataManager : Singleton<PhysicsDataManager>
{
    public readonly string PLAYER_TAG = "Player";
    public readonly string ENEMY_TAG = "Enemy";
    public readonly string INTERACTABLE_TAG = "Interactive Object";
    public readonly string KEY_CHARACTER_TAG = "Character";
    public readonly string BULLET_TAG = "Bullet";
    public readonly string GROUND_TAG = "Ground";

    public bool IsPlayer(GameObject potentialPlayer)
    {
        return potentialPlayer.CompareTag(PLAYER_TAG);
    }

    public bool IsInteractable(GameObject potentialBreakable)
    {
        return potentialBreakable.CompareTag(ENEMY_TAG) ||
            potentialBreakable.CompareTag(PLAYER_TAG)
            || potentialBreakable.CompareTag(KEY_CHARACTER_TAG)
            || potentialBreakable.CompareTag(BULLET_TAG)
            || potentialBreakable.CompareTag(GROUND_TAG); 
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
    }
}
