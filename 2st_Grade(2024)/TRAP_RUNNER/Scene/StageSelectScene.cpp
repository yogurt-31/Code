#include "pch.h"
#include "StageSelectScene.h"
#include "StageButton.h"
#include "StageText.h"
#include "StageSelectButton.h"
#include "Background.h"

void StageSelectScene::Init()
{
	Scene::Init();
	StageText* stageText = new StageText;
	StageButton* stage1 = new StageButton;
	StageButton* stage2 = new StageButton;
	StageSelectButton* stageSelect = new StageSelectButton;
	Background* background = new Background;
	AddObject(background, LAYER::BACKGROUND);
	AddObject(stageText, LAYER::UI);
	AddObject(stage1, LAYER::UI);
	AddObject(stage2, LAYER::UI);
	AddObject(stageSelect, LAYER::UI);

	stage1->SelectStageImage(L"Stage1");
	stage2->SelectStageImage(L"Stage2");

	stage1->GetTransform()->SetPosition({ 100 , 200 });
	stage2->GetTransform()->SetPosition({ 250 , 200 });
	stageSelect->GetTransform()->SetPosition({ 100 , 200 });
}

void StageSelectScene::Render(HDC _hdc)
{
	Scene::Render(_hdc);
}