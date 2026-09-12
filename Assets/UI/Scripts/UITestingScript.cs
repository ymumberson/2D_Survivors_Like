using UnityEngine;

public class UITestingScript : MonoBehaviour
{
#if !UNITY_EDITOR
    void Awake()
    {
        
        Destroy(this.gameObject);
        
    }
#endif

    public void SetTimeScale(int timeScale)
    {
        Time.timeScale = timeScale;
    }
}
