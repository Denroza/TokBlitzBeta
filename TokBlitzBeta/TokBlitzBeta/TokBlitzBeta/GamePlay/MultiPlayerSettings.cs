using System;
using System.Collections.Generic;
using System.Text;
using TokBlitzBeta.GamePlay;
using Microsoft.AspNetCore.SignalR.Client;
using System.Timers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TokBlitzBeta.GamePlay
{
    public class MultiPlayerSettings
    {
       static Timer GameTimer;
       static bool isPlayer1;
        static bool Yes;
       static bool GameIsTimed = true, TimerIsRunning, TimerP1IsVisible, TimerP2IsVisible, onlyAllowRoomId = false;
       static int roundNumP1 = 0, guessNumP1 = 0, roundNumP2 = 0, guessNumP2 = 0, timeCount = 60;
        static int OtherPoints = 0;
            static string OtherGuess = "Guess: 1", OtherRound="Round: 1";
     static   GamePlayer me = new GamePlayer();
      static GameInfo gameInfo;
        
     static   HubConnection connection;

        static string OtherPlayersName;
        static string OtherPlayersPic;
        public static void GetOtherUserIngameData(int points, string guess, string round) {
            OtherPoints = points;
            OtherGuess = guess;
            OtherRound = round;
            SetOtherPoint();
            SetOtherGuess();
            SetOtherRound();
        }

        public static int SetOtherPoint() {
            return OtherPoints;
        }
        public static string SetOtherGuess()
        {
            return OtherGuess;
        }
        public static string SetOtherRound()
        {
            return OtherRound;
        }
        public static void GetGameInfo(GameInfo game) {
            gameInfo = game;
            ReadGameInfo();
        }
        public static GameInfo ReadGameInfo() {
            return gameInfo;
        }
        public static void OtherPlayersData(string Otherplayer, string Otherpic) {
            OtherPlayersPic = Otherpic;
            OtherPlayersName = Otherplayer;
            ShowOtherPlayerName();
            ShowOtherPlayerPicture();
        }
        public static string ShowOtherPlayerName() {
            return OtherPlayersName;
        }
        public static string ShowOtherPlayerPicture() {
            return OtherPlayersPic;    
        }

        public static void IsThisPlayer1(bool player) {
            Yes = player;
            YupThisIsPlayer1();
        }
        public static bool YupThisIsPlayer1() {
            return Yes;
        }
        public async void SetupGame() {
            await SignalRService.MultiplayerSetup(me);
        }
        public async void StopGameWaiting() {
            await SignalRService.MultiplayerStopWaiting(me);
        }

        #region Connectios
        public async void GameConnect(){
            if (string.IsNullOrEmpty(UserData.GetUserLabel()))
                return;
            try
            {
                me.UserName = UserData.GetUserLabel()?.Trim();
                me.UserId = Guid.NewGuid().ToString();

               // spinner.IsVisible = true;
            //    spinner.IsRunning = true;
            //    lblMessages.Text = "Getting connection info...";

                SignalRConnectionInfo connectionInfo = await SignalRService.GetSignalRConnectionInfo();
                var url = connectionInfo.Url;
             //   lblMessages.Text = $"Connecting...";

                connection = new HubConnectionBuilder()
                .WithUrl(url, options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(connectionInfo.AccessToken);
                })
                .Build();

                //connection.Closed += async (error) =>
                //{
                //    //Optional code: only used for retrying to connect
                //    //await Task.Delay(new Random().Next(0, 5) * 1000);
                //    //await connection.StartAsync();
                //};
                #region Connection
                //Waiting for other player
                connection.On<GamePlayer>("waiting", (player) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (player.UserId == me.UserId)
                        {
                       //     lblStatus.Text = "Waiting for players...";
                      //      waitingSpinner.IsVisible = true;
                  //          waitingSpinner.IsRunning = true;

                  //          btnSetup.IsVisible = false;
                   //         btnStopWaiting.IsEnabled = true;
                 //           btnStopWaiting.IsVisible = true;
                        }
                    });
                });

                //2 player game successfully created (method name MUST be 'newgame'
                connection.On<GameInfo>("newgame", (info) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        gameInfo = info;
                        var currentPlayer = info.Players.Find(x => x.UserId == me.UserId);
                      //  multiplayerGameContent.IsVisible = true;
                    //    lblStatus.Text = $"Game setup successful, you are player {currentPlayer.PlayerNumber}";
                        if (currentPlayer.PlayerNumber == 1)
                        {
                            isPlayer1 = true;
                      ///      btnStartTimer.IsEnabled = true;
                        }
                        else
                        {
                            isPlayer1 = false;
                   //         btnStartTimer.IsEnabled = false;
                        }

                        //Fill label with game content
                        //      lblGameContent.Text = $"Room Id: {info.RoomId}{Environment.NewLine}Player 1: {info.Players[0].UserName}{Environment.NewLine}Player 2: {info.Players[1].UserName}{Environment.NewLine}Toks:{Environment.NewLine}";

                        for (int i = 0; i < info.Toks.Count; ++i) { }
                //            lblGameContent.Text += $"- {info.Toks[i].PrimaryFieldText}{Environment.NewLine}";

             //           btnStopWaiting.IsEnabled = false;
            //            btnStopWaiting.IsVisible = false;
             //           waitingSpinner.IsVisible = false;
            //            waitingSpinner.IsRunning = true;
                    });
                });
            #endregion
                #region Rounds and guesses

                connection.On<GameMessage>("log", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                        if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                            allowMessage = false;

                        if (allowMessage)
                        {
                            var newMessage = $"{message.Player.UserName}: {message.Type}";
                      //      lblMessages.Text += Environment.NewLine + newMessage;
                        }
                    });
                });

                connection.On<GameMessage>("round_p1", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                        if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                            allowMessage = false;

                        if (allowMessage)
                        {
                            var num = ++roundNumP1;
                      //  lblRoundP1.Text = (num).ToString();

                        }
                    });
                });
                connection.On<GameMessage>("guess_p1", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                        if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                            allowMessage = false;

                        if (allowMessage)
                        {
                            var num = ++guessNumP1;
                      //      lblGuessP1.Text = (num).ToString();
                        }
                    });
                });
                connection.On<GameMessage>("round_p2", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                        if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                            allowMessage = false;

                        if (allowMessage)
                        {
                            var num = ++roundNumP2;
                  //          lblRoundP2.Text = (num).ToString();
                        }
                    });
                });
                connection.On<GameMessage>("guess_p2", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                        if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                            allowMessage = false;

                        if (allowMessage)
                        {
                            var num = ++guessNumP2;
                    //        lblGuessP2.Text = (num).ToString();
                        }
                    });
                });
                #endregion

                //2 player "Elapsed" function
                connection.On<GameMessage>("time", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                        if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                            allowMessage = false;

                        if (allowMessage)
                        {
                            var num = (int)message.Content;
                            timeCount += num;
                   //         lblTimer.Text = (timeCount).ToString();
                        }
                    });
                });

                //2 player "Start/Stop" function
                connection.On<GameMessage>("startstoptimer", (message) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        bool allowMessage = true;
                        if (onlyAllowRoomId == true && gameInfo.RoomId != message.RoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
                            allowMessage = false;

                        if (allowMessage)
                        {
                            var timerIsRunning = Convert.ToBoolean(message.Content);

                            if (timerIsRunning)
                            {
                                if (isPlayer1)
                                    GameTimer.Stop();

                                TimerIsRunning = false;
                    //            btnStartTimer.Text = "Start Timer";
                            }
                            else
                            {
                                if (isPlayer1)
                                    GameTimer.Start();

                                TimerIsRunning = true;
                     //           btnStartTimer.Text = "Stop Timer";
                            }
                        }
                    });
                });

                await connection.StartAsync();
             //   lblMessages.Text = $"Connected.{Environment.NewLine}Id: {me.UserId}{Environment.NewLine}Name: {me.UserName}";
                //btnConnect.Text = connectionInfo.AccessToken;
           //     btnConnect.IsEnabled = false;

          //      gridRoundGuess.IsVisible = true;
         //       btnGuessP1.IsEnabled = true;
         //       btnGuessP2.IsEnabled = true;
      //          btnRoundP1.IsEnabled = true;
       //         btnRoundP2.IsEnabled = true;

    //            lblStatus.Text = "Click below to start a game:";
     //           btnSetup.IsEnabled = true;

   //             spinner.IsVisible = false;
     //           spinner.IsEnabled = false;
            }
            catch (Exception ex)
            {
        //        lblMessages.Text = ex.Message;
            }
        }
        #endregion

        #region Player1 Pass data
        public static async void Player1Datapass() {
            GameMessage newMessage = new GameMessage() { Type = "round_p1", RoomId = gameInfo.RoomId };
            await SignalRService.SendMessage(newMessage);
            newMessage = new GameMessage() { Type = "guess_p1", RoomId = gameInfo.RoomId };
            await SignalRService.SendMessage(newMessage);
            newMessage = new GameMessage() { Type = "point_p1", RoomId = gameInfo.RoomId };
            await SignalRService.SendMessage(newMessage);
        }
        #endregion
        #region Player2 Pass data
        public static async void Player2Datapass()
        {
            GameMessage newMessage = new GameMessage() { Type = "round_p2", RoomId = gameInfo.RoomId };
            await SignalRService.SendMessage(newMessage);
            newMessage = new GameMessage() { Type = "guess_p2", RoomId = gameInfo.RoomId };
            await SignalRService.SendMessage(newMessage);
            newMessage = new GameMessage() { Type = "point_p2", RoomId = gameInfo.RoomId };
            await SignalRService.SendMessage(newMessage);
        }
        #endregion
        #region Multiplayer Time Settings
        private async void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (isPlayer1)
            {
                if (timeCount > 0)
                {
                    GameMessage newMessage = new GameMessage() { Type = "time", Content = 1 };
                    await SignalRService.SendMessage(newMessage);
                }
                else
                {
                    GameTimer.Stop();
                }
            }
        }

        public void SetTimer()
        {
            if (GameTimer != null)
                GameTimer.Stop();

            if (GameIsTimed)
            {
                TimerP1IsVisible = true;
                TimerP2IsVisible = true;

                GameTimer = new Timer();
                GameTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                GameTimer.Interval = 1000;
            }
            else
            {
                TimerP1IsVisible = false;
                TimerP2IsVisible = false;
            }
        }
        #endregion

    }
}
