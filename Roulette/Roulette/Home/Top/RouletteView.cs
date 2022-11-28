using System;
using System.Collections;
using System.Collections.Generic;
using MokomoGames.Extensions;
using Roulette.Graph;
using UnityEngine;

namespace Roulette.Home.Top
{
    public class RouletteView : MonoBehaviour
    {
        [SerializeField] private RectTransform rouletteRoot;
        [SerializeField] private CircleGraphView rouletteBodyView;
        [SerializeField] private float initRotateSpeed;
        [SerializeField] private float startDecelerate = 0.99f;
        [SerializeField] private float decelerate;
        [SerializeField] private FloatRandom randomInitRotateDuration;
        [SerializeField] private float stopRotatingThreshold;
        private float _currentRotateDuration;
        private float _currentRotateSpeed;
        private List<CircleGraphData> _dataList = new();

        private Coroutine rotatingCoroutine;
        public bool IsRotating => _currentRotateDuration > stopRotatingThreshold;

        public event Action<string> OnEnded;

        public void SetupGraph(List<CircleGraphData> graphDataList)
        {
            rouletteRoot.transform.rotation = Quaternion.identity;
            _dataList = graphDataList;
            rouletteBodyView.CreateGraphs(_dataList);
        }

        public void StartRoulette()
        {
            if (IsRotating) StopRoulette();
            _currentRotateSpeed = initRotateSpeed;
            _currentRotateDuration = randomInitRotateDuration.Rand();
            rotatingCoroutine = StartCoroutine(StartRotatingCoroutine());
        }

        private IEnumerator StartRotatingCoroutine()
        {
            while (IsRotating)
            {
                rouletteRoot.transform.Rotate(new Vector3(0f, 0f, _currentRotateSpeed * Time.deltaTime));
                _currentRotateDuration -= Time.deltaTime;
                if (_currentRotateDuration <= startDecelerate) _currentRotateSpeed *= decelerate;
                yield return null;
            }

            int GetRouletteIndex()
            {
                var rouletteRatio = rouletteRoot.transform.rotation.eulerAngles.z / 360.0f;
                var baseSumRatio = 0f;
                for (var i = 0; i < _dataList.Count; i++)
                {
                    var data = _dataList[i];
                    if (rouletteRatio < baseSumRatio + data.Rate) return i;

                    baseSumRatio += data.Rate;
                }

                return 1;
            }

            var lotteryIndex = GetRouletteIndex();
            OnEnded?.Invoke(_dataList[lotteryIndex].Name);
        }

        public void StopRoulette()
        {
            if (rotatingCoroutine != null)
            {
                StopCoroutine(rotatingCoroutine);
                rotatingCoroutine = null;
            }
        }
    }
}