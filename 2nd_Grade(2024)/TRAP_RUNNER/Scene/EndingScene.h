#pragma once
#include "Scene.h"
class EndingScene :
    public Scene
{
public:
	void Init() override;
	void Render(HDC _hdc) override;
};

