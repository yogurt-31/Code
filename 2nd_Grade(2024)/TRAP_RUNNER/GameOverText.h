#pragma once
#include "Object.h"
class GameOverText :
    public Object
{
public:
	GameOverText();
	~GameOverText();
public:
	void Update() override;
	void Render(HDC _hdc) override;
};

