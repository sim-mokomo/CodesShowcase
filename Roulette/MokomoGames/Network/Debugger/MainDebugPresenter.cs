using TMPro;
using UnityEngine;

namespace MokomoGames.Network.Debugger
{
    public class MainDebugPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentLoggedInUserId;

        public void SetCurrentLoggedInUserId(string userId)
        {
            currentLoggedInUserId.text = $"Current Logged In User Id: {userId}";
        }
    }
}