using System.Collections.Generic;
using MokomoGames.Environment;
using NUnit.Framework;
using UnityEngine;

namespace MokomoGamesTest.EditorMode.Environment
{
    public class CommandLineArgsParserTest : MonoBehaviour
    {
        [Test]
        public void GetStringOptionTest()
        {
            {// 値が設定されている場合
                var value = "TestPort";
                var args = new List<string>()
                {
                    "-port",
                    value,
                    "",
                };
                
                Assert.AreEqual(
                    value,
                    CommandLineArgsParser.GetStringOption("-port", args));
            }
            
            {// 値が設定されていない場合
                var args = new List<string>()
                {
                    "",
                    "",
                    "-port",
                };
                
                Assert.AreEqual(
                    string.Empty,
                    CommandLineArgsParser.GetStringOption("-port", args));
            }
        }

        [Test]
        public void GetIntOptionTest()
        {
            {// 値が設定されている場合
                var args = new List<string>()
                {
                    "-port",
                    "7777",
                    "",
                };
                
                Assert.AreEqual(
                    7777,
                    CommandLineArgsParser.GetIntOption("-port", args));
            }
            
            {// 値が設定されていない場合
                var args = new List<string>()
                {
                    "",
                    "",
                    "-port",
                };
                
                Assert.AreEqual(
                    0,
                    CommandLineArgsParser.GetIntOption("-port", args));
            }
        }
    }
}
