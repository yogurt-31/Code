#include "pch.h"
#include "PigScene.h"
#include "Object.h"
#include "Agent.h"
#include "Transform.h"
#include "TileObject.h"
#include "TrapTileObject.h"
#include "FallTileObject.h"
#include "SpriteRenderer.h"
#include "CollisionManager.h"
#include "UIManager.h"
#include "RollingSkillUI.h"
#include "MagicTower.h"
#include "StarLazer.h"
#include "FollowTrap.h"
#include "Button.h"
#include "ConditionTile.h"
#include "ResourceManager.h"
#include "Gold.h"
#include "EndingManager.h"

void PigScene::Init()
{
	Scene::Init();
	GET_SINGLE(EndingManager)->Init();
	CollisionManager* cm = GET_SINGLE(CollisionManager);
	ResourceManager* rm = GET_SINGLE(ResourceManager);

	rm->LoadSound(L"BGM", L"Sound\\BGM.mp3", true);
	rm->Play(L"BGM");
	cm->CheckReset();
	cm->CheckLayer(LAYER::TRAP, LAYER::PLAYER);
	cm->CheckLayer(LAYER::PROJECTILE, LAYER::PLAYER);
	cm->CheckLayer(LAYER::BUTTON, LAYER::PLAYER);
	cm->CheckLayer(LAYER::BACKGROUND, LAYER::PLAYER);

	RollingSkillUI* rollingSkill = new RollingSkillUI;
	AddObject(rollingSkill, LAYER::UI);


#pragma region Tile Create

	vector<Object*> btns;
	Vec2* tilePos = new Vec2({ SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2 });
	CreateVerticalTileGroup(tilePos, TILE::NORMAL, 2, 10, false, true);
	CreateVerticalTileGroup(tilePos, TILE::NORMAL, 2, 3, false, true);
	CreateVerticalTileGroup(tilePos, TILE::NORMAL, 2, 3, false, true);
	CreateVerticalTileGroup(tilePos, TILE::NORMAL, 2, 3, false, true);
	CreateVerticalTileGroup(tilePos, TILE::NORMAL, 2, 10, false, true);
	CreateTrap(*tilePos, ATKTRAP::TOWER, 512, 2176);
	CreateTrap(*tilePos, ATKTRAP::TOWER, -256, 1664);
	CreateTrap(*tilePos, ATKTRAP::TOWER, 512, 1152);
	CreateTrap(*tilePos, ATKTRAP::TOWER, -256, 640);
	CreateTrap(*tilePos, ATKTRAP::TOWER, 512, 384);
	CreateVerticalTileGroup(tilePos, TILE::NORMAL, 2, 2, false, true);
	CreateTrap(*tilePos, ATKTRAP::TOWER, -512, 384);
	CreateVerticalTileGroup(tilePos, TILE::NORMAL, 2, 2, false, true);
	CreateTrap(*tilePos, ATKTRAP::TOWER, 512, 384);
	CreateHorizontalTileGroup(tilePos, TILE::NORMAL, 8, 2, true, true);
	tilePos->x -= 640;
	tilePos->y += 384;
	// Boom Trap
	CreateVerticalTileGroup(tilePos, TILE::NORMAL, 2, 4, true, true);
	CreateTile(*tilePos, TILE::TRAP, 0, -1152);
	CreateTile(*tilePos, TILE::TRAP, +256, -896);
	CreateTile(*tilePos, TILE::TRAP, 0, -640);
	CreateTile(*tilePos, TILE::TRAP, 256, -384);
	CreateVerticalTileGroup(tilePos, TILE::TRAP, 2, 1, true, false);
	CreateTrap(*tilePos, ATKTRAP::FOLLOW, 0, 0);
	CreateVerticalTileGroup(tilePos, TILE::NORMAL, 2, 3, true, true);
	CreateVerticalTileGroup(tilePos, TILE::NORMAL, 2, 2, true, false);
	// Fall Tile
	CreateTrap(*tilePos, ATKTRAP::TOWER, 512, 0);
	CreateTrap(*tilePos, ATKTRAP::TOWER, -256, 256);
	CreateTrap(*tilePos, ATKTRAP::TOWER, 512, 512);
	CreateTrap(*tilePos, ATKTRAP::TOWER, -256, 768);
	CreateVerticalTileGroup(tilePos, TILE::FALL, 2, 4, true, true);
	CreateVerticalTileGroup(tilePos, TILE::FALL, 2, 2, true, true);
	CreateTrap(*tilePos, ATKTRAP::FOLLOW, -256, -348);
	CreateVerticalTileGroup(tilePos, TILE::FALL, 2, 2, true, true);
	CreateVerticalTileGroup(tilePos, TILE::FALL, 2, 2, true, false);
	tilePos->x += 640;
	tilePos->y -= 256;
	CreateHorizontalTileGroup(tilePos, TILE::NORMAL, 2, 2, true, false);
	CreateHorizontalTileGroup(tilePos, TILE::NORMAL, 2, 1, true, false);
	Vec2* vec = new Vec2(tilePos->x - 512, tilePos->y - 256);
	CreateHorizontalTileGroup(vec, TILE::FALL, 2, 1, true, false);
	//tilePos->y += 256;
	btns.push_back(CreateTile(*tilePos, TILE::BUTTON, -256, 0));
	// Cross
	Vec2* tilePos2 = new Vec2(tilePos->x - 256, tilePos->y - 512);
	CreateVerticalTileGroup(tilePos2, TILE::NORMAL, 2, 3, false, false);
	btns.push_back(CreateTile(*tilePos2, TILE::BUTTON, 0, 256));
	tilePos2->y += 512;
	tilePos2->x += 512;
	CreateTile(*tilePos2, TILE::TRAP, -256, -256);
	CreateTrap(*tilePos2, ATKTRAP::FOLLOW, -256, 0);
	CreateTile(*tilePos2, TILE::TRAP, 0, 0);
	CreateTrap(*tilePos2, ATKTRAP::TOWER, 512, 256);
	CreateTrap(*tilePos2, ATKTRAP::TOWER, 768, 256);
	CreateTrap(*tilePos2, ATKTRAP::FOLLOW, 1280, -256);
	CreateTrap(*tilePos2, ATKTRAP::TOWER, 1536, 256);
	CreateTile(*tilePos2, TILE::TRAP, 768, 0);
	delete vec;
	vec = new Vec2(tilePos2->x + 1024, tilePos2->y);
	CreateVerticalTileGroup(vec, TILE::TRAP, 1, 2, false, false);
	CreateHorizontalTileGroup(tilePos2, TILE::NORMAL, 10, 2, true, false);
	btns.push_back(CreateTile(*tilePos2, TILE::BUTTON, 0, -256));
	CreateTile(*tilePos2, TILE::TRAP, -512, 0);
	CreateTrap(*tilePos2, ATKTRAP::FOLLOW, -256, 256);
	tilePos2->y -= 256;
	CreateVerticalTileGroup(tilePos2, TILE::NORMAL, 2, 5, true, false);
	CreateTrap(*tilePos2, ATKTRAP::FOLLOW, 512, -512);
	tilePos2->y -= 768;
	tilePos2->x += 512;
	CreateHorizontalTileGroup(tilePos2, TILE::NORMAL, 2, 2, true, false);

	CreateTile(*tilePos, TILE::TRAP, 512, -256);
	CreateTile(*tilePos, TILE::TRAP, 768, 0);
	delete vec;
	vec = new Vec2(tilePos->x + 1536, tilePos->y);
	CreateHorizontalTileGroup(vec, TILE::TRAP, 2, 1, true, false);
	CreateTrap(*tilePos, ATKTRAP::TOWER, 256, 256);
	CreateTrap(*tilePos, ATKTRAP::TOWER, 512, 256);
	CreateTrap(*tilePos, ATKTRAP::TOWER, 1280, 256);
	CreateTrap(*tilePos, ATKTRAP::TOWER, 1536, 256);
	CreateHorizontalTileGroup(tilePos, TILE::NORMAL, 11, 2, true, false);
	CreateTrap(*tilePos, ATKTRAP::TOWER, -768, 256);
	CreateTrap(*tilePos, ATKTRAP::TOWER, -512, 256);
	delete vec;
	vec = new Vec2(tilePos->x - 256, tilePos->y - 256);
	CreateHorizontalTileGroup(vec, TILE::TRAP, 2, 1, false, false);
	CreateTile(*tilePos, TILE::TRAP, 256, 0);
	tilePos->x += 512;
	btns.push_back(CreateTile(*tilePos, TILE::BUTTON));
	CreateHorizontalTileGroup(tilePos, TILE::NORMAL, 2, 2, true, false);
	tilePos->y -= 256;
	auto conditionTiles = CreateHorizontalTileGroup(tilePos, TILE::CONDITION, 2, 2, true, false);
	tilePos->x += 128;
	tilePos->y -= 128;
	CreateHorizontalTileGroup(tilePos, TILE::NORMAL, 1, 1, true, true);
	CreateTrap(*tilePos, ATKTRAP::LAZER, 0, -384);
	CreateHorizontalTileGroup(tilePos, TILE::NORMAL, 1, 1, true, true);
	CreateVerticalTileGroup(tilePos, TILE::NORMAL, 1, 1, false, true);
	CreateVerticalTileGroup(tilePos, TILE::FALL, 1, 1, false, true);
	CreateHorizontalTileGroup(tilePos, TILE::FALL, 1, 1, false, true);
	CreateHorizontalTileGroup(tilePos, TILE::FALL, 1, 1, false, true);
	CreateVerticalTileGroup(tilePos, TILE::FALL, 1, 1, false, true);
	CreateVerticalTileGroup(tilePos, TILE::FALL, 1, 1, false, true);
	CreateHorizontalTileGroup(tilePos, TILE::TRAP, 1, 1, true, true);
	CreateTrap(*tilePos, ATKTRAP::LAZER, 0, 384);
	CreateHorizontalTileGroup(tilePos, TILE::TRAP, 1, 1, true, true);
	CreateHorizontalTileGroup(tilePos, TILE::TRAP, 1, 1, true, true);
	CreateHorizontalTileGroup(tilePos, TILE::NORMAL, 1, 1, true, true);
	CreateHorizontalTileGroup(tilePos, TILE::NORMAL, 8, 1, true, false);
	CreateTile({ tilePos->x - 256, tilePos->y }, TILE::GOLD, 0, 0);

#pragma endregion

	Agent* agent = new Agent;
	agent->GetTransform()->SetPosition({ SCREEN_WIDTH / 2 , SCREEN_HEIGHT / 2 });
	AddObject(agent, LAYER::PLAYER);

	for (auto obj : conditionTiles)
	{
		auto ct = dynamic_cast<ConditionTile*>(obj);
		if (ct)
		{
			for (auto bobj : btns)
			{
				auto c = dynamic_cast<Condition*>(bobj);
				ct->AddCondiiton(c);
			}
		}
	}


	delete tilePos;
	delete tilePos2;
	delete vec;
}

