VAR HP = 1
VAR SAN = 2

VAR Hunger_VAR = 50
VAR Thirst_VAR = 50
VAR Mood_VAR = 50
VAR Sleep_VAR = 50
VAR Illness_VAR = 50

VAR STR_VAR = 5
VAR INT_VAR = 5
VAR DEX_VAR = 5
VAR WIS_VAR = 5

VAR HUN = 5
VAR MON = 0

VAR CHECK = ""
VAR time = 0 // 统一大小写，C#，tag和global var中需要用相同的名字
VAR EVENT = 0

CONST NPC2 = "志愿者"
CONST NPC3 = "便利店老板"
CONST NPC4 = "蛇头"
CONST NPC5 = "翻译"

CONST HUGE_FAIL = "HugeFail"
CONST FAIL = "Fail"
CONST SUCCESS = "Success"
CONST HUGE_SUCCESS = "HugeSuccess"

//节点一的初始参数

~temp ShoreVillageTurn_1 = 0
~ShoreVillageTurn_1 ++
VAR been_tent_1 = 0
VAR been_shop_1 = 0
VAR been_smuggler_1 = 0
VAR been_restroom_1 = 0
VAR negative_event_1_complete = false
VAR negative_event_2_complete = false
VAR negative_event_3_complete = false
VAR negative_event_4_complete = false
VAR translator_you_meet = 0
VAR translator_you_employ = 0


//2-1-tent的初始参数

VAR know_doctor_1_can_speak_Chinese = false
VAR konw_you_can_buy_EOL = false
VAR buy_the_EOL = 0
VAR know_the_tent_post_1 = false
VAR get_the_aid_1 = false


//2-2-grocery的初始参数

VAR recognize_the_thief = false
VAR attitude_of_shopkeeper = 0
VAR know_shopkeeper_1_is_Chinese = false



//2-3-gang的初始参数

VAR know_the_travel_time_1 = false


//2-4-restroom的初始参数

VAR meet_shen_2 = false

//Thunderbolt的初始参数

VAR know_the_sleep_skill_1 = false
VAR know_the_rob = false




