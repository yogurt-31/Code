#pragma once
#include "Object.h"

enum class SELECT_TYPE {
	STAGE1,
	STAGE2,
};

class StageSelectButton :
    public Object
{
public:
	StageSelectButton();
	~StageSelectButton();
public:
	void Update() override;
	void Render(HDC _hdc) override;
public:
	void LoadStage1();
	void LoadStage2();
private:
	int stage1x = 100, stage2x = 250;
	SELECT_TYPE stageType = SELECT_TYPE::STAGE1;
};

