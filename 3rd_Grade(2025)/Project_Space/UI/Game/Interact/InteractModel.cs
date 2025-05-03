using System;
using UnityEngine;

namespace JMT.UISystem.Interact
{
    public enum InteractType
    {
        None = 0,
        Item = 1,
        Progress = 2,
        Building = 3,
        Station = 4,
        Attack = 5,
        Zeolite = 6,
        Village = 7,
        Laboratory = 8,
    }

    public class InteractModel
    {
        public event Action<InteractType> OnChangeInteractEvent;
        private InteractType currentType;

        public InteractType InteractType => currentType;
        public void ChangeInteract(InteractType type)
        {
            currentType = type;
            OnChangeInteractEvent?.Invoke(type);
        }
    }
}
