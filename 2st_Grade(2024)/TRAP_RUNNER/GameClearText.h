#pragma once
#include "Object.h"
class GameClearText :
    public Object
{
public:
	GameClearText();
	~GameClearText();
public:
	void Update() override;
	void Render(HDC _hdc) override;
};

