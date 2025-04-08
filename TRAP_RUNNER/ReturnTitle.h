#pragma once
#include "Object.h"
class ReturnTitle :
    public Object
{
public:
	ReturnTitle();
	~ReturnTitle();
public:
	void Update() override;
	void Render(HDC _hdc) override;
};

