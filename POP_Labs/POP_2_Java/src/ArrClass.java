public class ArrClass {
    private final int threadNum;
    public final int[] arr;
    public int index_res;

    public ArrClass(int dim, int threadNum)
    {
        arr = new int[dim];
        this.threadNum = threadNum;
        for (int i = 0; i < arr.length; i++)
        {
            arr[i] = ((int)(Math.random() * 50) - 25);
            System.out.print(arr[i] + ", ");
        }
        FindMinInThread(arr, threadNum);
    }
    private void FindMinInThread(int[] arr,int threadNum)
    {

        var thread1  = new Thread[threadNum];
        var min = 100;
        var MinIdx = -1;

        Object locker = new Object();

        for (int i = 0; i < threadNum; i++)
        {
            thread1[i] = new Thread(() ->
            {
                for (int j = 0; j < arr.length ;j++)
                {
                    synchronized (locker)
                    {
                        if (arr[j] < min)
                        {
                            min = arr[j];
                            MinIdx = j;
                        }
                    }
                }
            });
            thread1[i].start();
        }
        System.out.println("\nНайменше число масиву: "+ min + ", його індекс:  "+ MinIdx);
    }
}