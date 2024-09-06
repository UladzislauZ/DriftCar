using UnityEngine;

namespace DriftCar
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
    public class Level : ScriptableObject
    {
        public int Time;
    }
}