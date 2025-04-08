#pragma once
#include "Object.h"
class Background :
    public Object
{
public:
	Background();
	~Background();
public:
	void Update() override;
	void Render(HDC _hdc) override;
};

