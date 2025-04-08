#include "pch.h"
#include "FallTileObject.h"
#include "SpriteRenderer.h"
#include "Transform.h"
#include "Collider.h"
#include "Animator.h"
#include "Animation.h"
#include "Action.h"
#include "TimeManager.h"
#include "SceneManager.h"

void FallTileObject::FallTile(Object* owner) {

	auto func = [](Object* obj) {
		FallTileObject* tile = dynamic_cast<FallTileObject*>(obj);
		Animator* ani = tile->GetComponent<Animator>();
		ani->PlayAnimation(L"FallTile_Falling", false);

		auto func1 = [](Object* obj) {
			FallTileObject* tile = dynamic_cast<FallTileObject*>(obj);
			tile->GetComponent<Collider>()->SetEnable(false);
			};
		GET_SINGLE(TimeManager)->DelayRun(0.5f, func1, obj);

		auto func2 = [](Object* obj) {
			FallTileObject* tile = dynamic_cast<FallTileObject*>(obj);
			Animator* ani = tile->GetComponent<Animator>();
			ani->StopAnimation();
			tile->isEnter = false;
			ani->PlayAnimation(L"FallTile_Idle", false);
			tile->GetComponent<Collider>()->SetEnable(true);
			};

		GET_SINGLE(TimeManager)->DelayRun(2.3f, func2, obj);
		};

	GET_SINGLE(TimeManager)->DelayRun(0.7f, func, owner);
	SetName(L"Tile");
}

FallTileObject::FallTileObject()
{
	GetTransform()->SetScale({ 256,256 });

	AddComponent<Animator>();
	Animator* animator = GetComponent<Animator>();
	animator->CreateTexture(L"Texture\\FallTile.bmp", L"fallTile_Sheet");
	animator->CreateAnimation(L"FallTile_Idle", Vec2(0, 0), Vec2(32, 32), Vec2(32, 0), 1, 0.1f);
	animator->CreateAnimation(L"FallTile_Warning", Vec2(0, 0), Vec2(32, 32), Vec2(32, 0), 2, 0.2f);

	animator->CreateTexture(L"Texture\\FallTile_Falling.bmp", L"fallTile_falling_Sheet");
	animator->CreateAnimation(L"FallTile_Falling", Vec2(0, 0), Vec2(32, 32), Vec2(32, 0), 10, 0.1f);

	animator->PlayAnimation(L"FallTile_Idle", false);

	AddComponent<Collider>();
	Collider* col = GetComponent<Collider>();
	col->SetSize({ 256,256 });

	col->SetEnable(true);
	SetName(L"Tile");
}

FallTileObject::~FallTileObject()
{
}

void FallTileObject::Update()
{
}

void FallTileObject::Render(HDC _hdc)
{
	ComponentRender(_hdc);
}

void FallTileObject::EnterCollision(Collider* _other)
{
}

void FallTileObject::StayCollision(Collider* _other)
{
	if (_other->GetOwner()->GetName() == L"Player" && !isEnter) {
		isEnter = true;
		Animator* ani = GetComponent<Animator>();
		ani->StopAnimation();
		ani->PlayAnimation(L"FallTile_Warning", true);
		FallTile(this);
	}
}

void FallTileObject::ExitCollision(Collider* _other)
{
}
