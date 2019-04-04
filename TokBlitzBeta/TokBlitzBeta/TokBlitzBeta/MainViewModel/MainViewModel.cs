using System;
using System.Collections.Generic;
using System.Text;
using TokBlitzBeta.Model;
using TokBlitzBeta.GamePlay;
namespace TokBlitzBeta.MainViewModel
{
    public class MainViewModel
    {

        public List<OnlineQoutes> OnlineQoutes = new List<OnlineQoutes>();
        public  List<Qoutes> Qoutes = new List<Qoutes>();
        public List<Sayings> Sayings = new List<Sayings>();
        public List<GameTok> GameToks = new List<GameTok>();
        public  MainViewModel() {
         
            Qoutes.Add(new Qoutes { id = 1, TheQoute = "Good vibes only", WordCount = 3, Category="Positivity", QouteSource="", MaxChar=5 });
            Qoutes.Add(new Qoutes { id = 2,TheQoute = "Be Happy and Smile", WordCount = 4, Category ="Positivity",QouteSource="", MaxChar = 5 });
            Qoutes.Add(new Qoutes { id = 3,TheQoute = "Don't Give Up", WordCount = 3, Category="Positivity", QouteSource="", MaxChar = 5 });
            Qoutes.Add(new Qoutes { id = 4,TheQoute = "Become Stronger than yesterday", WordCount = 4, Category="Encouragement",QouteSource="", MaxChar = 9 });
            Qoutes.Add(new Qoutes { id = 5,TheQoute = "Decide, Commit, Succeed", WordCount = 3, Category="Encouragement", QouteSource="", MaxChar = 7 });
            Qoutes.Add(new Qoutes { id = 6, TheQoute = "This world offers no guarantees for the future.", WordCount = 8, Category="Reality", QouteSource="", MaxChar = 10 });
            Qoutes.Add(new Qoutes { id = 7, TheQoute = "Be Happy, Be Right, Be You", WordCount = 6, Category = "Positivity", QouteSource = "", MaxChar = 6 });
            Qoutes.Add(new Qoutes { id = 8, TheQoute = "There's more to a real man than just his good looks!", WordCount = 11, Category = "Reality", QouteSource = "", MaxChar = 7 });
            Qoutes.Add(new Qoutes { id = 9, TheQoute = "Stop pouting! You learned something valuable. Remember the lesson, not the disappointment.", WordCount = 11, Category = "Encouragement", QouteSource = "", MaxChar = 15 });
            Qoutes.Add(new Qoutes { id = 10, TheQoute = "Being able to realize your own fault is a virtue.", WordCount = 10, Category = "Virtue", QouteSource = "", MaxChar = 7 });
            Qoutes.Add(new Qoutes { id = 11, TheQoute = "If I'm not at my limit, It means I'm not trying as hard as I can!", WordCount = 16, Category = "Encouragement", QouteSource = "", MaxChar = 6 });
            Qoutes.Add(new Qoutes { id = 12, TheQoute = "Dividing an impossibly large task into smaller solvable problems, is a programmer's job.", WordCount = 13, Category = "Mindset", QouteSource = "Akasaka Ryuunosukes", MaxChar = 12 });
            Qoutes.Add(new Qoutes { id = 13, TheQoute = "It's a programmer's job to make the most of limited resources to turn an impractical idea into reality.", WordCount = 18, Category = "Mindset", QouteSource = "Akasaka Ryuunosukes", MaxChar = 12 });
            Qoutes.Add(new Qoutes { id = 14, TheQoute = "Trying to improve by learning from others that is what calls friendship", WordCount=12, Category = "Virtue", QouteSource = "", MaxChar = 10 });
            Qoutes.Add(new Qoutes { id = 15, TheQoute = "It's not bad to dream. But you also have to consider what's realistic", WordCount = 13, Category = "Reality", QouteSource = "", MaxChar = 9 });
            Qoutes.Add(new Qoutes { id = 16, TheQoute = "You are calm and intelligent, but youth can invite disaster. Do not fight alone.", WordCount = 14, Category = "Encouragement", QouteSource = "", MaxChar = 12 });
            Qoutes.Add(new Qoutes { id = 17, TheQoute = "Live on and endure the shadows! There is no light for those who do not know darkness.", WordCount = 17,Category = "Encouragement", QouteSource = "", MaxChar = 9 });
            Qoutes.Add(new Qoutes { id = 18, TheQoute = "People need to be told they're worthy of being alive by someone else or they can't go on.", WordCount = 18,Category = "Reality", QouteSource = "", MaxChar = 7 });
            Qoutes.Add(new Qoutes { id = 19, TheQoute = "We hold these truths to be self-evident, that all men are created equal, that they are endowed by their Creator with certain unalienable Rights, that among these are Life, Liberty and the pursuit of Happiness.", WordCount = 34, Category = "Bible", QouteSource = "", MaxChar = 13 });
            Qoutes.Add(new Qoutes { id = 20, TheQoute = "Freedom is something that you need to actively acquire. It's not something that's given with no strings attached. To be free means to take responsibility, and to prepare yourself for what's to come.", WordCount = 33, Category = "Virtue", QouteSource = "", MaxChar = 15 });
            Qoutes.Add(new Qoutes { id = 21, TheQoute = "The ones who accomplish something are the fools who keep pressing onward. The ones who accomplish nothing are the wise who cease advancing.", WordCount = 23, Category = "Virtue", QouteSource = "", MaxChar = 10 });
            Qoutes.Add(new Qoutes { id = 22, TheQoute = "Just walk the path you believe in. And don't forget, you're the main character of your own life story!", WordCount = 19, Category = "Encouragement", QouteSource = "", MaxChar = 9 });
            Qoutes.Add(new Qoutes { id = 23, TheQoute = "Everyone will always question your ideals. That's simply reality.", WordCount = 9, Category = "Reality", QouteSource = "Keima Katsuragi", MaxChar = 8 });
            Qoutes.Add(new Qoutes { id = 24, TheQoute = "The world doesn't get to decide whether my life is boring, fun, or ordinary, because that's my decision to make. As long as I have the will, nothing is impossible!", WordCount = 30, Category = "Positivity", QouteSource = "Keima Katsuragi", MaxChar = 11 });
            Qoutes.Add(new Qoutes { id = 25, TheQoute = "If everyone were perfect, there would be no need to look out for others. Sympathy or love is needed because people are imperfect. A perfect human cannot love anyone.", WordCount = 29, Category = "Positivity", QouteSource = "Keima Katsuragi", MaxChar = 10 });
            Qoutes.Add(new Qoutes { id = 26, TheQoute = "You keep imposing your ideals on them. Don't worry about other people. You only need to do what you think is right.", WordCount = 22, Category = "Encouragement", QouteSource = "Keima katsuragi", MaxChar = 8 });
            Qoutes.Add(new Qoutes { id = 27, TheQoute = "True, I've given up on the real world. However, I haven't given up on myself.", WordCount = 15, Category = "Positivity", QouteSource = "Keima Katsuragi", MaxChar = 8 });
            Qoutes.Add(new Qoutes { id = 28, TheQoute = "The game design for life is flawed.", WordCount = 7, Category = "Rant", QouteSource = "Keima Katsuragi", MaxChar = 7 });
            Qoutes.Add(new Qoutes { id = 29, TheQoute = "Smiles are what connect people! It allows them to communicate through their souls! Souls that are connected. Will never lose to power that only relies on control!", WordCount = 27, Category = "Encouragement", QouteSource = "", MaxChar = 11 });
            Qoutes.Add(new Qoutes { id = 30, TheQoute = "The LORD is my shepherd, I lack nothing.", WordCount = 8, Category = "Bible", QouteSource = "Psalm 23:1", MaxChar = 9 });
            Qoutes.Add(new Qoutes { id = 31, TheQoute = "Anxiety weighs down the heart, but a kind word cheers it up.", WordCount = 12, Category = "Bible", QouteSource = "Proverbs 12:25", MaxChar = 7 });
            Qoutes.Add(new Qoutes { id = 32, TheQoute = "Cast all your anxiety on him because he cares for you.", WordCount = 11, Category = "Bible", QouteSource = "Peter 5:7", MaxChar = 7 });
            Qoutes.Add(new Qoutes { id = 33, TheQoute = "When I am afraid, I put my trust in you.", WordCount = 10, Category = "Bible", QouteSource = "Psalms 56:3", MaxChar = 7 });
            Qoutes.Add(new Qoutes { id = 34, TheQoute = "When they saw the star, they were overjoyed.", WordCount = 8, Category = "Bible", QouteSource = "Matthew 2:10", MaxChar = 10});
            Qoutes.Add(new Qoutes { id = 35, TheQoute = "For no word from God will ever fail.", WordCount = 8, Category = "Bible", QouteSource = "Luke 1:37", MaxChar = 5 });
            Qoutes.Add(new Qoutes { id = 36, TheQoute = "Stop judging by mere appearances, but instead judge correctly.", WordCount = 9, Category = "Bible", QouteSource = "John 7:24", MaxChar = 12 });
            Qoutes.Add(new Qoutes { id = 37, TheQoute = "Be merciful to those who doubt", WordCount = 6, Category = "Bible", QouteSource = "Jude 1:22", MaxChar = 8 });
            Qoutes.Add(new Qoutes { id = 38, TheQoute = "If you love me, keep my commands.", WordCount = 7, Category = "Bible", QouteSource = "John 14:15 ", MaxChar = 9 });
            Qoutes.Add(new Qoutes { id = 39, TheQoute = "Be merciful, just as your Father is merciful.", WordCount = 8, Category = "Bible", QouteSource = "Luke 6:36", MaxChar = 9 });
          Qoutes.Add(new Qoutes { id = 40, TheQoute = "Praise be to you, LORD, teach me your decrees.", WordCount = 9, Category = "Bible", QouteSource = "Psalms 119:12", MaxChar = 8 });
            Qoutes.Add(new Qoutes { id = 41, TheQoute = "Do everything without grumbling or arguing,", WordCount = 6, Category = "Bible", QouteSource = "Philippians 2:14", MaxChar = 10 });
            Qoutes.Add(new Qoutes { id = 42, TheQoute = "Produce fruit in keeping with repentance.", WordCount = 6, Category = "Bible", QouteSource = "Matthew 3:8", MaxChar = 11 });
            //       Qoutes.Add(new Qoutes { id =45, TheQoute = "Do everything without grumbling or arguing,", WordCount = 6, Category = "Bible", QouteSource = "Philippians 2:14" });
            //      Qoutes.Add(new Qoutes { id = 46, TheQoute = "Do everything without grumbling or arguing,", WordCount = 6, Category = "Bible", QouteSource = "Philippians 2:14" });
            //
            Sayings.Add(new Sayings {id = 1, TheSaying= "Life isn't about finding yourself. Life is about creating yourself.",WordCount =10,Category="Life", SayingSource= "George Bernard Shaw", MaxChar= 8 });
            Sayings.Add(new Sayings { id = 2, TheSaying = "The most important thing is to enjoy your life, to be happy, it's all that matters.", WordCount = 16, Category = "Life", SayingSource = "Audrey Hepburn", MaxChar = 9 });
            Sayings.Add(new Sayings { id = 3, TheSaying = "I have found that if you love life, life will love you back.", WordCount = 13, Category = "Life", SayingSource = "George Bernard Shaw", MaxChar = 5 });
            Sayings.Add(new Sayings { id = 4, TheSaying = "A brother may not be a friend, but a friend will always be a brother.", WordCount = 15, Category = "Family", SayingSource = "Ben Franklin (1706-1790)", MaxChar = 7 });
            Sayings.Add(new Sayings { id = 5, TheSaying = "Charity begins at home.", WordCount = 4, Category = "Family", SayingSource = "Tobias George Smollett (1721-1771)", MaxChar = 7 });
            Sayings.Add(new Sayings { id = 6, TheSaying = "Home is where the heart is.", WordCount = 6, Category = "Family", SayingSource = "George Bernard Shaw", MaxChar = 5 });
            Sayings.Add(new Sayings { id = 7, TheSaying = "United we stand, divided we fall.", WordCount = 6, Category = "Diversity", SayingSource = "Aesop (620 -560 B.C.)", MaxChar = 7 });
            Sayings.Add(new Sayings { id = 8, TheSaying = "Give good and get good.", WordCount = 5, Category = "Justice", SayingSource = "George Bernard Shaw", MaxChar = 5 });
            Sayings.Add(new Sayings { id = 9, TheSaying = "In our work and in our living, we must recognize that difference is a reason for celebration and growth, rather than a reason for destruction.", WordCount = 25, Category = "Diversity", SayingSource = "Audre Lorde", MaxChar = 11 });
            Sayings.Add(new Sayings { id = 10, TheSaying = "The most important thing a father can do for his children is to love their mother.", WordCount = 16, Category = "Family", SayingSource = "Theodore Hesburgh", MaxChar = 9 });
            Sayings.Add(new Sayings { id = 11, TheSaying = "Heaven means to be one with God.", WordCount = 7, Category = "Religious", SayingSource = "Confucius", MaxChar = 6 });
            Sayings.Add(new Sayings { id = 12, TheSaying = "He who rules must fully humor as much as he commands.", WordCount = 11, Category = "Leadership", SayingSource = "George Eliot (1819-1880)", MaxChar = 8 });
            Sayings.Add(new Sayings { id = 13, TheSaying = "The propitious smiles of Heaven can never be expected on a nation that disregards the eternal rules of order and right, which Heaven itself has ordained.", WordCount = 26, Category = "Religious", SayingSource = "George Washington", MaxChar = 10 });
            Sayings.Add(new Sayings { id = 14, TheSaying = "The ultimate test of man's conscience may be his willingness to sacrifice something today for future generations whose words of thanks will not be heard.", WordCount = 25, Category = "Sacrifice", SayingSource = "Gaylord Nelson", MaxChar = 11 });
            Sayings.Add(new Sayings { id = 15, TheSaying = "It takes courage to grow up and become who you really are.", WordCount = 12, Category = "Growth", SayingSource = "E. E. Cummings", MaxChar = 7 });
            Sayings.Add(new Sayings { id = 16, TheSaying = "Your eyes make me shy.", WordCount = 5, Category = "Crush", SayingSource = "Anais Nin", MaxChar = 4 });
            Sayings.Add(new Sayings { id = 17, TheSaying = "When one door closes, another opens; but we often look so long and so regretfully upon the closed door that we do not see the one that has opened for us.", WordCount = 31, Category = "Life", SayingSource = "Alexander Graham Bell", MaxChar = 11 });
            Sayings.Add(new Sayings { id = 18, TheSaying = "Accidents will happen", WordCount = 3, Category = "Warnings", SayingSource = "George Colman (1732-1794)", MaxChar = 9 });
            Sayings.Add(new Sayings { id = 19, TheSaying = "You were born an original. Don t die a copy.", WordCount = 9, Category = "Reality", SayingSource = "John Mason", MaxChar = 8 });
            Sayings.Add(new Sayings { id = 20, TheSaying = "Walking with a friend in the dark is better than walking alone in the light.", WordCount = 15, Category = "Friendship", SayingSource = "Helen Keller", MaxChar = 7 });
            Sayings.Add(new Sayings { id = 21, TheSaying = "We all have our time machines. Some take us back, they're called memories. Some take us forward, they're called dreams.", WordCount = 20, Category = "Time", SayingSource = "Jeremy Irons", MaxChar = 8 });
            Sayings.Add(new Sayings { id = 22, TheSaying = "Everything in moderation.", WordCount = 3, Category = "Wisdom", SayingSource = "Unknown", MaxChar = 10 });
            Sayings.Add(new Sayings { id = 23, TheSaying = "Honest difference of views and honest debate are not disunity. They are the vital process of policy among free men.", WordCount = 20, Category = "Diversity", SayingSource = "Herbert Hoover", MaxChar = 10 });
            Sayings.Add(new Sayings { id = 24, TheSaying = "Sometimes in life, you do things you don't want to. Sometimes you sacrifice, sometimes you compromise. Sometimes you let go and sometimes you fight. It's all about deciding what's worth losing and what's worth keeping", WordCount = 35, Category = "Sacrifice", SayingSource = "Lindy Zart", MaxChar = 10 });
            Sayings.Add(new Sayings { id = 25, TheSaying = "Some people say you are going the wrong way, when it's simply a way of your own", WordCount = 17, Category = "Yourself", SayingSource = "Angelina Jolie", MaxChar = 6 });
            Sayings.Add(new Sayings { id = 26, TheSaying = "Believe you can and you're halfway there.", WordCount = 7, Category = "Positivity", SayingSource = "Theodore Roosevelt", MaxChar = 7 });
            Sayings.Add(new Sayings { id = 27, TheSaying = "Your small support could accomplish a big dream.", WordCount = 8, Category = "Support", SayingSource = "Mohammad Rishad sakhi", MaxChar = 10 });
            Sayings.Add(new Sayings { id = 28, TheSaying = "Do not trust all men, but trust men of worth; the former course is silly, the latter a mark of prudence.", WordCount =21 , Category = "Trust", SayingSource = "Democritus", MaxChar = 8 });
            Sayings.Add(new Sayings { id = 29, TheSaying = "A friend is someone who gives you total freedom to be yourself.", WordCount = 12, Category = "Friendship", SayingSource = "Jim Morrison", MaxChar = 7 });
            Sayings.Add(new Sayings { id = 30, TheSaying = "Never regret taking a chance, even if you fall on your face; It's better to know you tried and failed than to wonder the great what if?", WordCount = 27, Category = "Opportunity", SayingSource = "Devin Frye", MaxChar = 6 });
            Sayings.Add(new Sayings { id = 31, TheSaying = "Never give an order that can't be obeyed.", WordCount = 8, Category = "Leadership", SayingSource = "General Douglas MacArthur", MaxChar = 6 });
            Sayings.Add(new Sayings { id = 32, TheSaying = "Determine never to be idle. No person will have occasion to complain of the want of time who never loses any. It is wonderful how much can be done if we are always doing.", WordCount = 34, Category = "Time", SayingSource = "Thomas Jefferson", MaxChar = 9 });
            Sayings.Add(new Sayings { id = 33, TheSaying = "Strength does not come from winning. Your struggles develop your strengths. When you go through hardships and decide not to surrender, that is strength.", WordCount = 24, Category = "Being Strong", SayingSource = "Arnold Schwarzenegger", MaxChar = 9 });
            Sayings.Add(new Sayings { id = 34, TheSaying = "We seal our fate with the choices we make.", WordCount = 9, Category = "Fate", SayingSource = "Gloria Estefan", MaxChar = 7 });
            Sayings.Add(new Sayings { id = 35, TheSaying = "You will never succeed if you do not believe that you deserve what you want. Visualize your goal. Make an effort to go for it and always stay positive that it can be yours.", WordCount = 34, Category = "Positivity", SayingSource = "Dr Anil Kr Sinha", MaxChar = 9 });
            Sayings.Add(new Sayings { id = 36, TheSaying = "To be one's self, and unafraid whether right or wrong, is more admirable than the easy cowardice of surrender to conformity.", WordCount = 21, Category = "Yourself", SayingSource = "Irving Wallace", MaxChar = 10 });
            Sayings.Add(new Sayings { id = 37, TheSaying = "Life is a progress, and not a station.", WordCount = 8, Category = "Life", SayingSource = "Ralph Waldo Emerson", MaxChar = 8 });
            Sayings.Add(new Sayings { id = 38, TheSaying = "All great things worth having require great sacrifice worth giving.", WordCount = 10, Category = "Sacrifice", SayingSource = "Paullina Simons", MaxChar = 9 });
            Sayings.Add(new Sayings { id = 39, TheSaying = "You learn so much from taking chances, whether they work out or not. Either way, you can grow from the experience and become stronger and smarter.", WordCount = 27, Category = "Being Strong", SayingSource = "John Legend", MaxChar = 10 });
            Sayings.Add(new Sayings { id = 40, TheSaying = "Be kind, for everyone you meet is fighting a harder battle", WordCount = 11, Category = "Compassion", SayingSource = "Plato", MaxChar = 8 });


        }

      

    }
}
