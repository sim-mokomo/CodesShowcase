using System;

namespace MokomoGames.Counter
{
    public class Counter
    {
        private readonly int _endCounter;
        private int _currentCount;
        public event Action OnEnd;

        public Counter(int startCount, int endCounter)
        {
            _currentCount = startCount;
            _endCounter = endCounter;
        }

        public void Increase(int diff)
        {
            _currentCount += diff;
            if (_currentCount == _endCounter)
            {
                OnEnd?.Invoke();
            }
        }
    }
}