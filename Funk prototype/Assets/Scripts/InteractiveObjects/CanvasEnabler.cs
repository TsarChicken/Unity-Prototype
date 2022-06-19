using UnityEngine;

public class CanvasEnabler : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
   
    public void UpdateCanvas()
    {
        if(canvas.activeSelf == false)
        {
            while(canvas.activeSelf == false)
            {
                canvas.SetActive(true);
            }
            PauseManager.instance.Pause();
        } else
        {
            canvas.SetActive(false);
            PauseManager.instance.Continue();
        }
    }
    
}
