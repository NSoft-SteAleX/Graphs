namespace GraphsRender.TaskTwo
{
    /// <summary>
    /// Класс, реализующий АТД "Отношения эвивалентности"
    /// </summary>
    public class UnionFind
    {
        private readonly int[] id;
        private readonly int[] sz;

        /// <summary>
        /// Поиск по дереву
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int FindUnite(int x)
        {
            while (x != id[x])
            {
                x = id[x];
            }

            return x;
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="n"></param>
        public UnionFind(int n)
        {
            id = new int[n];
            sz = new int[n];

            for (int i = 0; i < n; i++)
            {
                id[i] = i;
                sz[i] = 1;
            }
        }

        /// <summary>
        /// Поиск отношения эвивалентности
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public bool Find(int p, int q)
        {
            return FindUnite(p) == FindUnite(q);
        }

        /// <summary>
        /// Объединение
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        public void Unite(int p, int q)
        {
            int i = FindUnite(p), j = FindUnite(q);
           
            if (i == j) return; 
           
            if(sz[i] < sz[j])
            {
                id[i] = j;
                sz[j] += sz[i];
            }
            else
            {
                id[j] = i;
                sz[i] += sz[j];
            }
        }
    }
}
