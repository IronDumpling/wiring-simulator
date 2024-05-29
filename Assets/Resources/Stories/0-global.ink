VAR HP = 1
VAR SAN = 2
VAR HUN = 5
VAR MON = 0

VAR NPC2 = "志愿者"
VAR NPC3 = "便利店老板"
VAR NPC4 = "蛇头"
VAR NPC5 = "翻译"


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

VAR know_doctor_1_can_speak_Chinese = false
VAR konw_you_can_buy_EOL = false
VAR buy_the_EOL = 0
VAR know_the_tent_post_1 = false
VAR get_the_aid_1 = false
VAR meet_shen_2 = false
VAR know_the_travel_time_1 = false