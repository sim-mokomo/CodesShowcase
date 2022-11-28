using System;
using System.Collections.Generic;
using System.Linq;
using Protobuf;
using UnityEngine;
using UnityEngine.UI;

namespace Roulette.Home.Edit
{
    public class EditDataSetView : MonoBehaviour
    {
        [SerializeField] private EditDataSetItemView itemViewPrefab;
        [SerializeField] private RectTransform contentRoot;
        [SerializeField] private float showCellDuration = 0.25f;
        private readonly List<EditDataSetItemView> _cellList = new();
        public event Action<DataSetItem> OnClickedTrashButton;
        public event Action<DataSetItem> OnChangedItemContent;

        public void Tick()
        {
            if (_cellList.Any(x => x.IsPlayingAnimation))
                LayoutRebuilder.MarkLayoutForRebuild(contentRoot.transform as RectTransform);
        }

        public void Init(Protobuf.DataSet dataSetEntity)
        {
            UpdateList(dataSetEntity.ItemList);
        }

        public void Clear()
        {
            _cellList.ForEach(x => Destroy(x.gameObject));
            _cellList.Clear();
        }

        public void UpdateList(IReadOnlyList<DataSetItem> list)
        {
            var removeCellList =  _cellList
                .Where(cell => list.FirstOrDefault(x => x.Id == cell.DataSetEntityItem.Id) == null)
                .Select(x => x.DataSetEntityItem);
            foreach (var dataSetItem in removeCellList)
            {
                RemoveCell(dataSetItem);
            }

            var addCellList = list
                .Where(x => _cellList.FirstOrDefault(cell => cell.DataSetEntityItem.Id == x.Id) == null);
            foreach (var dataSetItem in addCellList)
            {
                AddCell(dataSetItem);
            }
        }

        private void AddCell(DataSetItem entity)
        {
            var cell = Instantiate(itemViewPrefab, contentRoot.transform);
            cell.Init(entity, showCellDuration);
            cell.OnClickedTrashButton += self =>
            {
                OnClickedTrashButton?.Invoke(self.DataSetEntityItem);
            };
            cell.OnChangedNum += self =>
            {
                OnChangedItemContent?.Invoke(self.DataSetEntityItem);
            };
            cell.OnChangedTitle += self =>
            {
                OnChangedItemContent?.Invoke(self.DataSetEntityItem);
            };

            _cellList.Add(cell);
        }

        private void RemoveCell(DataSetItem entity)
        {
            GetCell(entity).Show(false, showCellDuration, x => { _cellList.Remove(GetCell(x)); });
        }

        private EditDataSetItemView GetCell(DataSetItem itemEntity)
        {
            return _cellList.FirstOrDefault(x => x.DataSetEntityItem.Id == itemEntity.Id);
        }
    }
}