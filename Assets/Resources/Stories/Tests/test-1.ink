INCLUDE ../0-global.ink

-> test

=== test ===
VAR correction_test = false
~ correction_test = false
测试内容：
+[常规测试] ->normal_test
+[玩家数值测试] ->value_test
+[时间推进测试] ->time_test
+[检定测试] ->check_test
+[物品测试] ->object_test
+[事件条件测试] ->event_test
-->END

=normal_test
    NPC说话 #speaker:NPC #title:NPC #portrait:
    你说话 #speaker:YOU #title:YOU #portrait:
    
    +[继续]
        这是一张图 #image:Tests/Placeholder
        ++[离开]
            ->test
    
    ->normal_test
    
=value_test
  数值计算及选项按条件来判断是否出现的测试。
  
  +[将生命增加一点] #HP:+1
  +[将理智增加两点] #SAN:+2
  +[将生命减少一点] #HP:-1
  +[将生命变为1] #HP:=1
  +[将生命增加至上限] #HP:=10000
  +[将饱食度增加十点] #Hunger:+10
  +[将饱食度减少十点] #Hunger:-10
  +[将饥渴度增加十点] #Thirst:+10
  +[将饥渴度减少十点] #Thirst:-10
  +[将睡眠度增加十点] #Sleep:+10
  +[将睡眠度减少十点] #Sleep:-10
  +[将情绪值增加十点] #Mood:+10
  +[将情绪值减少十点] #Mood:-10
  +[将健康值增加十点] #Illness:+10
  +[将健康值减少十点] #Illness:-10
  +[将力量增加一点] #STR:+1
  +[将力量减少一点] #STR:-1
  
  +[将智慧增加一点] #INT:+1
  +[将智慧减少一点] #INT:-1
  
  +[将心智增加一点] #WIS:+1
  +[将心智减少一点] #WIS:-1
  
  +[将速度增加一点] #DEX:+1
  +[将速度减少一点] #DEX:-1
  +{HP >= 20}[当生命达到20，该选项才会出现]
  +[退出数值测试] ->test
  -目前的血量为：
  目前的饱腹度为：
  ->value_test
  
=time_test
  点击以下选项，时间将推进或清零。
  最终计数器将呈现出总计时间，以此判断时间tag是否正常运行。

  +[快进十分钟] #time:+10min
  +[快进一小时二十分钟] #time:+1hr,20min
  +[快进一天一小时] #time:+1d,1hr
  +[时间清零] #time:=0min
  +[退出时间推进测试]
  ->test
  
  -最终时间：//计数器的位置，这个得交给程序来做。最好还能够直接在文本上显示出来。
             //以及这些tag只是暂时的，最终不一定是这么写，主要看程序。
  ->time_test

=check_test
  骰出相应的骰子，并进行最终结果的判定。
  事件或身体状况将影响投掷结果，即所谓的“补正”。
  通过修改投掷内容或比对值的大小，可以判断掷骰系统是否正常运行。
  注：在选择“受事件影响补正的八面骰”之前，应先点击“获得事件补正”一项。
  检定难度标准分级：
    trivial, easy, medium
    challenge, formidable, legendary
    heroic, godly, impossible
  
   +[投掷八面骰] #check:2d6>easy
   { CHECK:
    - HUGE_SUCCESS: 这是一次大成功
    - SUCCESS: 这是一次成功
    - FAIL: 这是一次失败
    - HUGE_FAIL: 这是一次大失败
    }
   
   +[投掷两个六面骰] #check:2d6>medium
   { CHECK:
    - HUGE_SUCCESS: 这是一次大成功
    - SUCCESS: 这是一次成功
    - FAIL: 这是一次失败
    - HUGE_FAIL: 这是一次大失败
    }
   
   +[在血量基础上，投掷一个八面骰] #check:HP+2d6>godly
   { CHECK:
    - HUGE_SUCCESS: 这是一次大成功
    - SUCCESS: 这是一次成功
    - FAIL: 这是一次失败
    - HUGE_FAIL: 这是一次大失败
    }
   
   +[投掷两个受事件影响补正的八面骰] 
    {correction_test} #check:2d6+2>formidable
    {not correction_test} #check:2d6-2>formidable
   +{not correction_test}[获得事件补正+2]
    ~correction_test = true
    
   +[投掷一个受血量影响补正的八面骰] //这里还不知道怎么写，大意是：当血量大于等于10时，获得+2补正；反之则获得-2补正。不知道tag是否可以完成这种语法，感觉有些复杂，可以重点讨论。
   
   +[退出掷骰测试]->test

   -->check_test
  
=object_test
    +[获得物品Test5] #object:+Test5
    +[获得2个物品Test7] #object:+Test7*2
    +[获得5个物品Test8，1个物品Test3] #object:+Test8*5+Test3
    +[获得10个物品Test10，失去1个物品Test3] #object:+Test10*10-Test3
    +[失去物品Test1] #object:-Test1
    +[失去1个物品Test3，1个物品Test9] #object:-Test3-Test9
    +[失去2个物品Test7] #object:-Test7*2
    +[失去3个物品Test8，获得1个物品2，以及2个物品1] #object:-Test8*3+Test2+Test1*2
    +[退出物品测试]->test
-->object_test

=event_test
所有有关事件触发条件的测试：
    +[随机数测试] ->random_test
    +[对话次数测试] ->frequency_test
    +[退出事件测试]->test
->event_test
  
=random_test
//即之前说过的可以从多个结果中选一个，且选择后、再次选择时，可以排除已选结果的的功能。
->event_test
  
=frequency_test
  对话或事件触发次数的测试。
  //还没有想好这个功能最好怎么编写和使用，因为最终似乎都会单独打一个tag名。
  
  +[选A] //这类选择的tag
  +[选B] //同上
  +[退出对话次数测试] ->event_test
  
  -选A的次数：
  选B的次数：
  总计选择次数：
  
  ->frequency_test
  
  
  
  
  
  
