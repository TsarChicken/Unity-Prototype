using UnityEngine;

public class FollowTarget
{
    private float _minimumDistance = 1f, _speed = 5f;

    public FollowTarget(float minDist, float sp)
    {
        _minimumDistance = minDist;
        _speed = sp;
    }

    public bool Follow(Transform self, Transform target)
    {
        if (Mathf.Abs(self.position.x - target.position.x) < _minimumDistance)
        {
            return false;
        }
        var curPos = self.position;
        curPos.x = Vector2.MoveTowards(self.position, target.position, _speed * Time.deltaTime).x;
        self.position = curPos;
        return true;
    }

}
