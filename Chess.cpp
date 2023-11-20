#include <string>

using namespace std;

class Chess {
    private :

    int round;

    public : 
    Chess(int number) {
        round = number;
    }

    public :

    bool isWhitesTurn() {
      return round % 2 == 0;  
    }

    int roundNumber() {
        return round;
    }
};