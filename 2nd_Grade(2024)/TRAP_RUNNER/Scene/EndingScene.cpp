#include "pch.h"
#include "EndingScene.h"
#include "GameOverText.h"
#include "GameClearText.h"
#include "EndingManager.h"
#include "SceneManager.h"
#include "TimeText.h"
#include "Gold.h"
#include "Background.h"
#include "ReturnTitle.h"

void EndingScene::Init()
{
	Scene::Init();
	Object* gameResultText = nullptr;

	if(GET_SINGLE(EndingManager)->GetEnding())
		gameResultText = new GameClearText;
	else gameResultText = new GameOverText;

	AddObject(gameResultText, LAYER::UI);
	gameResultText->GetTransform()->SetPosition({ SCREEN_WIDTH / 2 , 100 });
	GET_SINGLE(EndingManager)->SetEnding(false);

	TimeText* timeText = new TimeText;
	AddObject(timeText, LAYER::UI);

	Background* background = new Background;
	AddObject(background, LAYER::BACKGROUND);

	Gold* gold = new Gold;
	AddObject(gold, LAYER::UI);
	gold->GetTransform()->SetPosition({ SCREEN_WIDTH - 200, SCREEN_HEIGHT - 200 });

	ReturnTitle* returnTitle = new ReturnTitle;
	AddObject(returnTitle, LAYER::UI);
}

void EndingScene::Render(HDC _hdc)
{
	Scene::Render(_hdc);
}
