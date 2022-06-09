using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞行棋游戏
{
    class Program
    {
        static int[] Maps = new int[100];//地图，静态字段模拟全局变量
        static int[] PlayerPos = new int[2];//存储两个玩家的坐标
        static string[] PlayerNames = new string[2];//存两个玩家的姓名
        static bool[] Flags = new bool[2];//两个玩家的标记，Flags[0]和Flags[1]默认都为false，用于暂停机关


        static void Main(string[] args)
        {
            GameShow();
            #region 输入玩家姓名
            Console.WriteLine("请输入玩家A的姓名：");
            PlayerNames[0] = Console.ReadLine();
            while (PlayerNames[0] == "")
            {
                Console.WriteLine(" 玩家A的姓名不能为空！重新输入：");
                PlayerNames[0] = Console.ReadLine();
            }
            Console.WriteLine("请输入玩家B的姓名：");
            PlayerNames[1] = Console.ReadLine();
            while (PlayerNames[1] == "" || PlayerNames[0] == PlayerNames[1])
            {
                if (PlayerNames[1] == "")
                {
                    Console.WriteLine(" 玩家B的姓名不能为空！重新输入：");
                    PlayerNames[1] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine(" 玩家B与玩家A的姓名不能相同！重新输入玩家A姓名：");
                    PlayerNames[1] = Console.ReadLine();
                }

            }
            #endregion
            Console.Clear();//输入完名字后清屏
            GameShow();
            Console.WriteLine("{0}的士兵用A表示", PlayerNames[0]);
            Console.WriteLine("{0}的士兵用B表示", PlayerNames[1]);
            InitailMap();
            DrawMap();
            while (PlayerPos[0] < 99 && PlayerPos[1] < 99)
            {
                if (Flags[0] == false)
                {
                    PlayGame(0);//Flags[0] = true;则不在执行
                }
                else
                {
                    Flags[0] = false;//保证第三回合可以玩游戏
                }
                if (PlayerPos[0] >= 99)
                {
                    Console.WriteLine("{0}赢得了胜利！！！！", PlayerNames[0]);
                    win();
                }
                if (Flags[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    Flags[1] = false;
                }
                if (PlayerPos[1] >= 99)
                {
                    Console.WriteLine("{0}赢得了胜利！！！！", PlayerNames[1]);
                    win();
                }
            } //while
            Console.ReadKey();

        }

        public static void GameShow()//游戏头
        {
            //Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t*************************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t*************************");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t*******飞行棋游戏********");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\t*************************");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t*************************");
        }
        //Maps[i]=0为□
        //Maps[i]=1为幸运轮盘◎
        //Maps[i]=2为地雷☆
        //Maps[i]=3为暂停▲
        //Maps[i]=4为时空隧道F
        public static void InitailMap()//初始化地图
        {
            int[] luckyturn = { 6, 23, 40, 55, 69, 83 };//幸运轮盘◎
            for (int i = 0; i < luckyturn.Length; i++)
            {
                //int index = luckyturn[i];
                Maps[luckyturn[i]] = 1;
            }
            int[] Landine = { 5, 13, 17, 33, 38, 050, 64, 80, 94 };//地雷☆
            for (int i = 0; i < Landine.Length; i++)
            {
                Maps[Landine[i]] = 2;
            }
            int[] pause = { 9, 27, 60, 93 }; // 暂停▲
            for (int i = 0; i < pause.Length; i++)
            {
                Maps[pause[i]] = 3;
            }
            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 }; //时空隧道F
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                Maps[timeTunnel[i]] = 4;
            }
        }

        public static void DrawMap()
        {
            Console.WriteLine("幸运轮盘：◎    地雷： ☆    暂停：  ▲    时空隧道：卍");
            //第一横行
            #region
            for (int i = 0; i < 30; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            #endregion
            Console.WriteLine();
            //第一竖行
            #region
            for(int i = 30; i <= 35; i++)
            {
                for (int j = 0; j <= 28; j++)//填充地图
                {
                    Console.Write("  ");
                }
                //画图
                Console.WriteLine(DrawStringMap(i));
            }                   
            #endregion
            //第二横行
            #region
            for (int i = 64; i > 35; i--)
            {
                Console.Write(DrawStringMap(i));
            }
            #endregion
            //第二竖行
            #region
            for (int i = 65; i <= 69; i++)
            {
                Console.WriteLine(DrawStringMap(i));
            }
            #endregion
            //第三横行
            #region
            for (int i = 70; i <= 99; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            #endregion
            Console.WriteLine();//换行
        }
        /// <summary>
        /// 把画图的程序抽象成为一个方法
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string DrawStringMap(int i)
        {
            string str = "";
            #region
            if (PlayerPos[0] == PlayerPos[1] && PlayerPos[0] == i)//玩家A与玩家B坐标相同，并且都在地图上，画<>
            {
                str = "<>";
            }
            else if (PlayerPos[0] == i)
            {
                str = "A";
            }
            else if (PlayerPos[1] == i)
            {
                str = "B";
            }
            else
            {
                switch (Maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        str = "□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        str = "◎";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        str = "☆";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        str = "▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        str = "卍";
                        break;
                }
            }
            return str;
            #endregion
        }


        /// <summary>
        /// 游戏逻辑代码
        /// </summary>
        /// <param name="PlayerNumber"></param>
        public static void PlayGame(int PlayerNumber)
        {
            Random r = new Random();
            int rNumber = r.Next(1, 7);
            Console.WriteLine("{0}按任意键开始游戏", PlayerNames[PlayerNumber]);
            Console.ReadKey(true);//true则不显示按下的健 
            Console.WriteLine("{0}抛出了{1}！", PlayerNames[PlayerNumber], rNumber);
            PlayerPos[PlayerNumber] += rNumber;
            ChangePos();
            Console.ReadKey(true);//true则不显示按下的健 
            Console.WriteLine("{0}按任意键开始行动", PlayerNames[PlayerNumber]);
            Console.ReadKey(true);//true则不显示按下的健 
            Console.WriteLine("{0}完成了行动", PlayerNames[PlayerNumber]);
            Console.ReadKey(true);//true则不显示按下的健 
            if (PlayerPos[PlayerNumber] == PlayerPos[1 - PlayerNumber])
            {
                Console.WriteLine("{0}踩到了{1}，{1}退6格！", PlayerNames[PlayerNumber], PlayerNames[1 - PlayerNumber], PlayerNames[1 - PlayerNumber]);
                PlayerPos[PlayerNumber] -= 6;
                ChangePos();
                Console.ReadKey(true);//true则不显示按下的健 
            }
            else//踩到了机关
            {
                switch (Maps[PlayerPos[PlayerNumber]])//0 1 2 3 4
                {
                    case 0:
                        Console.WriteLine("{0}踩到了方块！", PlayerNames[PlayerNumber]);
                        Console.ReadKey(true);//true则不显示按下的健 
                        break;
                    case 1:
                        Console.WriteLine("{0}踩到了幸运轮盘！请选择 1--交换位置 2--轰炸对方", PlayerNames[PlayerNumber]);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine("{0}与{1}交换了位置！", PlayerNames[PlayerNumber], PlayerNames[1 - PlayerNumber]);
                                Console.ReadKey(true);//true则不显示按下的健 
                                int temp = PlayerPos[PlayerNumber];
                                PlayerPos[PlayerNumber] = PlayerPos[1 - PlayerNumber];
                                PlayerPos[1 - PlayerNumber] = temp;
                                Console.WriteLine("交换完成！按任意键继续游戏！！！", PlayerNames[PlayerNumber], PlayerNames[1 - PlayerNumber]);
                                Console.ReadKey(true);//true则不显示按下的健 
                                break;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("{0}轰炸了{1}{2}退6格！", PlayerNames[PlayerNumber], PlayerNames[1 - PlayerNumber], PlayerNames[1 - PlayerNumber]);
                                Console.ReadKey(true);//true则不显示按下的健 
                                PlayerPos[1 - PlayerNumber] -= 6;
                                Console.WriteLine("{0}退了6格！", PlayerNames[1 - PlayerNumber]);
                                Console.ReadKey(true);//true则不显示按下的健 
                                break;
                            }
                            else
                            {
                                Console.WriteLine("{0}输入有误请重新输入数字！ 请选择 1--交换位置 2--轰炸对方", PlayerNames[PlayerNumber]);
                                input = Console.ReadLine();
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("{0}踩到了地雷！退6格！", PlayerNames[PlayerNumber]);
                        Console.ReadKey(true);//true则不显示按下的健 
                        PlayerPos[PlayerNumber] -= 6;
                        break;
                    case 3:
                        Console.WriteLine("{0}踩到了暂停！暂停一回合", PlayerNames[PlayerNumber]);
                        Flags[PlayerNumber] = true;
                        Console.ReadKey(true);//true则不显示按下的健 
                        break;
                    case 4:
                        Console.WriteLine("{0}踩到了时空隧道！前进10格！", PlayerNames[PlayerNumber]);
                        PlayerPos[PlayerNumber] += 10;
                        Console.ReadKey(true);//true则不显示按下的健 
                        break;
                }  //swich           
            } //else
            ChangePos();//perfect
            Console.Clear();
            DrawMap();

        }

        /// <summary>
        /// 坐标改变时候判断玩家坐标是否在地图内
        /// </summary>
        public static void ChangePos()
        {
            if (PlayerPos[0] < 0)
            {
                PlayerPos[0] = 0;
            }
            if (PlayerPos[0] >= 99)
            {
                PlayerPos[0] = 99;
            }
            if (PlayerPos[1] < 0)
            {
                PlayerPos[1] = 0;
            }
            if (PlayerPos[1] >= 99)
            {
                PlayerPos[1] = 99;
            }
        }

       public static void win()
        {
            Console.WriteLine("胜利！！！！！！！！！！！！");
            Console.WriteLine("胜利！！！！！！！！！！！！");
            Console.WriteLine("胜利！！！！！！！！！！！！");
            Console.WriteLine("胜利！！！！！！！！！！！！");
            Console.WriteLine("胜利！！！！！！！！！！！！");
            Console.WriteLine("胜利！！！！！！！！！！！！");
            Console.WriteLine("胜利！！！！！！！！！！！！");
            Console.WriteLine("胜利！！！！！！！！！！！！");
            Console.WriteLine("胜利！！！！！！！！！！！！");
            Console.WriteLine("胜利！！！！！！！！！！！！");
        }

    }
}
