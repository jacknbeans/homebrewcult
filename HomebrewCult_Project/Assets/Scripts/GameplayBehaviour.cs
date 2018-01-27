using UnityEngine;

public class GameplayBehaviour : MonoBehaviour
{
    protected StressChannelManager StressChannelMan;

    private void Awake()
    {
        StressChannelMan = FindObjectOfType<StressChannelManager>();
    }

    protected void Stress(float amount)
    {
        
    }

    protected void Channel(float amount)
    {
        
    }
}