void PigScene::Render(HDC _hdc)
{
	Scene::Render(_hdc);
	GET_SINGLE(UIManager)->RenderHP(_hdc);
}

void PigScene::Release()
{
	Scene::Release();
	ResourceManager* rm = GET_SINGLE(ResourceManager);
	rm->Stop(SOUND_CHANNEL::BGM);
}

Object* PigScene::CreateTile(Vec2 vec, TILE tileType, int plusX, int plusY)
{
	vec.x += plusX;
	vec.y += plusY;
	Object* tile = nullptr;
	switch (tileType) {
	case TILE::NORMAL:
		tile = new TileObject;
		AddObject(tile, LAYER::BACKGROUND);
		break;
	case TILE::TRAP:
		tile = new TrapTileObject;
		AddObject(tile, LAYER::TRAP);
		break;
	case TILE::FALL:
		tile = new FallTileObject;
		AddObject(tile, LAYER::TRAP);
		break;
	case TILE::BUTTON:
		tile = new Button;
		AddObject(tile, LAYER::BUTTON);
		break;
	case TILE::CONDITION:
		tile = new ConditionTile;
		AddObject(tile, LAYER::BACKGROUND);
		break;
	case TILE::GOLD:
		tile = new Gold;
		AddObject(tile, LAYER::BACKGROUND);
		break;
	}

	tile->GetTransform()->SetPosition(vec);

	return tile;
}

