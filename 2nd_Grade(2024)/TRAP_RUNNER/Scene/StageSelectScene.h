#pragma once
#include "Scene.h"
class StageSelectScene :
    public Scene
{
public:
	void Init() override;
	void Render(HDC _hdc) override;
};

