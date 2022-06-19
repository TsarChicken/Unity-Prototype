using System.Collections;
using UnityEngine;

[RequireComponent(typeof(KeyCharacter))]
public class ActionModifier : MonoBehaviour, IEventObservable
{
    public CharacterAction Action { get; set; }
    [SerializeField]
    private float timeDelay = 5f;

    private KeyCharacter _character;
    

    private WaitForSeconds _waitForSecs;
    private void Awake()
    {
        _character = GetComponent<KeyCharacter>();
        _waitForSecs = new WaitForSeconds(timeDelay);
    }

    public void OnEnable()
    {
        _character.onBecomingHostile.AddListener(StartAction);
        if (_character.HP)
        {
            _character.HP.onDie.AddListener(StopAction);
        }
    }

    public void OnDisable()
    {
        _character.onBecomingHostile.RemoveListener(StartAction);
        _character.HP.onDie.RemoveListener(StopAction);
    }
    private IEnumerator HandleAction()
    {
        while (true)
        {
            yield return _waitForSecs;
            Action.Work();
        }
    }

    private void StartAction()
    {
        StartCoroutine(HandleAction());
    }

    private void StopAction()
    {
        StopCoroutine(HandleAction());
        Action.Stop();
    }

}
