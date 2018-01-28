using UnityEngine;

public class StressChannelManager : MonoBehaviour
{
    public float MaxLevel = 50.0f;
    public Transform Needle;
    public float MinNeedleAngle = 90.0f;
    public float MaxNeedleAngle = -90.0f;

    public ClientManager TheClientManager;
    [Range(0.0f, 100.0f)] public float PercentToSucceed = 50.0f;

    // The first index will represent the stress level; second index the channel level
    // They both can range from -MaxLevel to MaxLevel
    private float[] _counterLevel;

    private float _numCountersDivisor;

    private bool ghostSummoned;
    private bool dialogRead;

    private const int StressIndex = 0;
    private const int ChannelIndex = 1;

    private const float MaxPercent = 100.0f;

    private void Start()
    {
        _counterLevel = new[] {0.0f, -MaxLevel};
        // The _numCountersDivisor is equal to 2x the length of the counter level array multiplied by the MaxLevel
        // This is because each counter takes up a range from -MaxLevel to MaxLevel
        _numCountersDivisor = _counterLevel.Length * 2.0f * MaxLevel;

        PercentToSucceed /= MaxPercent;
    }

    private void Update()
    {
        // This calculation will ensure that the two different ranges will be brought together into one
        var lerpT = Mathf.Abs(-1.0f * (_counterLevel[StressIndex] + MaxLevel) / _numCountersDivisor +
                              -1.0f * (_counterLevel[ChannelIndex] + MaxLevel) / _numCountersDivisor);

        var newNeedleAngle = Mathf.LerpAngle(MinNeedleAngle, MaxNeedleAngle, lerpT);
        Needle.localRotation = Quaternion.Euler(Needle.localEulerAngles.x, newNeedleAngle, Needle.localEulerAngles.z);

        if (lerpT >= PercentToSucceed)
        {
            TheClientManager.SummonGhost();
            _counterLevel[ChannelIndex] = -MaxLevel;
        }
    }

    public void AffectStressLevel(float amount)
    {
        _counterLevel[StressIndex] = Mathf.Clamp(_counterLevel[StressIndex] + amount, -MaxLevel, MaxLevel);
    }

    public void AffectChannelLevel(float amount)
    {
        _counterLevel[ChannelIndex] = Mathf.Clamp(_counterLevel[ChannelIndex] + amount, -MaxLevel, MaxLevel);
    }
}