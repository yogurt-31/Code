#include "pch.h"
#include "EndingManager.h"
#include <sstream>

void EndingManager::Init()
{
	ResetTime();
}

void EndingManager::SetEnding(bool isTrue)
{
	isClear = isTrue;
}

bool EndingManager::GetEnding()
{
	endTime = time(NULL);
	cout << endTime - startTime << endl;
	return isClear;
}

void EndingManager::ResetTime()
{
	startTime = time(NULL);
}

std::wstring EndingManager::GetTime()
{
	return std::to_wstring(endTime - startTime) + L" SEC";
}