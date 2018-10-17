using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TokBlitzBeta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SPpage : ContentPage
    {
        Label[,] labeled = new Label[2, 10];
       TapGestureRecognizer[,] gridTap = new TapGestureRecognizer[2,10];
        string[,] vars = new string[2, 10];
        public SPpage ()
		{
			InitializeComponent ();
            background.Source = ImageSource.FromResource("TokBlitzBeta.GameAssets.Pictures.bgSP.png");
     choicesGrid();
         
            
        }




                public void choicesGrid() {
      
                     for (int j = 0; j < 2; j++)
                      {
                           for (int i = 0; i < 10; i++)
                           {
                             vars[j, i] = i.ToString();


                             labeled[j, i] = new Label  {
                                           Text = vars[j,i],
                                        VerticalTextAlignment = TextAlignment.Center,
                                           HorizontalTextAlignment = TextAlignment.Center
                             };
                                gridTap[j, i] = new TapGestureRecognizer();
                                   var getJ = j;
                                 var getI = i;
                                gridTap[j, i].Tapped += (sender, e) =>
                                {
                                    var check = vars[getJ, getI];
                                    if (labeled[getJ, getI].Text.Equals("") || labeled[getJ, getI].Text.Equals(null))
                                    {
                                        labeled[getJ, getI].Text = check;
                                    }
                                    else {

                                        labeled[getJ, getI].Text = "";
                                        }
                             
                                //  DisplayAlert("Value", check, "OK");
                                    };
                                       labeled[j, i].GestureRecognizers.Add(gridTap[j, i]);
                                          choices.Children.Add(labeled[j, i], i, j);
                              }

                     }

                }

            

    }
} 