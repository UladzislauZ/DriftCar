using System;

namespace DriftCar
{
    public interface IDriftController
    {
        event Action<float> ChangedScore;
        float Score { get; }
    }
}