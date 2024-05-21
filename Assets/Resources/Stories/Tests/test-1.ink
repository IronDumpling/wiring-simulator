-> test

=== test ===

VAR correction_test = false
~ correction_test = false

测试内容：

+时间推进 ->time_tag_test
+随机数测试 ->random_test
+掷骰测试 ->dice_test
+对话次数测试 ->frequency_test
+数值测试 ->value_test
+常规测试 ->normal_test

-->END

=time_tag_test

  点击以下选项，时间将推进或清零。
  最终计数器将呈现出总计时间，以此判断时间tag是否正常运行。

  +快进十分钟 #time:+10min
  +快进一小时二十分钟 #time:+1hr,20min
  +快进一天一小时 #time:+1d,1hr
  +时间清零 #time:=0
  +退出时间推进测试
  ->test

  -最终时间：//计数器的位置，这个得交给程序来做。最好还能够直接在文本上显示出来。
  //以及这些tag只是暂时的，最终不一定是这么写，主要看程序。

  ->time_tag_test

=random_test

  //即之前说过的可以从多个结果中选一个，且选择后、再次选择时，可以排除已选结果的的功能。
  
  ->test

=dice_test
  
  骰出相应的骰子，并进行最终结果的判定。
  事件或身体状况将影响投掷结果，即所谓的“补正”。
  通过修改投掷内容或比对值的大小，可以判断掷骰系统是否正常运行。
  注：在选择“受事件影响补正的八面骰”之前，应先点击“获得事件补正”一项。
  检定难度标准分级：
    trivial
    easy
    medium
    challenge
    formidable
    ledendary
    heroic
    godly
    impossible
  
   +投掷八面骰 #dice:1d8>easy
   
   +投掷两个六面骰 #dice:2d6>medium
   
   +在血量基础上，投掷一个八面骰 #dice:HP+1d8>godly
   
   +投掷一个受事件影响补正的八面骰 
    {correction_test} #dice:1d8+2>heoric 
    {not correction_test} #dice:1d8>challenge
   +{not correction_test}获得事件补正+2
    ~correction_test = true
    
   +投掷一个受血量影响补正的八面骰 //这里还不知道怎么写，大意是：当血量大于等于10时，获得+2补正；反之则获得-2补正。不知道tag是否可以完成这种语法，感觉有些复杂，可以重点讨论。
   
   ->dice_test
   
   +退出掷骰测试
  ->test

   -投掷结果：//投掷结果的位置，也是最好能在文本上显示出来。
  ->dice_test
  
  
=frequency_test
    
  对话或事件触发次数的测试。
  //还没有想好这个功能最好怎么编写和使用，因为最终似乎都会单独打一个tag名。
  
  +选A //这类选择的tag
  +选B //同上
  +退出对话次数测试
  ->test
  
  -选A的次数：
  选B的次数：
  总计选择次数：
  
  ->frequency_test
 
  
  
=value_test
  VAR HP = 5
  数值计算及选项按条件来判断是否出现的测试。
  
  +将血量增加一点 #HP:+1
  +将饱腹度增加两点 #FUL:+2
  +将血量减少一点 #HP:-1
  +将血量变为1 #HP:=1
  +将血量增加至上限 //这个tag还不知道怎么写，看程序怎么样方便。
  +{HP == 20}当血量达到20，该选项才会出现
  +退出数值测试
    ->test
  
  -目前的血量为：
  目前的饱腹度为：
  ->value_test
  
=normal_test
    NPC说话 #speaker:NPC #title:NPC #portrait:
    你说话 #speaker:YOU #title:YOU #portrait:
    
    +继续
        这是一张图 #image:Tests/Placeholder
        ++离开
            ->test
    
    ->normal_test
  
  
  