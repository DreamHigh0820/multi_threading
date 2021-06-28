/*
 * SLEEP
*/

package demo03;



public class Program {

    public static void main(String[] args) {
        var th = new Thread(() -> {
            try {
                Thread.sleep(2000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }

            System.out.println("Done!!!");
        });

        th.start();

        System.out.println("main thread is running...");
    }

}
