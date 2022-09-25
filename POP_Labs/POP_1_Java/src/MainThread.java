public class MainThread extends Thread{
    private final int id;
    private final BreakThread breakThread;

    public MainThread(int id, BreakThread breakThread) {
        this.id = id;
        this.breakThread = breakThread;
    }

    @Override
    public void run() {
        long sum = 0;
        int step = 5;
        long i = 0;
        boolean isStop = false;
        do{
            sum += step;
            i++;
            isStop = breakThread.isCanBreak();
        } while (!isStop);
        System.out.println("Cума = " + sum + ", крок = " + step + ", кiлькiсть доданкiв = " + i);
    }
}