public class Main {

    public static void main(String[] args) {
        Main main = new Main();
        int storageSize = 3;
        int itemNumbers = 9;
        main.starter(storageSize, itemNumbers);
    }

    private void starter(int storageSize, int itemNumbers) {
        Manager manager = new Manager(storageSize);

        for(int i = 0;i < 3;i++)
        {
            new Consumer(itemNumbers, manager);
        }
        for (int i = 0;i < 3;i++)
        {
            new Producer(itemNumbers,manager);
        }
    }
}