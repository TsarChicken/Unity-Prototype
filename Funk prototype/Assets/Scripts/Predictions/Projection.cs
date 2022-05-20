using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Projection : Singleton<Projection>
{
    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxPhysicsFrameIterations = 100;
    [SerializeField] private List<Transform> _obstaclesParent;

    private Scene _simulationScene;
    private PhysicsScene2D _physicsScene;
    private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();

    private void Awake()
    {
        //CreatePhysicsScene();
    }

    private void CreatePhysicsScene()
    {
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        _physicsScene = _simulationScene.GetPhysicsScene2D();

        foreach (Transform obj in _obstaclesParent)
        {
            GameObject ghostObj = null;
            //if (!obj.CompareTag("Bullet"))
            //{
                ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            //} else
            //{
            //    ghostObj = obj.gameObject;
            //}
                if (ghostObj.TryGetComponent(out Renderer renderer))
                {
                    renderer.enabled = false;
                }
                else
                {
                    foreach (var rend in ghostObj.GetComponentsInChildren<Renderer>())
                    {
                        rend.enabled = false;
                    }
                }
                if (ghostObj.TryGetComponent(out Rigidbody2D rb))
                {
                    rb.isKinematic = true;
                }
            
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            print(LayerMask.LayerToName(ghostObj.gameObject.layer));
            if (!ghostObj.isStatic) _spawnedObjects.Add(obj, ghostObj.transform);
        }
    }

    private void Update()
    {
        foreach (var item in _spawnedObjects)
        {
            item.Value.position = item.Key.position;
            item.Value.rotation = item.Key.rotation;
        }
    }

    public void SimulateTrajectory(StandartBullet ghostBullet, Vector3 pos, Quaternion rotation)
    {

        _line.enabled = true;
        //ghostBullet.gameObject.SetActive(true);
        var ghostObj = Instantiate(ghostBullet.gameObject, pos, rotation);
        //ghostBullet.transform.SetPositionAndRotation(pos, rotation);
        SceneManager.MoveGameObjectToScene(ghostBullet.gameObject, _simulationScene);
        ghostBullet.Move(.2f);

        //print(LayerMask.LayerToName(ghostObj.gameObject.layer));

        _line.positionCount = _maxPhysicsFrameIterations;

        for (var i = 0; i < _maxPhysicsFrameIterations; i++)
        {
            if (ghostBullet != null && ghostBullet.isActiveAndEnabled)
            {
                //_line.SetPosition(i, ghostBullet.rb.position);
                _physicsScene.Simulate(Time.fixedDeltaTime);
            } else
            {
                break;
            }
           
        }

        Destroy(ghostObj.gameObject);
    }

    public void ClearLine()
    {
        //_line.enabled = false;

    }

}
