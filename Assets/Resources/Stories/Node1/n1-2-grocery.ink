INCLUDE ../0-global.ink

#title:杂货店

-> position_shop_1

=== position_shop_1 ===
  // 1. 智力检定
  {been_shop_1 == 0:
   刚走进门，你就瞧见了一个木头柜台——还有坐在后面的黄种人老板。柜台上放了两个大插排，上面密密麻麻地接了一堆电子设备。电器的主人们就站在门外，一边盯着，又一边打着呵欠。#speaker:
   整个商铺人员稀少，或许是少有人选择在此处停留——毕竟过了雨林，就是可以直达边境的集结点。
   你准备着挑选东西，一个熟悉的身影又闯进你的视野。
   
   
   [智力检定]#check:INT+1d8>easy
   
   {CHECK:
    - FAIL: 那是个矮个子黑人。他在货架后盯着你。 
    - HUGE_FAIL: 那是个矮个子黑人。他在货架后盯着你。
    - SUCCESS:是那个曾经想要偷你东西的矮个子。他在货架后盯着你。
    ~recognize_the_thief = true
    - HUGE_SUCCESS:是那个曾经想要偷你东西的矮个子。他在货架后盯着你。
    ~recognize_the_thief = true
    }
    
    
     ~been_shop_1 ++
     
     ->negative_event_1
   
   -else:
   空旷的货架——还有戴着墨镜的便利店老板。#speaker:
   
  }
    
    +[看看食品架。]#speaker:
    
    上面只摆了三类东西：面包，瓶装水，还有你再熟悉不过的红色包装泡面。
        
        ++[买一份面包。] #object:-金钱*1+面包*1
        ++[买一瓶水。] #object:-金钱*2+瓶装水*1
        ++[买一份泡面。] #object:-金钱*3+袋装泡面*1
        ++[（后退）]
    
    --->position_shop_1
    
    +[看看药品架。]#speaker:
    
    那是一个很小的架子。只有一种药物静静躺在上面，横七竖八。
    
        ++[买一份阿莫西林。] #object:-金钱*5+阿莫西林*1
        ++[（后退）]
    
    --->position_shop_1
    
    +[看看生活用品架。]
    
    上面摆满了你已经有的东西：雨伞，背包，睡袋……
    末尾是石灰包，可以在野外添水加热食物。
    但第二行的东西显然更上档次，你瞧见几个大号的精致睡袋。
        
        ++[买五个石灰包。] #object:-金钱*1+石灰包*5
        ++[买一个高级睡袋。] #object:-金钱*20+高级睡袋*1
        ++[（后退）]
    
    --->position_shop_1
    
    +[卖出物品。]
    
        ++[卖出塑料水瓶] #object:-塑料瓶*5+金钱*1
        ++[（后退）]
        
    --->position_shop_1    
    
    //+[更换货币种类。]#speaker:
    // 汇率讲价
    //->position_shop_1
    +[离开吧。]#speaker:
    你走出这栋破旧的木屋。等候设备充电的人们瞥了你一下，随即又垂下眼眸。你同他们一样疲惫。
        ++[继续]->END

  = negative_event_1
    
    ~temp quick_time = false
    
    [智力检定]#check:INT+1d8>challenge
    // 临时使用这个方法
    
    // 方法一： if else
    //{CHECK == FAIL || CHECK == HUGE_FAIL:
       // 你们的目光对视一瞬，随后，他径直从你身旁经过，又咕哝一句你听不懂的话。
    //-else:
       // 你们的目光对视一瞬，随后，他径直从你身旁经过。你听见他清楚地用当地语言说了一句“傻瓜”。
   // }
    
    // 方法二：switch case
    {CHECK:
    - FAIL: 你们的目光对视一瞬，随后，他径直从你身旁经过，又咕哝一句你听不懂的话。
    - HUGE_FAIL: 你们的目光对视一瞬，随后，他径直从你身旁经过，又咕哝一句你听不懂的话。
    - SUCCESS:你们的目光对视一瞬，随后，他径直从你身旁经过。你听见他清楚地用当地语言说了一句“傻瓜”。
    ~quick_time = true
    - HUGE_SUCCESS:你们的目光对视一瞬，随后，他径直从你身旁经过。你听见他清楚地用当地语言说了一句“傻瓜”。
    ~quick_time = true
    
    }
    
    
    *{translator_you_employ > 0}{CHECK == FAIL || CHECK == HUGE_FAIL}[（向翻译询问）他说什么？]#speaker: 
        俚语，意思是“傻瓜”。#speaker:翻译
    
        {translator_you_employ == 1 || translator_you_employ == 2 :
        听起来像本地口音，应该不是走线的。这种人最危险。#speaker:翻译
        ~quick_time = true
         }
    
        {translator_you_employ == 3:
        ……或者“笨蛋”。#speaker:翻译
        他若有若无地补上一句，似乎想纠正词语的意思。你感觉蠢透了。#speaker:
        }
        

            
            **{not quick_time && not recognize_the_thief}[有些不对……]#speaker:
            好一阵，你才想到检查自己的随身物品。你就地脱下背包，开始翻看里边的东西。很快，你感到一阵眩晕：一整套充电器和电池都不翼而飞。
            你冲出门，而那个矮个子已经消失在人群里。
    
                ***[（回到店里）]->talk_shopkeeper_1
                
            **{quick_time}[（迅速检查自己的背包）]#speaker:
            你就地脱下背包，开始检查里边的东西。很快，你感到一阵眩晕：一整套充电器和电池都不翼而飞。
            **{not quick_time && recognize_the_thief}[有些不对……]#speaker:
            你想到检查自己的随身物品，于是就地脱下背包，开始翻看里边的东西。很快，你感到一阵眩晕：一整套充电器和电池都不翼而飞。
            
            --你下意识地冲出大门。
            **[（尝试找寻那名小偷的踪迹）] 
            {quick_time:
            [灵巧检定]#check:DEX+1d8>challenge
            -else:
            [灵巧检定]#check:DEX+1d8+2>challenge
            }
            
            {CHECK:
            - FAIL: 好一阵子过去，你仍然没有找到他的影子。你确信——你跟丢了。
            - HUGE_FAIL: 好一阵子过去，你仍然没有找到他的影子。你确信——你跟丢了。
            - SUCCESS:你见那矮个子正站在一名蛇头的边上。周围都是人高马大的家伙……
            - HUGE_SUCCESS:你见那矮个子正站在一名蛇头的边上。周围都是人高马大的家伙……
            }
        
                //***{CHECK == SUCCESS || CHECK == HUGE_SUCCESS}[（跟上去）]->talk_shopkeeper_1  //暂时先这样
                ***[（回到店里）]->talk_shopkeeper_1
            
            **[（大喊）有小偷！]
            你呼喊着，却很快就发现这是自取其辱——在这片地区，还有什么比这更加滑稽的呢？
            与此同时，你确信他已经跑没影了。
                ***[（回到店里）]->talk_shopkeeper_1
                
            **[（回到店里）]->talk_shopkeeper_1
                
        
    
    *{not quick_time && not recognize_the_thief}[有些不对……]#speaker:
    好一阵，你才想到检查自己的随身物品。你就地脱下背包，开始翻看里边的东西。很快，你感到一阵眩晕：一整套充电器和电池都不翼而飞。
    你冲出门，而那个矮个子已经消失在人群里。
    
        **[（回到店里）]->talk_shopkeeper_1
    
    *{quick_time}[（迅速检查自己的背包）]#speaker:
    你就地脱下背包，开始检查里边的东西。很快，你感到一阵眩晕：一整套充电器和电池都不翼而飞。
    *{not quick_time && recognize_the_thief}[有些不对……]#speaker:
    你想到检查自己的随身物品，于是就地脱下背包，开始翻看里边的东西。很快，你感到一阵眩晕：一整套充电器和电池都不翼而飞。        

    
    -你下意识地冲出大门。
    *[（尝试找寻那名小偷的踪迹）] 
    {quick_time:
    [灵巧检定]#check:DEX+1d8>challenge
    -else:
    [灵巧检定]#check:DEX+1d8+2>challenge
    }
    
    {CHECK:
    - FAIL: 好一阵子过去，你仍然没有找到他的影子。你确信——你跟丢了。
    - HUGE_FAIL: 好一阵子过去，你仍然没有找到他的影子。你确信——你跟丢了。
    - SUCCESS:你见那矮个子正站在一名蛇头的边上。周围都是人高马大的家伙……
    - HUGE_SUCCESS:你见那矮个子正站在一名蛇头的边上。周围都是人高马大的家伙……
    }
        
        **{CHECK == SUCCESS || CHECK == HUGE_SUCCESS}[（跟上去）]->talk_shopkeeper_1  //暂时先这样
        **[（回到店里）]->talk_shopkeeper_1
    
    
    *[（大喊）有小偷！]
    你呼喊着，却很快就发现这是自取其辱——在这片地区，还有什么比这更加滑稽的呢？
    与此同时，你确信他已经跑没影了。
        **[（回到店里）]->talk_shopkeeper_1
        
    *[（回到店里）]->talk_shopkeeper_1
            
    
   
    
   
