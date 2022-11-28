using UnityEngine;

namespace Roulette.Licence.MasterData
{
    [CreateAssetMenu(
        fileName = "LicenceItem",
        menuName = "Licence/CreateItem",
        order = 1)]
    public class LicenceListItemSO : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private TextAsset licenceText;

        public string Title => title;

        public TextAsset LicenceText => licenceText;
    }
}