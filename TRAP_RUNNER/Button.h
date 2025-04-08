#pragma once
#include "Object.h"
#include "Condition.h"

class Button :
    public Object, public Condition
{
public:
	Button();
	~Button();
public:
	void Update() override;
	void Render(HDC _hdc) override;
public:
	void EnterCollision(Collider* _other)override;
};

