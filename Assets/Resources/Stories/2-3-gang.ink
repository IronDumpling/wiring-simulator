INCLUDE 0-global.ink

=== position_smuggler_1 ===
    
    {been_smuggler_1 == 0:
    一群穿着旅行套的人集中在五颜六色的旗帜前。看着这幅景象，你想起旅游风景区的游客。有些蛇头揽不着人，像黑车司机一样东拉西扯。
    你看不出他们的区别——只能碰碰运气，或者想别的办法。
    
    
    ~been_smuggler_1 ++
    
    -else:
    
    你看着闹哄哄的人群。有时候，走线的成败在此一举。
    
    }
    
    *找团队大一点的蛇头。
    一个接近二十人的团队吸引了你的注意。队伍闹哄哄的，显然是推搡着蛇头赶快出发；但他还想再揽些客源。就在此时，他也瞧见了你。
    
    {NPC4}- 要加入吗？马上，就出发了。
    他磕磕绊绊地说着中文，带着浓烈的地方口音，仿佛有块粘牙的糖。有几名偷渡客似乎按捺不住，朝他吼了几声。他没有回头。
    {NPC4}- 赶快，快。
    
     **路程有多久？
        
    {NPC4}- sa天。
    他伸出三根手指。你知道他是说“三天”。
    
     ***多少钱？
     
    他给你比了个数。（金钱-3）
    {NPC4}- 安全。包安全。
    他继续吃力地比划着。
     
     {not negative_event_3_complete:
     你忽然想起那个戴鸭舌帽的男人——你还没和莘先生联系。
     但走线客的分别本就是常态。先走一步，或许也可以在之后碰头。
     }
     
     ****好，现在就走吧。
     {translator_you_employ > 0:
     
         你和翻译攀谈几句，便就此别过。
     
     ~translator_you_employ = 0
     
     }
     
    //  ->leave_ShoreVillage  
     
     ****我还没准备好。下一班是什么时候？
    {NPC4}- 都有，每天都有。
    似乎是意识到你不打算即刻加入，他很快转过身，挥了挥手里的旗子。人群跟着他朝道路尽头走去，聚落转瞬间变得空荡不少。
    你忽然明白，加入这种队伍无疑是安全的——但是会多花不少时间。
     ->position_smuggler_1
    
    +找人少的蛇头。
    
    {not negative_event_3_complete:
     你忽然想起那个戴鸭舌帽的男人——你还没和莘先生联系。
     但走线客的分别本就是常态。先走一步，或许也可以在之后碰头。
     }
     
     ++好，现在就走吧。
     {translator_you_employ > 0:
     
         你和翻译攀谈几句，便就此别过。
     
     ~translator_you_employ = 0
     
     }
     
    //  ->leave_ShoreVillage 
     
     ++我还没准备好。
     
    似乎是意识到你不打算即刻加入，他很快转过身，挥了挥手里的旗子。
     ->position_smuggler_1
    
    
    ->position_smuggler_1
    
    +雇佣一名翻译。
    ->employ_translator
    
    
    +离开。
    ->END
    

->END


  = employ_translator
  
  {TURNS_SINCE(-> employ_translator) == 0 :
  即使蛇头不懂中文，一般也会有翻译作为中介，并拿走部分抽成。这里并不缺翻译——不如说，他们总会自己找上门来。
  
  {translator_you_employ > 0:
  你原先的翻译看着你，耸了耸肩。
  }
  
  }
  
  
  
  {shuffle:
    -{translator_you_employ != 3:
    一个高大的当地人用英文向你问话。你和他谈了几句，大概能明白对方的意思。他的中文并不算好，但人很宽厚，开价也很便宜。你觉得他很适合当“观光客”的导游——而不是走线客的。
    
    ~translator_you_meet = 3
    
    -else:
    你没有找到。
    {NPC5}- 你可以再逛逛……我熟人多。
    你原先的翻译接着推荐自己。
    
    ~translator_you_meet = 3
    
    }
    -{translator_you_employ != 2:
     一名精瘦的男子同时和几个人谈着话。似乎是生了气，他甩开几人，径直朝这边走来。他用极度流利的中文向你介绍自己：他是名翻译，也可以带蛇头的工作，不过要价更高。
     
    ~translator_you_meet = 2
    
     -else:
    你没有找到。
    {NPC5}- 你也可以问我走线的事。
    你原先的翻译接着推荐自己。
    
    ~translator_you_meet = 2
    
    }
    -{translator_you_employ != 1:
     -一名黄种人朝你挥手。他也是一名走线客，不过因为金钱不够，被迫留在此处打工。为了验证自己的能力，他将你的话转述给当地人——非常准确，当然，价钱也很昂贵。
     
    ~translator_you_meet = 1
    
     -else:
    你没有找到。
    {NPC5}- ……我需要干点别的吗？
    
    ~translator_you_meet = 1
    
    }
  }
    
    
    +再找找。
    ->employ_translator

    +{translator_you_employ != translator_you_meet}雇佣他。
    
    {translator_you_employ > 0:
    你和原先的翻译作别。
    }
    
    {translator_you_employ == 0:
    翻译站到你的旁边。你不太习惯说话都要依靠别人的感觉——仿佛嘴巴不是自己的。
    
    }
    
    ~translator_you_employ = translator_you_meet
    
    
    +该打住了。
    
    -->position_smuggler_1
    
    


  = negative_event_2

-> END
