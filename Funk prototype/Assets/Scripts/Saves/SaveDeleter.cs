using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveDeleter : MonoBehaviour, IEventObservable
{
    private Pushable _pushable;

    private void Awake()
    {
        _pushable = GetComponent<Pushable>();
    }
    public void OnEnable()
    {
        _pushable.onPush.AddListener(Delete);
    }
    public void OnDisable()
    {
        _pushable.onPush.RemoveListener(Delete);
    }
    private void Delete()
    {
        StartCoroutine(DeleteWithDelay());
    }
    private IEnumerator DeleteWithDelay()
    {
        yield return new WaitForSeconds(1.5f);
        DataManager.instance.DeleteSave();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }


}
