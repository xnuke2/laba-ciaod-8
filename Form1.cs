using System.Windows.Forms;

namespace laba_ciaod_8
{
    public partial class Form1 : Form
    {

        const int KeyConst = 45678;
        int MaxHash = 1000;
        double A = (Math.Sqrt(5) - 1) / 2;
        public Form1()
        {
            InitializeComponent();
        }

        public int HashingByDivision(int Key)
        {
            return Key % 997;
        }
        public int HashingByMiddleOfTheSquares(int Key)
        {
            long hesh = (long)Key*Key;
            if (hesh < 1000) return (int)hesh;
            int length = (int)(Math.Log10(Key) + 1);
            int keyLength = (int)(Math.Log10(MaxHash-1) + 1);
            int RightCut =  (int)(Math.Pow(10, (length - keyLength) / 2));
            int LeftCut = (int)(Math.Pow(10, keyLength));
            long tmp = Key / RightCut;
            tmp = tmp % LeftCut;
            return (int)tmp;
        }
        public int HashingByCollapses(int Key)
        {
            int sum = 0;
            int tmpKey = Key;
            int del = MaxHash;
            while (tmpKey > 0)
            {
                sum += tmpKey %del ;
                tmpKey /= del;
            }
            return sum % del;
        }
        public int HashingByMultiplication(int Key)
        {
            
            return (int)(MaxHash*(Key * A % 1));
        }
        public int[] CreateRandomArray(int size, int maxElement)
        {
            Random random = new Random();
            int[] array = new int[size];
            for(int i = 0; i < size; i++)
            {
                array[i] = random.Next(0,maxElement);
            }
            return array;
        }
        public int CheckCollisionHashing(int[] keys, int typeOfHashing)
        {
            List<int>[] tmp =new List<int>[keys.Length];
            int collision=0;
            for(int i = 0; i < keys.Length; i++)
            {
                int index;
                switch (typeOfHashing)
                {
                    case 1:
                        index = HashingByDivision(keys[i]);
                        break;
                    case 2:
                        index=HashingByMiddleOfTheSquares(keys[i]);
                        break ;
                    case 3:
                        index = HashingByCollapses(keys[i]);
                        break;
                    case 4:
                        index = HashingByMultiplication(keys[i]);
                        break;
                    default:
                        return -1;
                }
                if (tmp[index] != null)
                {
                    collision++;
                }
                else
                {
                    tmp[index]=new List<int>(){};
                }
            }
            return collision;
        }

        private void buttonHeshFind_Click(object sender, EventArgs e)
        {
            int points1 = 0;
            int points2 = 0;
            int points3 = 0;
            int points4 = 0;
            for (int i = 0; i < numericUpDownNumsOfRepeat.Value; i++)
            {
                int[] keys = CreateRandomArray(1000, 100000);
                int collision1 = CheckCollisionHashing(keys, 1);
                int collision2 = CheckCollisionHashing(keys, 2);
                int collision3 = CheckCollisionHashing(keys, 3);
                int collision4 = CheckCollisionHashing(keys, 4);
                int min = Math.Min(Math.Min(collision1, collision2), Math.Min(collision3, collision4));
                if (collision1 == min)
                {
                    points1++;
                }

                if (collision2 == min)
                {
                    points2++;
                }

                if (collision3 == min)
                {
                    points3++;
                }

                if (collision4 == min)
                {
                    points4++;
                }
            }
            textBox1.Text = Convert.ToString(points1);
            textBox2.Text = Convert.ToString(points2);
            textBox3.Text = Convert.ToString(points3);
            textBox4.Text = Convert.ToString(points4);
        }
    }
}