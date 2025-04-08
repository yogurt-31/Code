#include "pch.h"
#include "TimeText.h"
#include "SpriteRenderer.h"
#include "EndingManager.h"

TimeText::TimeText()
{
    GetTransform()->SetScale({ 300, 64 });

    AddComponent<SpriteRenderer>();
    SpriteRenderer* sp = GetComponent<SpriteRenderer>();
    sp->CreateTexture(L"Texture\\PlayTime.bmp", L"PlayTime");

    GetTransform()->SetPosition({ 200, 250 });
}

TimeText::~TimeText()
{
}

void TimeText::Update()
{
}

void TimeText::Render(HDC _hdc)
{
    ComponentRender(_hdc);
    PrintText(_hdc);
}

void TimeText::PrintText(HDC _hdc)
{
    HFONT hFont = CreateFont(
        80,                // Height (폰트 크기)
        0,                 // Width (0이면 자동 설정)
        0,                 // Escapement
        0,                 // Orientation
        FW_NORMAL,           // Font Weight (FW_BOLD: 굵게, FW_NORMAL: 기본)
        FALSE,             // Italic
        FALSE,             // Underline
        FALSE,             // StrikeOut
        DEFAULT_CHARSET,   // Charset
        OUT_DEFAULT_PRECIS,// Out Precision
        CLIP_DEFAULT_PRECIS,// Clip Precision
        DEFAULT_QUALITY,   // Quality
        DEFAULT_PITCH | FF_SWISS, // Pitch and Family
        L"Arial");         // Font Name

    SetBkMode(_hdc, TRANSPARENT);
    SetTextColor(_hdc, RGB(255, 255, 255));

    HFONT oldFont = (HFONT)SelectObject(_hdc, hFont);

    TextOut(_hdc, 400, 210, GET_SINGLE(EndingManager)->GetTime().c_str(), 8);
    DeleteObject(hFont);
}
