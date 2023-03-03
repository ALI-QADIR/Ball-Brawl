using UnityEngine;

namespace Assets.Scripts
{
    public enum PowerUpType
    { None, PushBack, Rocket }

    public class PowerUp : MonoBehaviour
    {
        public PowerUpType powerUpType;
    }
}