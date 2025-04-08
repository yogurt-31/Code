#pragma once
#include "Scene.h"

enum class TILE
{
	NORMAL,
	TRAP,
	FALL,
	BUTTON,
	CONDITION,
	GOLD,
};

enum class ATKTRAP
{
	TOWER,
	LAZER,
	FOLLOW,
};

class PigScene :
	public Scene
{
public:
	void Init() override;
	void Render(HDC _hdc) override;
	void Release() override;
	Object* CreateTile(Vec2 vec, TILE tileType, int plusX = 0, int plusY = 0);
	void CreateTrap(Vec2 vec, ATKTRAP atkTrap, int plusX = 0, int plusY = 0);
	vector<Object*> CreateVerticalTileGroup(Vec2* startVec, TILE tileType, int x, int y, bool isDown, bool isJump);
	vector<Object*> CreateHorizontalTileGroup(Vec2* startVec, TILE tileType, int x, int y, bool isRight, bool isJump);
};
