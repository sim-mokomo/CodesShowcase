using MokomoGames.Utilities;
using NUnit.Framework;

namespace MokomoGamesTest.EditorMode.Utilities
{
    namespace TimerTest
    {
        public class StartTest
        {
            [Test]
            public void ポーズ状態が解除されている確認()
            {
                var timer = new Timer(10);
                Assert.IsFalse(timer.IsPausing);
            }
        }

        public class UpdateTest
        {
            [Test]
            public void _1秒経過した後に時間が減っているかどうかを確認()
            {
                var timer = new Timer(20);
                timer.Start();
                timer.Update(1.0f);
                var date = timer.CurrentTime;
                Assert.AreEqual(0, date.Minutes);
                Assert.AreEqual(19, date.Seconds);
            }

            [Test]
            public void ポーズ中は時間経過後も残秒数が減少していないことを確認()
            {
                var timer = new Timer(20);
                timer.Pause(true);
                timer.Update(1.0f);
                var date = timer.CurrentTime;
                Assert.AreEqual(0, date.Minutes);
                Assert.AreEqual(20, date.Seconds);
            }

            [Test]
            public void タイムアップ後は時間が経過しないことを確認()
            {
                var timer = new Timer(0);
                timer.Start();
                timer.Update(1.0f);
                var date = timer.CurrentTime;
                Assert.AreEqual(0, date.Minutes);
                Assert.AreEqual(0, date.Seconds);
            }
        }

        public class ResetTest
        {
            [Test]
            public void 残秒数の設定が元に戻っていることを確認()
            {
                var timer = new Timer(20);
                timer.Start();
                timer.Update(1.0f);
                timer.Reset();
                var date = timer.CurrentTime;
                Assert.AreEqual(0, date.Minutes);
                Assert.AreEqual(20, date.Seconds);
            }
        }
        
        public class CurrentTimeTest
        {
            [Test]
            public void 負数で初期化した場合_0秒に下限が設定されていることを確認()
            {
                var timer = new Timer(-1);
                var date = timer.CurrentTime;
                Assert.AreEqual(0, date.Minutes);
                Assert.AreEqual(0, date.Seconds);
            }
            
            [Test]
            public void _1分未満の秒数が分に変換できていることを確認()
            {
                var timer = new Timer(20);
                var date = timer.CurrentTime;
                Assert.AreEqual(0, date.Minutes);
                Assert.AreEqual(20, date.Seconds);
            }

            [Test]
            public void _1分の秒数が分に変換できていることを確認()
            {
                var timer = new Timer(60);
                var date = timer.CurrentTime;
                Assert.AreEqual(1, date.Minutes);
                Assert.AreEqual(0, date.Seconds);
            }

            [Test]
            public void _1分以上2分未満の秒数が分に変換できていることを確認()
            {
                var timer = new Timer(90);
                var date = timer.CurrentTime;
                Assert.AreEqual(1, date.Minutes);
                Assert.AreEqual(30, date.Seconds);
            }

            [Test]
            public void _0秒が分に変換できていることを確認()
            {
                var timer = new Timer(0);
                var date = timer.CurrentTime;
                Assert.AreEqual(0, date.Minutes);
                Assert.AreEqual(0, date.Seconds);
            }
        }

        public class IsTimedUpTest
        {
            [Test]
            public void 残時間がまだ残っている場合にfalseとなっている事()
            {
                var timer = new Timer(1);
                Assert.IsFalse(timer.IsTimedUp);
            }

            [Test]
            public void 残時間が0以上の場合にtrueとなっていることを確認()
            {
                var timer = new Timer(0);
                Assert.IsTrue(timer.IsTimedUp);
            }
        }

        public class ElapsedSecondEventTest
        {
            [Test]
            public void _1秒経過した時に秒数経過イベントが呼び出されることを確認()
            {
                var calledOnElapsedSecond = false;
                var timer = new Timer(10);
                timer.OnElapsedSecond += () => { calledOnElapsedSecond = true; };
                timer.Start();
                timer.Update(1.0f);
                Assert.IsTrue(calledOnElapsedSecond);
            }

            [Test]
            public void _1秒に満たない場合は秒数経過イベントが呼び出されないことを確認()
            {
                var calledOnElapsedSecondNum = 0;
                var timer = new Timer(10);
                timer.Start();
                timer.OnElapsedSecond += () => { calledOnElapsedSecondNum++; };
                timer.Update(1.0f);
                timer.Update(0.5f);
                timer.Update(0.5f);
                Assert.AreEqual(2, calledOnElapsedSecondNum);
            }
        }

        public class CompletedEventTest
        {
            [Test]
            public void 残時間が0となった場合にイベントが呼ばれることを確認()
            {
                var calledCompletedEvent = false;
                var timer = new Timer(1);
                timer.OnCompleted += () => { calledCompletedEvent = true; };
                timer.Start();
                timer.Update(1.0f);
                Assert.IsTrue(calledCompletedEvent);
            }

            [Test]
            public void タイマー完了後にタイマーを更新しても複数回完了イベントが実行されないことを確認()
            {
                var calledCompletedEventNum = 0;
                var timer = new Timer(1);
                timer.OnCompleted += () => { calledCompletedEventNum++; };
                timer.Start();
                timer.Update(1.0f);
                timer.Update(1.0f);

                Assert.AreEqual(1, calledCompletedEventNum);
            }
        }
    }
}
