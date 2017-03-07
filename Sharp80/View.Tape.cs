﻿using System;
using System.Text;

namespace Sharp80
{
    internal class ViewTape : View
    {
        protected override ViewMode Mode => ViewMode.Tape;

        protected override bool ForceRedraw => true;

        int cursor = 0;

        protected override bool processKey(KeyState Key)
        {
            if (Key.Pressed)
            {
                switch (Key.Key)
                {
                    case KeyCode.P:
                        Computer.TapePlay();
                        break;
                    case KeyCode.L:
                        cursor = ++cursor % 4;
                        switch (cursor)
                        {
                            case 0:
                                Computer.TapeLoad(@"c:\Users\Matthew\Desktop\mcarpet.cas");
                                break;
                            case 1:
                                Computer.TapeLoad(@"c:\Users\Matthew\Desktop\startrek.bas.cas");
                                break;
                            case 2:
                                Computer.TapeLoad(@"c:\Users\Matthew\Desktop\seadragon.cas");
                                break;
                            case 3:
                                Computer.TapeLoad(@"c:\Users\Matthew\Desktop\foo.cas");
                                break;
                        }
                        break;
                    case KeyCode.W:
                        Computer.TapeRewind();
                        break;
                    case KeyCode.R:
                        Computer.TapeRecord();
                        break;
                    case KeyCode.B:
                        Computer.TapeLoadBlank();
                        break;
                    case KeyCode.V:
                        Computer.TapeSave();
                        break;
                    default:
                        return base.processKey(Key);
                }
            }
            return true;
        }

        protected override byte[] GetViewBytes()
        {
            string fileName = Computer.TapeFilePath;

            return PadScreen(Encoding.ASCII.GetBytes(
                Header("Cassette Management") +
                Format() +
                Format("Cassette File: " + (String.IsNullOrWhiteSpace(fileName) ? "{UNTITLED}" : fileName)) +
                Format(string.Format(@"{0:0000.0}  {1:mm\:ss\:ff}", Computer.TapeCounter, Computer.TapeElapsedTime)) +
                Format(string.Format("{0} {1} {2} {3}", stateStr(Computer.TapeStatus), Computer.TapeSpeed, Computer.TapeValue, Computer.TapePulseStatus)) +
                Format() +
                Format("[L] Load") +
                Format("[P] Play") +
                Format("[R] Record") +
                Format("[S] Stop") +
                Format("[B] Load Blank Tape")+
                Format() +
                Format("[W] Rewind") +
                Format("[F] Fast Forward") +
                Format("[V] Save Tape")
                ));
        }
        private string stateStr(TapeStatus Status)
        {
            switch (Status)
            {
                case TapeStatus.Stopped:
                    return "Stopped";
                case TapeStatus.ReadEngaged:
                    return "Play Engaged";
                case TapeStatus.WriteEngaged:
                    return "Record Engaged";
                case TapeStatus.Reading:
                    return "Playing";
                case TapeStatus.Writing:
                    return "Recording";
                case TapeStatus.Waiting:
                    return "Waiting";
                default:
                    return "Error";
            }
        }
    }
}