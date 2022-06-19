using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
public enum RumblePattern
{
    Constant,
    Pulse,
    Linear
}
public class Rumbler : MonoBehaviour
{
    private PlayerInput playerInput;
    private RumblePattern activeRumblePattern;
    private float rumbleDuration;
    private float pulseDuration;
    private float lowA;
    private float lowStep;
    private float highA;
    private float highStep;
    private float rumbleStep;
    private bool isMotorActive = false;

    [SerializeField]
    private float nonStopStep = 0f;
    [SerializeField]
    private float minNonStopLowA = .1f;
    [SerializeField]
    private float minNonStopHighA = .1f;
    [SerializeField]
    private float nonStopModifier = 1.5f;

    private float currentNonStopLowA = 0.1f;
    private float currentNonStopHighA = 0.1f;

    // Public Methods
    public void RumbleConstant(float low, float high, float duration)
    {
        activeRumblePattern = RumblePattern.Constant;
        lowA = low;
        highA = high;
        rumbleDuration = Time.time + duration;
    }

    public void RumblePulse(float low, float high, float burstTime, float duration)
    {
        activeRumblePattern = RumblePattern.Pulse;
        lowA = low;
        highA = high;
        rumbleStep = burstTime;
        pulseDuration = Time.time + burstTime;
        rumbleDuration = Time.time + duration;
        isMotorActive = true;
        var g = GetGamepad();
        g?.SetMotorSpeeds(lowA, highA);
    }

    public void RumbleLinear(float lowStart, float lowEnd, float highStart, float highEnd, float duration)
    {
        activeRumblePattern = RumblePattern.Linear;
        lowA = lowStart;
        highA = highStart;
        lowStep = (lowEnd - lowStart) / duration;
        highStep = (highEnd - highStart) / duration;
        rumbleDuration = Time.time + duration;
    }

    public void StopRumble()
    {
        var gamepad = GetGamepad();
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(0, 0);
        }
    }

    public void RumbleNonStop(Gamepad gp)
    {
        if (gp == null)
            return;
        gp.SetMotorSpeeds(currentNonStopLowA, currentNonStopHighA);
    }

    public void IncreaseNonStop()
    {   if(currentNonStopHighA == 0 || currentNonStopLowA == 0)
        {
            currentNonStopHighA = minNonStopHighA;
            currentNonStopLowA = minNonStopLowA;
        } else
        {
            currentNonStopHighA *= nonStopModifier;
            currentNonStopLowA *= nonStopModifier;
        }
    }
    private void Awake()
    {
        playerInput = PlayerInfo.instance.GetComponent<PlayerInput>();
    }

    private void Update()
    {
        var gamepad = GetGamepad();



        if (Time.time > rumbleDuration)
        {

            StopRumble();
            return;
        }

            if (gamepad == null)
            {
                return;
            }
            switch (activeRumblePattern)
            {
                case RumblePattern.Constant:
                    gamepad.SetMotorSpeeds(lowA, highA);
                    break;

                case RumblePattern.Pulse:

                    if (Time.time > pulseDuration)
                    {
                        isMotorActive = !isMotorActive;
                        pulseDuration = Time.time + rumbleStep;
                        if (!isMotorActive)
                        {
                            gamepad.SetMotorSpeeds(0, 0);
                        }
                        else
                        {
                            gamepad.SetMotorSpeeds(lowA, highA);
                        }
                    }

                    break;
                case RumblePattern.Linear:
                    gamepad.SetMotorSpeeds(lowA, highA);
                    lowA += (lowStep * Time.deltaTime);
                    highA += (highStep * Time.deltaTime);
                    break;
                default:
                    break;
            }
        
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        StopRumble();
    }
    private void OnDisable()
    {

        StopAllCoroutines();
        StopRumble();
    }

    private Gamepad GetGamepad()
    {
        return Gamepad.all.FirstOrDefault(g => playerInput.devices.Any(d => d.deviceId == g.deviceId));

       
    }
}

