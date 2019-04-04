using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokBlitzBeta.GamePlay
{
    public class Creation
    {

        //Game Board//
        static int row;
        static int coloumn;
        static int answerbankrow;
        static int answerbankcoloumn;
        public static void InitalizeRowAndColoumn(string[] words) {
            int wordcount = words.Count();


            if (wordcount <= 3)
            {

                row = 1;
                coloumn = 3;
               

            }
            if (wordcount >= 4 && wordcount <= 6)
            {
                row = 2;
                coloumn = 3;
             
            }
            if (wordcount >= 7 && wordcount <= 9)
            {
                row = 3;
                coloumn = 3;
             

            }


            if (wordcount >= 10 && wordcount <= 12)
            {
                row = 4;
                coloumn = 3;
             

            }
            if (wordcount >= 13 && wordcount <= 15)
            {
                row = 5;
                coloumn = 3;
               

            }
            if (wordcount >= 16 && wordcount <= 18)
            {
                row = 6;
                coloumn = 3;
               
            }
            if (wordcount >= 19 && wordcount <= 21)
            {
                row = 7;
                coloumn = 3;
              

            }

            if (wordcount >= 22 && wordcount <= 24)
            {
                row = 8;
                coloumn = 3;
             

            }
            if (wordcount >= 25 && wordcount <= 28)
            {
                row = 7;
                coloumn = 4;
              
            }
            if (wordcount >= 29 && wordcount <= 32)
            {
                row = 8;
                coloumn = 4;

            }
            if (wordcount > 32 && wordcount < 36)
            {
                row = 9;
                coloumn = 4;

            }
            SetRow();
            SetColoumn();

        }

        public static int SetRow() {
            return row;
        }
        public static int SetColoumn() {
            return coloumn;
        }
        //======//

        public static void AnswerBankLoader() {
            int difficulty = Flow.DifficultyIdentifier();
            int wordlenght = Game.SetFirstWord().Length;
            if (!Flow.SetPlayerMode() && !Flow.GameInCategory())
            {
                switch (difficulty)
                {
                    case 1: answerbankcoloumn = 6; answerbankrow = 1; break;
                    case 2: answerbankcoloumn = 9; answerbankrow = 1; break;
                    case 3: answerbankcoloumn = 6; answerbankrow = 2; break;
                    case 4: answerbankcoloumn = 9; answerbankrow = 2; break;
                    default: break;
                }
            }
            else {
                switch (wordlenght) {
                    case 3: answerbankcoloumn = 6; answerbankrow = 1; break;
                    case 4: answerbankcoloumn = 6; answerbankrow = 1; break;
                    case 5: answerbankcoloumn = 9; answerbankrow = 1; break;
                    case 6: answerbankcoloumn = 9; answerbankrow = 1; break;
                    case 7: answerbankcoloumn = 6; answerbankrow = 2; break;
                    case 8: answerbankcoloumn = 6; answerbankrow = 2; break;
                    case 9: answerbankcoloumn = 6; answerbankrow = 2; break;
                    case 10: answerbankcoloumn = 9; answerbankrow = 2; break;
                    case 11: answerbankcoloumn = 9; answerbankrow = 2; break;
                    case 12: answerbankcoloumn = 9; answerbankrow = 2; break;
                }

            }
            AnswerBankRow();
            AnswerBankColoumn();
        }
        public static int AnswerBankRow() {
            return answerbankrow;
        }
        public static int AnswerBankColoumn() {
            return answerbankcoloumn;
        }


       

    }
}
