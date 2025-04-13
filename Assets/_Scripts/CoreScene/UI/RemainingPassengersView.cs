using TMPro;
using UnityEngine;

namespace _Scripts.CoreScene
{
    public class RemainingPassengersView: MonoBehaviour
    {
        public TextMeshProUGUI RemainingPassengersText;
        
        public void SetText(int amount, int maxAmount) => RemainingPassengersText.text = $"{amount}/{maxAmount}";
    }
}