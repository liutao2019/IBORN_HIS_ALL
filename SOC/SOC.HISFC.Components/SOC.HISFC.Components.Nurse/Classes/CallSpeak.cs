using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeechLib;

namespace FS.SOC.HISFC.Components.Nurse.Classes
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

        public void Speech(FS.SOC.HISFC.CallQueue.Models.NurseAssign assignObj)
        {
            string callName = "";
            for (int len = 0; len < assignObj.PatientName.Length; len++)
            {
                callName += assignObj.PatientName.Substring(len, 1) + "";
            }
            //callName = assignObj.PatientName;

            //string speakText = "请" + assignObj.PatientSeeNO + "号 " + callName + " 到" + assignObj.Room.Name + "就诊！";
            string speakText = "请" + assignObj.PatientSeeNO + "号" + callName + "到" + assignObj.Room.Name + "就诊！";

            this.Speech(speakText);
            this.Speech(speakText);
            this.Speech(speakText);
        }
    }
}
