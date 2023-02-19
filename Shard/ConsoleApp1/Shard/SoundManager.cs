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
        public override void playSound(string file, float volume)
        {
            file = Bootstrap.getAssetManager().getAssetPath(file);

            if (SDL_mixer.Mix_OpenAudio(44100, SDL.AUDIO_S16SYS, 2, 4096) == -1)
            {
                Console.WriteLine("Error: " + SDL.SDL_GetError());
                return;
            }

            IntPtr chunk = SDL_mixer.Mix_LoadWAV(file);

            if (chunk == IntPtr.Zero)
            {
                Console.WriteLine("Error: " + SDL.SDL_GetError());
                return;
            }

            int channel = SDL_mixer.Mix_PlayChannel(-1, chunk, 0);

            if (channel == -1)
            {
                Console.WriteLine("Error: " + SDL.SDL_GetError());
            }
            else
            {
                SDL_mixer.Mix_Volume(channel, (int)(volume * SDL_mixer.MIX_MAX_VOLUME));
            }
        }
    }
}