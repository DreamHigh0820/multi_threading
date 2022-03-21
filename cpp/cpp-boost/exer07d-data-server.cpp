/*
THE DATA SERVER PROBLEM
Version D: Solving the problem using a blocking queue
*/


#include <iostream>
#include <string>
#include <vector>
#include <boost/ref.hpp>
#include <boost/chrono.hpp>
#include <boost/thread.hpp>
#include "mylib-blockingqueue.hpp"
using namespace std;



#define sleepsec(secs) \
    do { boost::this_thread::sleep_for(boost::chrono::seconds(secs)); } while (0)



void checkAuthUser() {
    cout << "[   Auth   ] Start" << endl;
    // Send request to authenticator, check permissions, encrypt, decrypt...
    sleepsec(20);
    cout << "[   Auth   ] Done" << endl;
}



void processFiles(const vector<string>& lstFileName, mylib::BlockingQueue<string>& blkq) {
    for (size_t i = 0; i < lstFileName.size(); ++i) {
        const string& fileName = lstFileName[i];

        // Read file
        cout << "[ ReadFile ] Start " << fileName << endl;
        sleepsec(10);
        cout << "[ ReadFile ] Done  " << fileName << endl;

        blkq.put(fileName); // You may put file data here

        // Write log into disk
        sleepsec(5);
        cout << "[ WriteLog ]" << endl;
    }
}



void processRequest() {
    vector<string> lstFileName;
    lstFileName.push_back("foo.html");
    lstFileName.push_back("bar.json");

    mylib::BlockingQueue<string> blkq;

    // The server checks auth user while reading files, concurrently
    boost::thread th(&processFiles, boost::cref(lstFileName), boost::ref(blkq));
    checkAuthUser();

    // The server waits for completion of loading files
    for (size_t i = lstFileName.size(); i > 0; --i) {
        blkq.take();
    }

    cout << "\nNow user is authorized and files are loaded" << endl;
    cout << "Do other tasks...\n" << endl;

    th.join();
}



int main() {
    processRequest();
    return 0;
}
