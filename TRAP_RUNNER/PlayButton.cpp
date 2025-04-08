#include "pch.h"
#include "PlayButton.h"
#include "SpriteRenderer.h"
#include "Collider.h"

PlayButton::PlayButton()
{
	GetTransform()->SetScale({ 150, 75 });

	AddComponent<SpriteRenderer>();
	SpriteRenderer* spriteRenderer = GetComponent<SpriteRenderer>();
	spriteRenderer->CreateTexture(L"Texture\\Play.bmp", L"play");
	GetTransform()->SetPosition({ 200 , 300 });
}

PlayButton::~PlayButton()
{
}

void PlayButton::Update()
{
}

void PlayButton::Render(HDC _hdc)
{
	ComponentRender(_hdc);
}

void PlayButton::EnterCollision(Collider* _other)
{
}

void PlayButton::StayCollision(Collider* _other)
{
}

void PlayButton::ExitCollision(Collider* _other)
{
}
