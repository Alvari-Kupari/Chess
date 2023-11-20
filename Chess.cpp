#include <string>

using namespace std;

class Chess {
    private :

    int a;
    string word;

    public : 
    Chess(int number) {
        a = number;
        word = "hi";
    }

    string getWord() {
      return word;  
    }

    int getNumber() {
        return a;
    }
};