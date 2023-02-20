/*
*
*   A very simple implementation of a very simple sound system.
*   @author Michael Heron
*   @version 1.0
*   
*/

using SDL2;
using System;
using System.IO;

namespace Shard
{
    public class SoundManager : Sound
    {
        private static bool initialized = false;

        public override void initializeAudioSystem()
        {
            if (!initialized)
            {
                if (SDL_mixer.Mix_OpenAudio(44100, SDL.AUDIO_S16SYS, 2, 4096) == -1)
                {
                    Console.WriteLine("Error: " + SDL.SDL_GetError());
                    return;
                }
                initialized = true;
            }
        }

        public override int playSound(string file, float volume, bool loop = false)
        {
            file = Bootstrap.getAssetManager().getAssetPath(file);

            IntPtr chunk = SDL_mixer.Mix_LoadWAV(file);

            if (chunk == IntPtr.Zero)
            {
                Console.WriteLine("Error: " + SDL.SDL_GetError());
                return -1; // Return an error code
            }

            int channel = SDL_mixer.Mix_PlayChannel(-1, chunk, loop ? -1 : 0);

            if (channel == -1)
            {
                Console.WriteLine("Error: " + SDL.SDL_GetError());
                return -1; // Return an error code
            }
            else
            {
                SDL_mixer.Mix_Volume(channel, (int)(volume * SDL_mixer.MIX_MAX_VOLUME));
                return channel; // Return the channel number
            }
        }

        public override SoundStatus getSoundStatus(int channel)
        {
            if (SDL_mixer.Mix_Playing(channel) == 0)
            {
                return SoundStatus.Stopped;
            }
            else if (SDL_mixer.Mix_Paused(channel) == 1)
            {
                return SoundStatus.Paused;
            }
            else
            {
                return SoundStatus.Playing;
            }
        }

        public override void stopSound(int channel)
        {
            SDL_mixer.Mix_HaltChannel(channel);
        }
    }
}
