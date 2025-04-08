#include "pch.h"
#include "ReturnTitle.h"
#include "InputManager.h"
#include "SpriteRenderer.h"
#include "SceneManager.h"

ReturnTitle::ReturnTitle()
{
    GetTransform()->SetScale({ 597.5f, 50.0f });

    AddComponent<SpriteRenderer>();
    SpriteRenderer* sp = GetComponent<SpriteRenderer>();
    sp->CreateTexture(L"Texture\\ToTitle.bmp", L"toTitle");

    GetTransform()->SetPosition({ SCREEN_WIDTH / 2, SCREEN_HEIGHT - 100});
}

ReturnTitle::~ReturnTitle()
{
}

void ReturnTitle::Update()
{
    if (GET_KEYDOWN(KEY_TYPE::SPACE)) {
        GET_SINGLE(SceneManager)->LoadScene(L"Title");
    }
}

void ReturnTitle::Render(HDC _hdc)
{
    ComponentRender(_hdc);
}
