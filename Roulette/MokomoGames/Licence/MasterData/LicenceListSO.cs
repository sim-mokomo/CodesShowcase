using System.Collections.Generic;
using UnityEngine;

namespace Roulette.Licence.MasterData
{
    [CreateAssetMenu(
        fileName = "LicenceList",
        menuName = "Licence/CreateList",
        order = 1)]
    public class LicenceListSO : ScriptableObject
    {
        [SerializeField] private List<LicenceListItemSO> licenceList;
        public List<LicenceListItemSO> LicenceList => licenceList;
    }
}