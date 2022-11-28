using System.Collections.Generic;
using System.Linq;
using Roulette.Licence.MasterData;
using UnityEngine;

namespace Roulette.Licence
{
    public class LicenceRepository : MonoBehaviour
    {
        [SerializeField] private LicenceListSO licenceListSo;

        public List<LicenceEntity> Load()
        {
            return
                licenceListSo
                    .LicenceList
                    .Select(x => new LicenceEntity(x.Title, x.LicenceText.text))
                    .ToList();
        }
    }
}