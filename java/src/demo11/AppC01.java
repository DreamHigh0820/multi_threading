/*
 * THREAD POOLS
 * Version C01: Thread pools and Future objects - Getting started
 *
 * Future objects help you to programatically manage tasks, such as:
 * - Wait for a task to finish executing (and get result), with the "get" method.
 * - Cancel a task prematurely, with the "cancel" method.
 */

package demo11;

import java.util.concurrent.ExecutionException;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.Future;



public class AppC01 {

    public static void main(String[] args) throws InterruptedException, ExecutionException {
        ExecutorService executor = Executors.newSingleThreadExecutor();

        Future<String> task = executor.submit(() -> "lorem ipsum");

        executor.shutdown();

        while (false == task.isDone()) {
            // Waiting...
        }

        String result = task.get();
        System.out.println(result);
    }

}
