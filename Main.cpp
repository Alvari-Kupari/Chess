#include <iostream>
#include <vector>
#include <string>
#include "Chess.cpp"




using namespace std;

int main() {
    vector<string> msg{"hello", "world"};

    for (const string &word : msg) {
        cout << word << " ";
    }

    Chess game(5);
    int number = game.roundNumber();
    bool yes = game.isWhitesTurn();

    


    cout << "Is it whites turn? " << yes << ". The number is: " << number << endl;

    int x;

    cout << "Type a number";
    cin >> x;
    cout << "Your number is: " << x;
    cout << endl << "test";
    
    return 0;
}