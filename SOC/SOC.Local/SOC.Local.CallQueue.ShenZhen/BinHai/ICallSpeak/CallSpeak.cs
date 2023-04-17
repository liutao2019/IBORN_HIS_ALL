using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeechLib;

namespace FS.SOC.Local.CallQueue.ShenZhen.BinHai.ICallSpeak
{
    public class CallSpeak : FS.SOC.HISFC.CallQueue.Interface.ICallSpeak
    {
        private SpeechLib.SpVoice voice = new SpeechLib.SpVoice();

        public virtual void Speech(string text)
        {
            //int begin = 0;
            //int length = 0;
            //bool isChinese = false;
            //Regex regex = new Regex("[\u4e00-\u9f5a]", RegexOptions.Compiled);
            //for (int current = 0; current < text.Length; current++)
            //{
            //    if (regex.IsMatch(text[current].ToString()))
            //    {
            //        if (!isChinese)
            //        {
            //            length = current - begin;
            //            SpeakEnglish(text.Substring(begin, length));
            //            begin = current;
            //            isChinese = true;
            //        }
            //    }
            //    else
            //    {
            //        if (isChinese)
            //        {
            //            length = current - begin;
            //            SpeakChinese(text.Substring(begin, length));
            //            begin = current;
            //            isChinese = false;
            //        }
            //    }
            //}
            //length = text.Length - begin;
            //if (isChinese)
            //{
            //    SpeakChinese(text.Substring(begin, length));
            //}
            //else
            //{
            //    SpeakEnglish(text.Substring(begin, length));
            //}
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

        public void Speech(FS.SOC.HISFC.CallQueue.Models.NurseAssign nurseAssign)
        {
            string converSeeNo = "";
            //填充空格
            if (nurseAssign.PatientSeeNO.Length > 0)
            {
                for (int i = 0; i < nurseAssign.PatientSeeNO.Length; i++)
                {
                    converSeeNo += nurseAssign.PatientSeeNO.Substring(i, 1) + " ";
                }
            }
            //string speakText = ",,," + nurseAssign.PatientName + ",,请,,到,,,," + nurseAssign.Room.Name + ",,就,,诊";//+ seeNO + "号,,,,"
            string speakText = "   请 " + converSeeNo + "  号  到    " + nurseAssign.Room.Name + "  就诊";//+ seeNO + "号,,,,"
            this.Speech(speakText);
        }

        #endregion
    }
}
