using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeechLib;

namespace GJLocal.HISFC.Components.OpGuide.InDrugStore.Common
{
    public class CallSpeak
    {
        private SpeechLib.SpVoice voice = new SpeechLib.SpVoice();
        /// <summary>
        /// 语音叫号
        /// </summary>
        /// <param name="text"></param>
        public virtual void Speech(string text)
        {
            SpeakChinese(text);
        }

        /// <summary>
        /// 设置中文语音
        /// </summary>
        private void SetChineseVoice()
        {
            voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
            int iRate = voice.Rate;
            voice.Rate = iRate;

        }

        /// <summary>
        /// 朗读中文
        /// </summary>
        /// <param name="text"></param>
        private void SpeakChinese(string text)
        {
            SetChineseVoice();
            voice.Speak(text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        }

        /// <summary>
        /// 设置英文语音
        /// </summary>
        private void SetEnglishVoice()
        {
            voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(1);
        }

        /// <summary>
        /// 朗读英文
        /// </summary>
        /// <param name="text"></param>
        private void SpeakEnglish(string text)
        {
            SetEnglishVoice();
            voice.Speak(text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        }

        #region ICallSpeak 成员

        public void Speech(FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Models.DrugStoreAsign drugStoreAsign)
        {
            string name = "";
            for (int i = 0; i < drugStoreAsign.patientName.Length; i++)
            {
                name += drugStoreAsign.patientName.Substring(i, 1) + "";
            }
            string speakText = "请" + name + "到" + drugStoreAsign.sendTerminalName + "取药";
            this.Speech(speakText);
            this.Speech(speakText);
        }

        #endregion
    }
}
