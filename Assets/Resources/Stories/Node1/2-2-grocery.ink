INCLUDE ../0-global.ink

-> position_shop_1

=== position_shop_1 ===

  {been_shop_1 == 0:
   刚走进门，你就瞧见了一个木头柜台——还有坐在后面的黄种人老板。柜台上放了两个大插排，上面密密麻麻地接了一堆电子设备。电器的主人们就站在门外，一边盯着，又一边打着呵欠。#speaker:
   整个商铺人员稀少，或许是少有人选择在此处停留——毕竟过了雨林，就是可以直达边境的集结点。
   你准备挑选东西，一个熟悉的身影又闯进你的视野。是那个曾经想要偷你东西的矮个子。他在货架后盯着你。
   你们的目光对视一瞬，随后，他径直从你身旁经过，又咕哝一句你听不懂的话。 #time:20min
   
     ~been_shop_1 ++
     
     ->negative_event_1
   
   -else:
   空旷的货架——还有戴着墨镜的便利店老板。
   
  }
    
    +看看食品架。#speaker:
    ->position_shop_1
    +看看药品架。#speaker:
    ->position_shop_1
    +更换货币种类。#speaker:
    ->position_shop_1
    +离开吧。#speaker:
    你走出这栋破旧的木屋。等候设备充电的人们瞥了你一下，随即又垂下眼眸。你同他们一样疲惫。
    
    ->END

  = negative_event_1
    
    ~temp quick_time = false
    
    *{translator_you_employ > 0}（向翻译询问）他说什么？#speaker:
    俚语，意思是“傻瓜”。#speaker:翻译
    
    {translator_you_employ == 1 || translator_you_employ == 2 :
    听起来像本地口音，应该不是走线的。这种人最危险。#speaker:翻译
    
    ~quick_time = true
    
    }
    
    {translator_you_employ == 3:
    ……或者“笨蛋”。#speaker:翻译
    他若有若无地补上一句，似乎想纠正词语的意思。你感觉蠢透了。#speaker:
    }
    
    **{not quick_time}有些不对……#speaker:
    好一阵，你才想到检查自己的随身物品。你就地脱下背包，开始翻看里边的东西。很快，你感到一阵眩晕：一整套充电器和电池都不翼而飞。
     ~SAN--
    你冲出门，而那个矮个子已经消失在人群里。
    
      ***回到店里。
    
    
    **{quick_time}迅速检查自己的背包。#speaker:
    你就地脱下背包，开始检查里边的东西。很快，你感到一阵眩晕：一整套充电器和电池都不翼而飞。
     ~SAN--
    你冲出门，见那矮个子正站在一名蛇头的边上。
      
      ***跟上去。
    //   ->negative_event_2
      
      ***回到店里。
    
   
    
    *{not quick_time}有些不对……#speaker:
    好一阵，你才想到检查自己的随身物品。你就地脱下背包，开始翻看里边的东西。很快，你感到一阵眩晕：一整套充电器和电池都不翼而飞。
     ~SAN--
    你冲出门，而那个矮个子已经消失在人群里。
    
      ***回到店里。
   
    招惹本地人了？ #speaker:老板
    老板忽然向你发话。他扶了扶自己的墨镜，你能感受到那张干巴面孔上的笑意。#speaker:
    那你得注意点，指不定哪天就被介绍到某个黑蛇头那边，然后把你卖了。#speaker:老板
    他们最喜欢的就是中国人，身价高，还好赎。
     说着，他点了支烟。#speaker:
     
     **他偷了我的东西！#speaker:
       那大概是拿不回来了。或者你还记得他的样子，可以去问问蛇头。#speaker:老板
       不过对咱们来说，这群人都长得千篇一律的，都不知道怎么描述。
       他装模作样地叹了口气。#speaker:
       难办哦。#speaker:老板
     
     **（瞪着他）#speaker:
       哦，哦！别急，老乡，我没看到作案过程。#speaker:老板
       他的语气带着嘲讽。#speaker:
       说不定早被偷了，只是现在才发现。别误会，我是同情你的。#speaker:老板
     -- 
     **……你是中国人？#speaker:
      我一直在说中文，还是黄皮肤——你觉得呢？#speaker:老板
      他向后躺到了椅子上。#speaker:
     
     {translator_you_employ == 0:
       
       我建议你到一个新地方就先找翻译。蛇头那儿就能叫几个。#speaker:老板
       他点了根烟，一边朝门外指指。#speaker:
       不包安全，有的是马货，但至少遇到本地人不会什么都不懂。#speaker:老板
       你也可以自己学。只要有闲心。
       显然，他在拿你打趣。#speaker:
       
       }
      
       ***你能介绍安全路径吗？#speaker:
       他朝你摆摆手。周围的烟气被他扇去了。
       别问我，我没走过线。#speaker:老板
       你们都是有去无回，我哪知道死了多少？
       他仍是那副皮笑肉不笑的表情。你感觉脊骨发麻。#speaker:
    
      {translator_you_employ == 2:
    
      你注意到，便利店老板似乎一直盯着你和你的翻译。但他一直保持着那张笑脸，很难判断用意。#speaker:
    
      }
    
       行了，我忙我的了。#speaker:老板
       这基本是下了逐客令。你只得站到几排寒酸的塑料架子跟前。#speaker:
       
       ~negative_event_1_complete = true //被动事件1结束
    
     ->position_shop_1
    -> END