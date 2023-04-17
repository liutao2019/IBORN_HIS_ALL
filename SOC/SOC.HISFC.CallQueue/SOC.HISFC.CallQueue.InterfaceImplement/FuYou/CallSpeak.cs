using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neusoft.SOC.HISFC.CallQueue.Interface;

namespace Neusoft.SOC.HISFC.CallQueue.InterfaceImplement.FuYou
{
    public class CallSpeak : Neusoft.SOC.HISFC.CallQueue.Interface.ICallSpeak
    {
        IFLYTTSLib.TTSCtrlClass ttsCtrl = new IFLYTTSLib.TTSCtrlClass();

        public void Speech(Neusoft.SOC.HISFC.CallQueue.Models.NurseAssign nurseAssign)
        {
            string speakText = "请" + nurseAssign.PatientSeeNO + "号；到" + nurseAssign.Room.Name + "就诊"
                                + "；；；请" + nurseAssign.PatientSeeNO + "号；到" + nurseAssign.Room.Name + "就诊";
            this.Speech(speakText);
        }

        public virtual void Speech(string text)
        {
            this.Speak(text);
        }

        public void Speak(string text)
        {
            IFLYTTSLib.enAudioFmt eAudio = IFLYTTSLib.enAudioFmt.eAdfDefault;

            ttsCtrl.AudioDataFmt = eAudio;

            ttsCtrl.Speed = -200; //语速
            ttsCtrl.Pitch = 20; //语调
            ttsCtrl.Volume = 20; //音量 
            ttsCtrl.SyncMod = 0; //1表示同步  0 表示异步

            ttsCtrl.Speak(text, IFLYTTSLib.enSpeakFlags.esfText, 0);
        }
    }
}
