#include<winuser.h>
#include<windows.h>
#include<cmath>
#include<iostream>

float d = 0;

float amountOfClicks = 10000;
float delayBetweenClick = 10;

bool click = false;

void GetDesktopResolution(int&, int&);
void spamClick();

int SCREEN_WIDTH, SCREEN_HEIGHT;

using namespace std;

int main() {
    cout << "Auto clicker by flebedev99\n press f8 to toggle auto clicking\n press f9 to exit auto clicker\n";
    cout << "Enter number of clicks: ";
    cin >> amountOfClicks;
    cout << "Enter delay between clicks: ";
    cin >> delayBetweenClick;
    cout << "Auto clicker by flebedev99 will initalize in one second \n";

    Sleep(1000);
    GetDesktopResolution(SCREEN_WIDTH, SCREEN_HEIGHT);
    spamClick();
    return 0;
}

void GetDesktopResolution(int& horizontal, int& vertical) {
   RECT desktop;
   const HWND hDesktop = GetDesktopWindow();
   GetWindowRect(hDesktop, &desktop);
   horizontal = desktop.right;
   vertical = desktop.bottom;
}

void spamClick() {
    while(d < amountOfClicks) {
        d+=1;
        if (GetAsyncKeyState(VK_F8) < 0) {
            click = !click;
        }
        if (click) {
           mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        if (GetAsyncKeyState(VK_F9) < 0) {
            return;
        }
        Sleep(delayBetweenClick);
    }
}