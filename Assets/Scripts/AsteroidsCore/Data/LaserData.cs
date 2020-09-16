using System;

namespace AsteroidsCore
{
    [Serializable]
    public struct LaserData : IData
    {
        #region Fields

        public float range;

        public float duration;

        public float cooldown;

        #endregion
    }
}