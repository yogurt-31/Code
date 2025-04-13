#include "pch.h"
#include "StageSelectButton.h"
#include "Animator.h"
#include "InputManager.h"
#include "SceneManager.h"

StageSelectButton::StageSelectButton()
{
	GetTransform()->SetScale({ 128, 128 });
	AddComponent<Animator>();
	Animator* anim = GetComponent<Animator>();
	anim->CreateTexture(L"Texture\\Stage.bmp", L"stage");
	anim->CreateAnimation(L"StageSelect", Vec2(62, 0), Vec2(31, 31), Vec2(31, 0), 1, 0.1f);
	anim->PlayAnimation(L"StageSelect", false);
}

StageSelectButton::~StageSelectButton()
{
}

void StageSelectButton::Update()
{
	if (GET_KEYDOWN(KEY_TYPE::D)) {
		LoadStage2();
	}
	if (GET_KEYDOWN(KEY_TYPE::A)) {
		LoadStage1();
	}
	if (GET_KEYDOWN(KEY_TYPE::SPACE)) {
		switch (stageType)
		{
		case SELECT_TYPE::STAGE1:
			GET_SINGLE(SceneManager)->LoadScene(L"MyScene");
			break;
		case SELECT_TYPE::STAGE2:
			GET_SINGLE(SceneManager)->LoadScene(L"Stage2");
			break;
		}
	}
}

void StageSelectButton::Render(HDC _hdc)
{
	ComponentRender(_hdc);
}

void StageSelectButton::LoadStage1()
{
	stageType = SELECT_TYPE::STAGE1;
	GetTransform()->SetPosition({ stage1x, 200 });
}

void StageSelectButton::LoadStage2()
{
	stageType = SELECT_TYPE::STAGE2;
	GetTransform()->SetPosition({ stage2x, 200 });
}
