#include "pch.h"
#include "GameOverText.h"
#include "SpriteRenderer.h"

GameOverText::GameOverText()
{
	GetTransform()->SetScale({ 540, 128 });
	AddComponent<SpriteRenderer>();
	SpriteRenderer* spriteRenderer = GetComponent<SpriteRenderer>();
	spriteRenderer->CreateTexture(L"Texture\\GameOver.bmp", L"GameOverText");
}

GameOverText::~GameOverText()
{
}

void GameOverText::Update()
{
}

void GameOverText::Render(HDC _hdc)
{
	ComponentRender(_hdc);
}
