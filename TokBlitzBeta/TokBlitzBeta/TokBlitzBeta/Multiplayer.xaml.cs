
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TokBlitzBeta.GamePlay;
using Microsoft.AspNetCore.SignalR.Client;
using System.Timers;

namespace TokBlitzBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Multiplayer : ContentPage
    {
        Timer GameTimer;
        bool isPlayer1;
        bool GameIsTimed = true, TimerIsRunning, TimerP1IsVisible, TimerP2IsVisible, onlyAllowRoomId = false;
        int roundNumP1 = 0, guessNumP1 = 0, roundNumP2 = 0, guessNumP2 = 0, timeCount = 60;

        GamePlayer me = new GamePlayer();
        GameInfo gameInfo = new GameInfo();
        HubConnection connection;
        public Multiplayer()
        {
            InitializeComponent();
            SetTimer();
            txtUserName.Text = UserData.GetUserLabel();
            txtUserName.IsEnabled = false;
        }
        private async void BtnSetup_Clicked(object sender, EventArgs e)
        {
            btnSetup.IsEnabled = false;
            await SignalRService.MultiplayerSetup(me);
            //After this you just wait for either for "waiting" or "newgame" events to run
        }

        private async void BtnStopWaiting_Clicked(object sender, EventArgs e)
        {
            lblStatus.Text = "Stopping waiting...";
            waitingSpinner.IsVisible = true;
            waitingSpinner.IsRunning = true;

            //Clear the waiting list
            await SignalRService.MultiplayerStopWaiting(me);
            lblStatus.Text = "Click below to start a game:";

            waitingSpinner.IsVisible = false;
            waitingSpinner.IsRunning = false;
            btnStopWaiting.IsEnabled = false;
            btnStopWaiting.IsVisible = false;
            btnSetup.IsEnabled = true;
            btnSetup.IsVisible = true;
        }

        private async void btnConnect_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
                return;

            try
            {
                me.UserName = txtUserName.Text?.Trim();
                me.UserId = Guid.NewGuid().ToString();

                spinner.IsVisible = true;
                spinner.IsRunning = true;
                lblMessages.Text = "Getting connection info...";

                SignalRConnectionInfo connectionInfo = await SignalRService.GetSignalRConnectionInfo();
                var url = connectionInfo.Url;
                lblMessages.Text = $"Connecting...";

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

                //Waiting for other player
                connection.On<GamePlayer>("waiting", (player) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (player.UserId == me.UserId)
                        {
                            lblStatus.Text = "Waiting for players...";
                            waitingSpinner.IsVisible = true;
                            waitingSpinner.IsRunning = true;

                            btnSetup.IsVisible = false;
                            btnStopWaiting.IsEnabled = true;
                            btnStopWaiting.IsVisible = true;
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
                        multiplayerGameContent.IsVisible = true;
                        lblStatus.Text = $"Game setup successful, you are player {currentPlayer.PlayerNumber}";
                        if (currentPlayer.PlayerNumber == 1)
                        {
                            isPlayer1 = true;
                            btnStartTimer.IsEnabled = true;
                        }
                        else
                        {
                            isPlayer1 = false;
                            btnStartTimer.IsEnabled = false;
                        }

                        //Fill label with game content
                        lblGameContent.Text = $"Room Id: {info.RoomId}{Environment.NewLine}Player 1: {info.Players[0].UserName}{Environment.NewLine}Player 2: {info.Players[1].UserName}{Environment.NewLine}Toks:{Environment.NewLine}";

                        for (int i = 0; i < info.Toks.Count; ++i)
                            lblGameContent.Text += $"- {info.Toks[i].PrimaryFieldText}{Environment.NewLine}";

                        btnStopWaiting.IsEnabled = false;
                        btnStopWaiting.IsVisible = false;
                        waitingSpinner.IsVisible = false;
                        waitingSpinner.IsRunning = true;
                    });
                });

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
                            lblMessages.Text += Environment.NewLine + newMessage;
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
                            lblRoundP1.Text = (num).ToString();
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
                            lblGuessP1.Text = (num).ToString();
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
                            lblRoundP2.Text = (num).ToString();
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
                            lblGuessP2.Text = (num).ToString();
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
                            lblTimer.Text = (timeCount).ToString();
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
                                btnStartTimer.Text = "Start Timer";
                            }
                            else
                            {
                                if (isPlayer1)
                                    GameTimer.Start();

                                TimerIsRunning = true;
                                btnStartTimer.Text = "Stop Timer";
                            }
                        }
                    });
                });

                await connection.StartAsync();
                lblMessages.Text = $"Connected.{Environment.NewLine}Id: {me.UserId}{Environment.NewLine}Name: {me.UserName}";
                //btnConnect.Text = connectionInfo.AccessToken;
                btnConnect.IsEnabled = false;

                gridRoundGuess.IsVisible = true;
                btnGuessP1.IsEnabled = true;
                btnGuessP2.IsEnabled = true;
                btnRoundP1.IsEnabled = true;
                btnRoundP2.IsEnabled = true;

                lblStatus.Text = "Click below to start a game:";
                btnSetup.IsEnabled = true;

                spinner.IsVisible = false;
                spinner.IsEnabled = false;
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }

        private void LimitToRoomId_Toggled(object sender, ToggledEventArgs e)
        {
            //Turn off if on
            if (onlyAllowRoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
            {
                onlyAllowRoomId = false;
            }
            //Turn on if off
            else if (!onlyAllowRoomId && !string.IsNullOrEmpty(gameInfo.RoomId))
            {
                onlyAllowRoomId = true;

            }
        }

        #region Rounds, Guesses, and TImer
        private async void btnRoundP1_Clicked(object sender, EventArgs e)
        {
            GameMessage newMessage = new GameMessage() { Type = "round_p1", RoomId = gameInfo.RoomId };
            await SignalRService.SendMessage(newMessage);
        }

        private async void btnGuessP1_Clicked(object sender, EventArgs e)
        {
            GameMessage newMessage = new GameMessage() { Type = "guess_p1", RoomId = gameInfo.RoomId };
            await SignalRService.SendMessage(newMessage);
        }

        private async void btnRoundP2_Clicked(object sender, EventArgs e)
        {
            GameMessage newMessage = new GameMessage() { Type = "round_p2", RoomId = gameInfo.RoomId };
            await SignalRService.SendMessage(newMessage);
        }

        private async void btnGuessP2_Clicked(object sender, EventArgs e)
        {
            GameMessage newMessage = new GameMessage() { Type = "guess_p2", RoomId = gameInfo.RoomId };
            await SignalRService.SendMessage(newMessage);
        }

        private async void btnStartTimer_Clicked(object sender, EventArgs e)
        {
            GameMessage newMessage = new GameMessage() { Type = "startstoptimer", RoomId = gameInfo.RoomId, Content = TimerIsRunning };
            await SignalRService.SendMessage(newMessage);
        }

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