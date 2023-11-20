#include <iostream>
#include <vector>
#include <string>
#include "Chess.cpp"



using namespace std;

int main() {
    vector<string> msg{"hello", "world", "c++"};

    for (const string &word : msg) {
        cout << word << " ";
    }

    Chess* game = new Chess(5);
    int number = (*game).getNumber();
    string word = game->getWord();

    


    cout << "The word is: " << word << ". The number is: " << number << endl;






    cout << endl;
}