using UnityEngine;

public class Flag : MonoBehaviour
{
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
