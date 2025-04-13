using JMT.UISystem;
using UnityEngine;

namespace JMT.Player
{
    public class PlayerSystem : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private FillBarUI playerHpUI;
        [SerializeField] private FillBarUI playerOxygenUI;

        private void Awake()
        {
            player.OnDamageEvent += playerHpUI.SetHpBar;
            player.OnOxygenEvent += playerOxygenUI.SetHpBar;
        }
    }
}