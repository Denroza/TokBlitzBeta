using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
using CocosDenshion;
using Xamarin.Forms;
namespace TokBlitzBeta.GameScenes
{
    public class GameScene : CCScene
    {
        public GameScene(CCGameView scene) : base(scene)
        {
            CCContentManager.SharedContentManager.SearchPaths.Add("Images");
            CCSprite sprite = new CCSprite("starshine.png");
            var layer = new CCLayer();
           
            this.AddLayer(layer);
            layer.AddChild(sprite);

        }

        static GameScene gameScene;
        public static void HandleViewCreated(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            
            if (gameView != null)
            {
                // This sets the game "world" resolution to 100x100:
                gameView.DesignResolution = new CCSizeI(480, 854);
            
                // GameScene is the root of the CocosSharp rendering hierarchy:
                gameScene = new GameScene(gameView);

                // Starts CocosSharp:
                gameView.RunWithScene(gameScene);
            }
        }
    }
}