void PigScene::CreateTrap(Vec2 vec, ATKTRAP atkTrap, int plusX, int plusY)
{
	if (atkTrap == ATKTRAP::FOLLOW) return;
	vec.x += plusX;
	vec.y += plusY;
	Object* trap = nullptr;
	switch (atkTrap) {
	case ATKTRAP::TOWER:
		trap = new MagicTower(1);
		AddObject(trap, LAYER::TRAP);
		break;
	case ATKTRAP::LAZER:
		trap = new StarLazer(vec);
		AddObject(trap, LAYER::TRAP);
		break;
	case ATKTRAP::FOLLOW:
		trap = new FollowTrap();
		AddObject(trap, LAYER::PROJECTILE);
		break;
	}

	trap->GetTransform()->SetPosition(vec);
}

vector<Object*> PigScene::CreateVerticalTileGroup(Vec2* startVec, TILE tileType, int x, int y, bool isDown, bool isJump = true)
{
	vector<Object*> objvec = {};
	int dirY = isDown ? 1 : -1;
	int vecX = startVec->x;
	for (int i = 0; i < y; ++i) {
		for (int j = 0; j < x; ++j) {
			objvec.push_back(CreateTile(*startVec, tileType));
			startVec->x += 256;
		}
		startVec->x = vecX;
		startVec->y += dirY * 256;
	}
	if (isJump) startVec->y += dirY * 128;
	return objvec;
}

vector<Object*> PigScene::CreateHorizontalTileGroup(Vec2* startVec, TILE tileType, int x, int y, bool isRight, bool isJump = true)
{
	vector<Object*> objvec = {};
	int dirX = isRight ? 1 : -1;
	int vecY = startVec->y;
	for (int i = 0; i < x; ++i) {
		for (int j = 0; j < y; ++j) {
			objvec.push_back(CreateTile(*startVec, tileType));
			startVec->y -= 256;
		}
		startVec->y = vecY;
		startVec->x += dirX * 256;
	}
	if (isJump) startVec->x += dirX * 128;
	return objvec;
}
