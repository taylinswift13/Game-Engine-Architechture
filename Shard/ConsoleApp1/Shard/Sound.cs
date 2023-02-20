/*
*
*   This class intentionally left blank.  
*   @author Michael Heron
*   @version 1.0
*   
*/

//Sound manager
//Should be able to loop sound
//Should be able to play one shot
//Should be able to handle different sounds at the same time
//Should be able to stop a sound
//Should be able to adjust the volume of the sound

using SDL2;
using System;

namespace Shard
{
    public enum SoundStatus
    {
        Stopped,
        Playing,
        Paused
    }

    abstract public class Sound
    {
        abstract public void initializeAudioSystem();
        abstract public int playSound(string file, float volume, bool loop = false);
        abstract public SoundStatus getSoundStatus(int channel);
    }


}