=talk_shopkeeper_1

    // 3. 三个检定，如果都成功，则送一件关键物品
   
    你再次走进屋。其他人似乎毫不在意方才的骚动。#speaker:
    招惹本地人了？ #speaker:老板
    老板忽然向你发话。他扶了扶自己的墨镜，你能感受到那张干巴面孔上的笑意。#speaker:
    那你得注意点，指不定哪天就被介绍到某个黑蛇头那边，然后把你卖了。#speaker:老板
    他们最喜欢的就是中国人，身价高，还好赎。
    说着，他点了支烟。#speaker:
    
    [感知检定]#check:WIS+1d8>trivial
    
    {CHECK:
    - SUCCESS:不会错的。你看着眼前的便利店老板。无论是神态、动作，还是语言的抑扬顿挫——他都熟悉到令你感到异样。
    - HUGE_SUCCESS:不会错的。你看着眼前的便利店老板。无论是神态、动作，还是语言的抑扬顿挫——他都熟悉到令你感到异样。
    - FAIL:他用着调侃的语气。
    - HUGE_FAIL:他用着调侃的语气。
    }
    
    
    *{CHECK == FAIL || CHECK == HUGE_FAIL}[你说话一定要这么别扭吗？]#speaker:
    只是过来人的忠告，朋友。#speaker:老板
    他说着，一边翘了翘眉毛。#speaker:
     
    *{CHECK == SUCCESS || CHECK == HUGE_SUCCESS}[你是中国人吧。]
    在这地方说中文的黄皮肤，还能是什么人呢，老乡？#speaker:老板
    ~attitude_of_shopkeeper ++ //老板好感度+1
    ~know_shopkeeper_1_is_Chinese = true
    
    [感知检定]#check:WIS+1d8>easy
    {CHECK:
    - SUCCESS:你察觉到老板脸上止不住的笑意。#speaker:
    - HUGE_SUCCESS:你察觉到老板脸上止不住的笑意——他似乎来了兴致。#speaker:
    - FAIL:他看着你。#speaker:
    - HUGE_FAIL:他看着你。#speaker:
    }
     
    *[（瞪着他）]#speaker:
    哦，哦！别急，我可没看到作案过程，也懒得做那些同流合污的事情。#speaker:老板
    他的语气里带着嘲讽。#speaker:
    说不定那东西其实早被偷了，只是现在才发现。别误会，我是同情你的。#speaker:老板
    
    -
    *[他偷了我的充电器。]#speaker:
        那大概是拿不回来了。或者你还记得他的样子，可以去问问蛇头。#speaker:老板
        不过这群人长得千篇一律的，都不知道怎么描述。
        他装模作样地叹了口气。#speaker:
        这里也没监控，难办哦。#speaker:老板
        
        [智力检定]#check:INT+1d8>medium #speaker:
        {CHECK:
        - SUCCESS:你忽然意识到，这个便利店与其他救济站有本质区别——它是通过货架售卖的。#speaker:
        而为了防止偷窃，大部分商铺都该是窗口取货才对。
        - HUGE_SUCCESS:你忽然意识到，这个便利店与其他救济站有本质区别——它是通过货架售卖的。#speaker:
        而为了防止偷窃，大部分商铺都该是窗口取货才对。
        - FAIL:当然不可能有监控。他不过在嘲讽你。
        - HUGE_FAIL:当然不可能有监控。他不过在嘲讽你。
        }
        
        **{CHECK == SUCCESS|| CHECK == HUGE_SUCCESS}[你就不担心有人偷东西？]#speaker:
        哈，好问题。#speaker:老板
        他指了指你身后。#speaker:
        这里不是休息站，只是一个聚落里的前哨。这里没什么人，我也赚不到什么钱，这小房间用一只眼睛就能看过来。#speaker:老板
        说着，他指了指自己的眼睛。#speaker:
        
            [智力检定]#check:INT+1d8>medium
            {CHECK:
            - SUCCESS:那他当然一直盯着——包括偷窃。
            - HUGE_SUCCESS:那他当然一直盯着——包括偷窃。
            - FAIL:这属于最常见的胡诌。你不止一次听过这种大话。
            - HUGE_FAIL:这属于最常见的胡诌。你不止一次听过这种大话。
            }
            
            ***{CHECK == SUCCESS || CHECK == HUGE_SUCCESS}[那就对了。]
            
            什么对了？#speaker:老板
            
                ****{CHECK == SUCCESS || CHECK == HUGE_SUCCESS}[你应该把偷窃过程看得一清二楚。]
            
                你瞧见便利店老板咧开笑。#speaker:
                你真是个聪明人，朋友，那你应该也能想清楚。#speaker:老板
                能从你的拉链背包里捞东西，那必须得贴着你好一阵，总不能一个错身就把东西拿走了。
                直说吧——我确实没看到他偷东西。如果有，那至少没发生在我的店里。
                他把两只胳膊都放到了桌面上，显然比方才更认真些。#speaker:
                你应该觉得自己运气好，老乡。我要是真和那人一伙，那你这辈子都得蒙在鼓里。#speaker:老板
                ~attitude_of_shopkeeper ++ //老板好感度+1
            
            *** ->talk_shopkeeper_1_1 //跳过选项的一个典型
        
        ** ->talk_shopkeeper_1_1 //也是跳过选项的一个典型
        
        
    -(talk_shopkeeper_1_1)
     
    他摸了摸自己反着油光的脸颊。#speaker:
    别灰心嘛。走线的路子多了去了，只要命还在，没什么东西是丢不得的。#speaker:老板
    至于充电设备，找找人共用，总能度过去。

    *{know_shopkeeper_1_is_Chinese == false}[……你是中国人？]#speaker:
      哦，现在才发现？我一直在说中文，还是黄皮肤——是不是太自然了？#speaker:老板
      他向后躺到了椅子上。#speaker:
      ~know_shopkeeper_1_is_Chinese = true
      
       **{attitude_of_shopkeeper == 0 }[那你能介绍安全路径吗？]#speaker:

        他朝你摆摆手。周围的烟气被他扇去了。
        别问我，我没走过线。#speaker:老板
        你们都是有去无回，我哪知道死了多少？
        
        [感知检定] #check:WIS+1d8>medium
        
        {CHECK:
        - SUCCESS:他说得很难听，但你知道他只是在吓唬你。#speaker:
        - HUGE_SUCCESS:他说得很难听，但你知道他只是在吓唬你。就像随口讲个茶余饭后的笑话。#speaker:
        - FAIL:他仍是那副皮笑肉不笑的表情。你感觉脊骨发麻。#speaker:
        - HUGE_FAIL:他仍是那副皮笑肉不笑的表情。你感觉脊骨发麻。#speaker:
        }   
        
        **{attitude_of_shopkeeper > 0 }[那你能介绍安全路径吗？] #speaker:
        
        我没走过线。但要说安全，建议去人多的队伍。#speaker:老板
        也就收费高些、协调麻烦些，但花钱消灾，懂吧？
        他得意地挥了挥手里的烟。#speaker:
       
    *{know_shopkeeper_1_is_Chinese == true}[看在老乡的份上，能介绍安全路径吗？]
    
        我没走过线。但要说安全，建议去人多的队伍。#speaker:老板
        也就收费高些、协调麻烦些，但花钱消灾，懂吧？
        他得意地挥了挥手里的烟。#speaker: #Hunger:+2
    
    
    -{translator_you_employ == 0:
    我建议你到一个新地方就先找翻译。蛇头那儿就能叫几个。#speaker:老板
    他一边朝门外指指。#speaker:
    不包安全，有的是马货，但至少遇到本地人不会什么都不懂，被拐走了都晓不得。#speaker:老板
    哦——你也可以自己学。只要有闲心。
    }
       
    [感知检定]#check:WIS+1d8>formidable
    {CHECK:
    - SUCCESS:显然，他在拿你打趣。#speaker:
    - HUGE_SUCCESS:显然，他在拿你打趣。这似乎是他一贯的做法。#speaker:
    - FAIL:当然，你不太可能去学。#speaker:
    - HUGE_FAIL:当然，你不太可能去学。#speaker:
    }
    

   {translator_you_employ == 2:
   你注意到，便利店老板似乎一直盯着你和你的翻译。但他一直保持着那张笑脸，很难判断用意。#speaker:
   }
    
    
    
    {attitude_of_shopkeeper:
    -0: 行了，忙你的事情去吧。我也有我的活计要干。#speaker:老板
    这基本是下了逐客令。你只得站到几排寒酸的塑料架子跟前。#speaker:
    
    -1:祝你顺利，朋友。我有空也可以和你聊聊天。#speaker:老板
    至于充电器——你可以在我这儿买一个，有个二手的。但你知道，这不便宜。
    你有打算就和我说一声。
    说完，他开始招呼一名刚进门的客人了。#speaker:
    
    -else:你还真是有趣,朋友，我聊得很开心。
    这样——我这里有一个备用的充电器。你可以花大价钱买下它，但我也可以借给你，就在我的店铺门口充电。我只收充电的费用。
    随时找我吧，老乡。
    说完，他开始招呼一名刚进门的客人了。#speaker:
    
    }
       
    ~negative_event_1_complete = true //被动事件1结束
    
    ->position_shop_1
     
    -> END
