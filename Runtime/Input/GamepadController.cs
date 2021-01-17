using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Scream.UniMO.Input
{
    public static class GamepadController
    {
        static Dictionary<VibrateDuration, int> duration =
            new Dictionary<VibrateDuration, int>() { { VibrateDuration.SHORT, 100 }, { VibrateDuration.NORMAL, 200 }, { VibrateDuration.LONG, 500 }
            };

        static Dictionary<VibrateStrength, Strength> strength =
            new Dictionary<VibrateStrength, Strength>() { { VibrateStrength.SLIGHT, new Strength (.25f, .25f) }, { VibrateStrength.NORMAL, new Strength (.5f, .5f) }, { VibrateStrength.STRONG, new Strength (.95f, .95f) }
            };

        static async public void VibrateController(int time = 100, float lowFrequency = 0.25f, float highFrequency = 0.75f)
        {
            if (Gamepad.current == null) return;
            Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);
            await Task.Delay(time);
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }

        static async public void VibrateController(VibrateDuration duration = VibrateDuration.NORMAL, VibrateStrength strength = VibrateStrength.NORMAL)
        {
            if (Gamepad.current == null) return;
            Strength s = GamepadController.strength[strength];
            Gamepad.current.SetMotorSpeeds(s.LowFrequency, s.HighFrequency);
            await Task.Delay(GamepadController.duration[duration]);
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }

        static public void SetDS4LightColor(Color color)
        {
            DualShockGamepad ds4 = Gamepad.current as DualShockGamepad;
            ds4.SetLightBarColor(color);
        }

        private class Strength
        {
            public Strength(float low, float high)
            {
                this.LowFrequency = low;
                this.HighFrequency = high;
            }
            public float LowFrequency { get; private set; }
            public float HighFrequency { get; private set; }
        }
    }

    public enum VibrateStrength
    {
        STRONG,
        NORMAL,
        SLIGHT,
    }

    public enum VibrateDuration
    {
        SHORT,
        NORMAL,
        LONG,
    }

}