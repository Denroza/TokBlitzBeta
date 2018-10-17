using System;
using System.Collections.Generic;
using System.Text;
using TokBlitzBeta.Model;
namespace TokBlitzBeta.MainViewModel
{
    public class MainViewModel
    {
        public  List<Qoutes> Qoutes = new List<Qoutes>();
        
      
 
        public  MainViewModel() {
            Qoutes.Add(new Qoutes { id = 1, TheQoute = "Good vibes only", WordCount = 3, Category="Positivity", QouteSource="" });
            Qoutes.Add(new Qoutes { id = 2,TheQoute = "Be Happy and Smile", WordCount = 4, Category ="Positivity",QouteSource="" });
            Qoutes.Add(new Qoutes { id = 3,TheQoute = "Don't Give Up", WordCount = 3, Category="Positivity", QouteSource=""});
            Qoutes.Add(new Qoutes { id = 4,TheQoute = "Become Stronger than yesterday", WordCount = 4, Category="Encouragement",QouteSource="" });
            Qoutes.Add(new Qoutes { id = 5,TheQoute = "Decide, Commit, Succeed", WordCount = 3, Category="Encouragement", QouteSource="" });
            Qoutes.Add(new Qoutes { id = 6, TheQoute = "This world offers no guarantees for the future.", WordCount = 8, Category="Reality", QouteSource="" });
            Qoutes.Add(new Qoutes { id = 7, TheQoute = "Be Happy, Be Right, Be You", WordCount = 6, Category = "Positivity", QouteSource = "" });
            Qoutes.Add(new Qoutes { id = 8, TheQoute = "There's more to a real man than just his good looks!", WordCount = 11, Category = "Reality", QouteSource = "" });
            Qoutes.Add(new Qoutes { id = 9, TheQoute = "Stop pouting! You learned something valuable. Remember the lesson, not the disappointment.", WordCount = 11, Category = "Encouragement", QouteSource = "" });
            Qoutes.Add(new Qoutes { id = 10, TheQoute = "Being able to realize your own fault is a virtue.", WordCount = 10, Category = "Virtue", QouteSource = "" });
            Qoutes.Add(new Qoutes { id = 11, TheQoute = "If I'm not at my limit, It means I'm not trying as hard as I can!", WordCount = 16, Category = "Encouragement", QouteSource = "" });
            Qoutes.Add(new Qoutes { id = 12, TheQoute = "Dividing an impossibly large task into smaller solvable problems, is a programmer's job.", WordCount = 13, Category = "Mindset", QouteSource = "Akasaka Ryuunosukes" });
            Qoutes.Add(new Qoutes { id = 13, TheQoute = "It's a programmer's job to make the most of limited resources to turn an impractical idea into reality.", WordCount = 18, Category = "Mindset", QouteSource = "Akasaka Ryuunosukes" });
            Qoutes.Add(new Qoutes { id = 14, TheQoute = "Trying to improve by learning from others that is what calls friendship", WordCount=12, Category = "Virtue", QouteSource = "" });
            Qoutes.Add(new Qoutes { id = 15, TheQoute = "It's not bad to dream. But you also have to consider what's realistic", WordCount = 13, Category = "Reality", QouteSource = "" });
            Qoutes.Add(new Qoutes { id = 16, TheQoute = "You are calm and intelligent, but youth can invite disaster. Do not fight alone.", WordCount = 14, Category = "Encouragement", QouteSource = "" });
            Qoutes.Add(new Qoutes { id = 17, TheQoute = "Live on and endure the shadows! There is no light for those who do not know darkness.", WordCount = 17,Category = "Encouragement", QouteSource = "" });
            Qoutes.Add(new Qoutes { id = 18, TheQoute = "People need to be told they're worthy of being alive by someone else or they can't go on.", WordCount = 18,Category = "Reality", QouteSource = "" });
            Qoutes.Add(new Qoutes { id = 19, TheQoute = "We hold these truths to be self-evident, that all men are created equal, that they are endowed by their Creator with certain unalienable Rights, that among these are Life, Liberty and the pursuit of Happiness. ", WordCount = 34, Category = "Bible", QouteSource = "" });
            Qoutes.Add(new Qoutes { id = 20, TheQoute = "Freedom is something that you need to actively acquire. It's not something that's given with no strings attached. To be free means to take responsibility, and to prepare yourself for what's to come.", WordCount = 33, Category = "Virtue", QouteSource = "" });
            Qoutes.Add(new Qoutes { id = 21, TheQoute = "Evil expects evil from others.", WordCount = 5, Category = "Reality", QouteSource = "" });
            Qoutes.Add(new Qoutes { id = 22, TheQoute = "The ones who accomplish something are the fools who keep pressing onward. The ones who accomplish nothing are the wise who cease advancing.", WordCount = 23, Category = "Virtue", QouteSource = "" });
            Qoutes.Add(new Qoutes { id = 23, TheQoute = "Just walk the path you believe in. And don't forget, you're the main character of your own life story!", WordCount = 19, Category = "Encouragement", QouteSource = "" });
            Qoutes.Add(new Qoutes { id = 24, TheQoute = "Everyone will always question your ideals. That's simply reality.", WordCount = 9, Category = "Reality", QouteSource = "Keima Katsuragi" });
            Qoutes.Add(new Qoutes { id = 25, TheQoute = "The world doesn't get to decide whether my life is boring, fun, or ordinary, because that's my decision to make. As long as I have the will, nothing is impossible!", WordCount = 30, Category = "Positivity", QouteSource = "Keima Katsuragi" });
            Qoutes.Add(new Qoutes { id = 26, TheQoute = "If everyone were perfect, there would be no need to look out for others. Sympathy or love is needed because people are imperfect. A perfect human cannot love anyone.", WordCount = 29 });
            Qoutes.Add(new Qoutes { id = 27, TheQoute = "You keep imposing your ideals on them. Don't worry about other people. You only need to do what you think is right.", WordCount = 22, Category = "Encouragement", QouteSource = "Keima katsuragi" });
            Qoutes.Add(new Qoutes { id = 28, TheQoute = "True, I've given up on the real world. However, I haven't given up on myself.", WordCount = 15, Category = "Positivity", QouteSource = "Keima Katsuragi" });
            Qoutes.Add(new Qoutes { id = 29, TheQoute = "The game design for life is flawed.", WordCount = 7, Category = "Rant", QouteSource = "Keima Katsuragi" });
            Qoutes.Add(new Qoutes { id = 30, TheQoute = "Smiles are what connect people! It allows them to communicate through their souls! Souls that are connected. Will never lose to power that only relies on control!", WordCount = 27, Category = "Encouragement", QouteSource = "" });












        }

    }
}
