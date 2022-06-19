using UnityEngine;

public class EnemyView
{
    private Vector3 _scale;
   
    public EnemyView(Vector3 scale)
    {
        _scale = scale;
    }

    public void UpdateView(Transform self, Transform target)
    {
        bool right = IsTargetRight(self, target);
        bool left = IsTargetLeft(self, target);
        if (right)
        {
            _scale.x = 1;
        }
        if (left)
        {
            _scale.x = -1;
        }
        _scale.x *= Mathf.Sign(self.right.x);
        self.localScale = _scale;
    }

    private bool IsTargetRight(Transform self, Transform target)
    {
        return target.position.x > self.position.x;
    }

    private bool IsTargetLeft(Transform self, Transform target)
    {
        return target.position.x < self.position.x;
    }
}
