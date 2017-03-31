﻿/// Sharp 80 (c) Matthew Hamilton
/// Licensed Under GPL v3. See license.txt for details.

using System;

namespace Sharp80.TRS80
{
    public interface IFloppy
    {
        bool DoubleSided { get; }
        bool Changed { get; }
        byte NumTracks { get; }
        byte SectorCount(byte TrackNum, bool SideOne);
        string FilePath { get; set; }
        string FileDisplayName { get; }
        bool WriteProtected { get; set; }
        bool Formatted { get; }
        SectorDescriptor GetSectorDescriptor(byte TrackNum, bool SideOne, byte SectorIndex);
    }
}
