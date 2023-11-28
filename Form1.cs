using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace laba_ciaod_8
{
    public partial class Form1 : Form
    {

        const int KeyConst = 45678;
        int MaxHash = 10000;
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
            int keyLength = (int)(Math.Log10(MaxHash - 1) + 1);
            long kk = (long)Key * Key;
            if (kk < MaxHash)
            {
                return (int)kk;
            }
            int l = Convert.ToInt32(Math.Log10(kk) + 1);

            l = (l - keyLength) / 2;

            kk = kk / Convert.ToInt32(Math.Pow(10, l));
            return (int)(kk % Convert.ToInt32(Math.Pow(10, keyLength)));
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
            double A = (Math.Sqrt(5) - 1) / 2;
            return (int)(MaxHash * ((Key * A) % 1));
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
                        break;
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
                tmp[index].Add(keys[i]);
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

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        //public int[] getHeshArray()
        //{
        //    int[] array = CreateRandomArray(10000, 10000);
        //    int[] hashArray = new int[10000];
        //    Array.Clear(hashArray);
        //    int sum = 0;
        //    int numOfComparison = 0;
        //    int numsOfFound = 0;
        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        int j = HashingByMultiplication(array[i]);
        //        while (hashArray[j] != 0)
        //        {
        //            j++;
        //            j = j % (array.Length);
        //        }
        //        hashArray[j] = array[i];
        //    }
        //    return
        //}
        private void button2_Click(object sender, EventArgs e)
        {
            int[] array = CreateRandomArray(10000, 10000);
            int[] hashArray=new int[10000];
            Array.Clear(hashArray);
            int sum = 0;
            int numOfComparison = 0;
            int numsOfFound = 0;
            for(int i=0; i<array.Length; i++)
            {
                int j = HashingByMultiplication(array[i]);
                while (hashArray[j] != 0)
                {
                    j++;
                    j = j % (array.Length);
                }
                hashArray[j] = array[i];
            }
            int[] keys = CreateRandomArray(10000, 20000);
            var time = new Stopwatch();
            time.Start();
            int num = keys.Length;
            for (int i=0; i<keys.Length; i++)
            {
                numOfComparison = 0;
                int tmp = HashingByMultiplication(keys[i]) - 1;
                if (HashingByMultiplication(keys[i]) == 0)
                {
                    tmp = keys.Length-1;
                }
                int j = HashingByMultiplication(keys[i]);
                while(j != tmp)
                {
                    numOfComparison++;
                    if (keys[i] == hashArray[j])
                    {
                        numsOfFound++;
                        break;
                    }
                    j= (j+1)%keys.Length;
                }
                sum += numOfComparison;
            }
            time.Stop();
            textBox5.Text = Convert.ToString(time.ElapsedMilliseconds);
            textBox6.Text = Convert.ToString(sum / keys.Length);
            textBox7.Text = Convert.ToString(numsOfFound);
        }
    }
}
