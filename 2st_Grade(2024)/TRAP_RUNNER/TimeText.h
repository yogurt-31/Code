#pragma once
#include "Object.h"
class TimeText :
    public Object
{
public:
	TimeText();
	~TimeText();
public:
	void Update() override;
	void Render(HDC _hdc) override;
	void PrintText(HDC _hdc);
};

