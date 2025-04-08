#include "pch.h"
#include "GameClearText.h"
#include "SpriteRenderer.h"

GameClearText::GameClearText()
{
	GetTransform()->SetScale({ 540, 128 });
	AddComponent<SpriteRenderer>();
	SpriteRenderer* spriteRenderer = GetComponent<SpriteRenderer>();
	spriteRenderer->CreateTexture(L"Texture\\GameClear.bmp", L"GameClearText");
}

GameClearText::~GameClearText()
{
}

void GameClearText::Update()
{
}

void GameClearText::Render(HDC _hdc)
{
	ComponentRender(_hdc);
}
