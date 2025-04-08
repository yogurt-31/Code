#include "pch.h"
#include "RollingSkillUI.h"
#include "Animator.h"

RollingSkillUI::RollingSkillUI()
{
	GetTransform()->SetScale({ 100, 100 });

	AddComponent<Animator>();
	Animator* animator = GetComponent<Animator>();
	animator->CreateTexture(L"Texture\\Rolling.bmp", L"rollingSkill_sheet");
	animator->CreateAnimation(L"Rolling_Idle", Vec2(288, 0), Vec2(32, 32), Vec2(32, 0), 1, 0.1f);
	animator->CreateAnimation(L"Rolling_Reroll", Vec2(0, 0), Vec2(32, 32), Vec2(32, 0), 10, 0.08f);
	animator->PlayAnimation(L"Rolling_Idle", false);

	SetName(L"RollingSkillUI");

	GetTransform()->SetPosition({ 100, SCREEN_HEIGHT - 100 });
}

RollingSkillUI::~RollingSkillUI()
{
}

void RollingSkillUI::Update()
{
}

void RollingSkillUI::Render(HDC _hdc)
{
	ComponentRender(_hdc);
}

void RollingSkillUI::CoolTimeAnimation()
{
	Animator* animator = GetComponent<Animator>();
	animator->PlayAnimation(L"Rolling_Reroll", false);
}

void RollingSkillUI::EnterCollision(Collider* _other)
{
}

void RollingSkillUI::StayCollision(Collider* _other)
{
}

void RollingSkillUI::ExitCollision(Collider* _other)
{
}
