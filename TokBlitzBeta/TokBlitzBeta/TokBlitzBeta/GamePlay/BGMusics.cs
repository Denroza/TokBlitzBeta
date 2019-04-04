using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.SimpleAudioPlayer;
using TokBlitzBeta.GamePlay;
namespace TokBlitzBeta.GamePlay
{
    public class BGMusics
    {
        static ISimpleAudioPlayer MenuBGM { get; set; }
        static ISimpleAudioPlayer LoadingSound { get; set; }
        static ISimpleAudioPlayer WrongSound { get; set; }
        static ISimpleAudioPlayer CorrectSound { get; set; }
        static ISimpleAudioPlayer InGameBGM { get; set; }
        static ISimpleAudioPlayer TimeUpSound { get; set; }
        static ISimpleAudioPlayer ClickingSound { get; set; }
        static ISimpleAudioPlayer LetterSound { get; set; }
        static ISimpleAudioPlayer StrikeSound { get; set; }

        static bool IsMusicPlaying = true;
        static bool IsSoundPlaying = true;
        public static void LoadGameSounds()
        {
            MenuBGM = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            InGameBGM = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            LoadingSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            ClickingSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            LetterSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            CorrectSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            TimeUpSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            StrikeSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            WrongSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

            MenuBGM.Load("sounds/Main Menu (Tok Blitz).wav");
            MenuBGM.Loop = true;
            InGameBGM.Load("sounds/Game music (Tok Blitz).wav");
            InGameBGM.Loop = true;

            LoadingSound.Load("sounds/loading screen (Tok Blitz).wav");
            ClickingSound.Load("sounds/dropsound.wav");

            LetterSound.Load("sounds/resetsound.wav");
            CorrectSound.Load("sounds/correctsound.mp3");
            TimeUpSound.Load("sounds/timeup.wav");
            StrikeSound.Load("sounds/thunder shock sound.wav");
            WrongSound.Load("sounds/wrongsound.wav");
            Menu_BGM();
            InGame_BGM();

            //Effects
            Loading_Sound();
            Wrong_Sound();
            Correct_Sound();
            TimeUp_Sound();
            Clicking_Sound();
            Letter_Sound();
            Strike_Sound();

            DisabledBackgroundMusic = false;
            DisabledSoundEffect = false;
        }
        public static ISimpleAudioPlayer Menu_BGM()
        {
            if (!BackgroundMusicSet())
            {
                MenuBGM.Stop();
            }

            return MenuBGM;
        }

        public static ISimpleAudioPlayer Loading_Sound()
        {
            if (!SoundEffectSet())
            {
                LoadingSound.Stop();
            }

            return LoadingSound;
        }
        public static ISimpleAudioPlayer Wrong_Sound()
        {

            if (!SoundEffectSet())
            {
                WrongSound.Stop();
            }

            return WrongSound;
        }
        public static ISimpleAudioPlayer Correct_Sound()
        {

            if (!SoundEffectSet())
            {
                CorrectSound.Stop();
            }

            return CorrectSound;
        }
        public static ISimpleAudioPlayer InGame_BGM()
        {

            if (!BackgroundMusicSet())
            {
                InGameBGM.Stop();
            }

            return InGameBGM;
        }
        public static ISimpleAudioPlayer TimeUp_Sound()
        {

            if (!SoundEffectSet())
            {
                TimeUpSound.Stop();
            }

            return TimeUpSound;
        }
        public static ISimpleAudioPlayer Clicking_Sound()
        {

            if (!SoundEffectSet())
            {
                ClickingSound.Stop();
            }

            return ClickingSound;
        }
        public static ISimpleAudioPlayer Letter_Sound()
        {

            if (!SoundEffectSet())
            {
                LetterSound.Stop();
            }

            return LetterSound;
        }
        public static ISimpleAudioPlayer Strike_Sound()
        {
            if (!SoundEffectSet())
            {
                StrikeSound.Stop();
            }

            return StrikeSound;
        }

        public static bool DisabledBackgroundMusic
        {
            get; set;
        }


        public static bool DisabledSoundEffect
        {
            get; set;
        }

        public static void EnableBackgroundMusic(bool _play)
        {
            IsMusicPlaying = _play;
            BackgroundMusicSet();
        }

        public static bool BackgroundMusicSet()
        {
            return IsMusicPlaying;
        }


        public static void EnableSoundEffect(bool _play)
        {
            IsSoundPlaying = _play;
            SoundEffectSet();
        }

        public static bool SoundEffectSet()
        {
            return IsSoundPlaying;
        }

        public static void BackgroundEffects()
        {
            if (BackgroundMusicSet())
            {
                MenuBGM = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                InGameBGM = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

                MenuBGM.Load("sounds/Main Menu (Tok Blitz).wav");
                MenuBGM.Loop = true;
                InGameBGM.Load("sounds/Game music (Tok Blitz).wav");
                InGameBGM.Loop = true;
            }
            else
            {

                MenuBGM.Dispose();

                InGameBGM.Dispose();

            }
            Menu_BGM();
            InGame_BGM();
        }

        public static void SoundEffects()
        {
            if (SoundEffectSet())
            {
                LoadingSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                ClickingSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                LetterSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                CorrectSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                TimeUpSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                StrikeSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                WrongSound = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

                ClickingSound.Load("sounds/dropsound.wav");
                LetterSound.Load("sounds/resetsound.wav");
                CorrectSound.Load("sounds/correctsound.mp3");
                TimeUpSound.Load("sounds/timeup.wav");
                StrikeSound.Load("sounds/thunder shock sound.wav");
                WrongSound.Load("sounds/wrongsound.wav");
            }
            else
            {
                ClickingSound.Dispose();
                LetterSound.Dispose();
                CorrectSound.Dispose();
                TimeUpSound.Dispose();
                StrikeSound.Dispose();
                WrongSound.Dispose();
            }
            Loading_Sound();
            Wrong_Sound();
            Correct_Sound();
            TimeUp_Sound();
            Clicking_Sound();
            Letter_Sound();
            Strike_Sound();
        }
    }
}
