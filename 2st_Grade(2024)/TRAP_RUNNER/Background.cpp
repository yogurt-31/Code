#include "pch.h"
#include "Background.h"
#include "SpriteRenderer.h"

Background::Background()
{
	GetTransform()->SetScale({ SCREEN_WIDTH,SCREEN_HEIGHT });
	AddComponent<SpriteRenderer>();
	SpriteRenderer* spriteRenderer = GetComponent<SpriteRenderer>();
	spriteRenderer->CreateTexture(L"Texture\\Background.bmp", L"Background");
	GetTransform()->SetPosition({ SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2 });
}

Background::~Background()
{
}

void Background::Update()
{
	
}

void Background::Render(HDC _hdc)
{
	ComponentRender(_hdc);
}
