INCLUDE ../0-global.ink

->beginning

=== beginning ===

山坡朝下，你和一团队伍走着并不能被称之为“路”的狭窄通道，正谨慎地下行。#speaker:

[灵巧检定]#check:DEX+1d8>medium
{CHECK:
    - SUCCESS:于你而言，路线不算太过崎岖。相较于地面，你更在意前方是否会遇到毒虫和劫匪。
    - HUGE_SUCCESS:所幸，这样的泥泞路子于你而言并不算崎岖。相较于地面，你更在意前方是否会遇到毒虫或是劫匪。你的步速是如此之快，几乎与领头齐平。他不时看你几眼。
    - FAIL:你走得并不平稳。你能感觉到鞋子在泥潭一样的下坡路段打滑，整个身体只得借助四周的树干，或是旁人的搭手。想到前方更可怕的路途，你打了个寒颤。#SAN:-1
    - HUGE_FAIL:你走得并不平稳，几乎每行几步就会打滑——甚至一度摔入一片泥泞，整个队伍都因此停下来等待一阵。疼痛之余，你感到羞愧难当。#HP:-1 #SAN:-1
    }

这里是名副其实的热带丛林：树木遮天蔽日，潮湿与腐烂的气味涌入鼻腔。道路没有阳光滋养，也没有你熟悉的寻常矮灌，唯有青苔在岩壁与树干上厚厚生长，爬到及人高的位置，令你无从倚靠。
泥泞仿佛没有尽头，你的精神也在这片湿气与阴影之中恍惚。径流与峭壁有阳光洒下，可永远伴随更为凶险的自然环境。
总有偷渡者或走线客越过这片丛林。你想到。他们或与你一样借助当地人的力量，或只凭着远在天边的定位卫星在这片森林中摸索。
无论如何，你已经来到此处：阿坎迪雨林，凶险旅途的开端。

+[白天] ->day
+[黑夜] ->night


=== day ===

+[抢劫] ->robbery
//+[邂逅]
//+[求助]
//+[争执]
+{CHECK == HUGE_SUCCESS}[与领队谈话] ->talk_with_leader   


-->END


= robbery

领队做了个手势，队伍停将下来。你看到他变了个脸色。

{know_the_rob == true:
->know_the_rob_t
-else:
->know_the_rob_f
}

-(know_the_rob_t)

[感知检定]#check:WIS+1d8>medium
{CHECK:
    - SUCCESS:你听到一些响动。那声音直冲着队伍而来，毫无掩饰之意。 
    - HUGE_SUCCESS:你听到一些响动。那声音直冲着队伍而来，毫无掩饰之意。那分明的其他人的响动——来者不善。
    - FAIL:你并没有理解他的意思。
    - HUGE_FAIL:你并没有理解他的意思。
    }
->rob_0_1
    
-(know_the_rob_f)

[感知检定]#check:WIS+1d8>easy
{CHECK:
    - SUCCESS:你听到一些响动。那声音直冲着队伍而来，毫无掩饰之意。 
    - HUGE_SUCCESS:你听到一些响动。那声音直冲着队伍而来，毫无掩饰之意。那分明的其他人的响动——来者不善。
    - FAIL:你并没有理解他的意思。
    - HUGE_FAIL:你并没有理解他的意思。
    }
->rob_0_1
    
-(rob_0_1)

领队向前走了几步。从草丛里现身的是一小群皮肤黝黑的“居民”，与你此前所见的别无二致——若不是他们身上都挂着你非常熟悉，又异常陌生的东西：步枪。#SAN:-1

{CHECK == FAIL || CHECK == HUGE_FAIL:

你终于明白过来：这是一场抢劫。紧随着这一念头，恐惧随之而来。#SAN:-1

}

你在内心暗骂领队：他本向所有人担保这是一趟安定的旅途。

[智力检定]#check:INT+1d8>easy
{CHECK:
    - SUCCESS:或许是当地势力默默圈大了一块地盘。
    - HUGE_SUCCESS:或许是当地势力又默默圈大了一块地盘，甚至更坏：领队本就是他们的人。
    - FAIL:你看着那群劫匪，一时间不敢吭声。
    - HUGE_FAIL:你看着那群劫匪，一时间不敢吭声。
    }

领队向你们交待事由，建议所有人立刻把身上的财物都丢出来。

[智力检定]#check:INT+1d8>easy
{CHECK:
    - SUCCESS:你知道，无论男女，下一步就是搜身了。除了事前准备，或许你也可以想办法让他们相信自己没有被搜索的价值。
    - HUGE_SUCCESS:你知道，无论男女，下一步就是搜身了。除了事前准备，或许你也可以想办法让他们相信自己没有被搜索的价值。
    - FAIL:你知道，事到如今也只能照做了。
    - HUGE_FAIL:你知道，事到如今也只能照做了。
    }

你主动把荷包都翻了个底朝天，里面空空如也；随后是你的背包。一个男人走过来，你向他展示只装了日常用品和一个小钱包的外层。#MON：-1
他指了指你的大包——你的大部分财物和衣物所在的位置，随后伸出手，眼看着就要夺过来。

+{CHECK == SUCCESS || CHECK == HUGE_SUCCESS}（主动展示衣物）

你想主动展示衣物——至少把真正藏着钱和重要物品的夹层掩在褶皱底下。

[灵巧检定]#check:DEX+1d8>medium
{CHECK:
    - SUCCESS:你成功抢先一步，在他得手之前就把衣服一件件摊在手中，迅速过了一遍。你希望这没有引起他的疑心。
    - HUGE_SUCCESS:你成功抢先一步，在他得手之前就把衣服一件件摊在手中，迅速过了一遍。他似乎根本没有反应过来——也迅速对你失去了兴趣。
    - FAIL:可这没有奏效。他一把夺过背包，就地检查起来。#MON:-5
    - HUGE_FAIL:可这没有奏效。你掩饰的动作显然惹恼了他，他一巴掌扇在你脸上，随后指了指自己身上的枪。他就地检查起来。#HP:-1 #MON:-5
    }

+（把所有东西倾倒出来）

你抢在他之前把所有东西都倾倒在地上。衣服都黏上了泥土——但对于真正重要的东西来说，它们无足挂齿。
对方显然有些生气，俯身检查那些衣物。你低着头，见一件缝了金钱的羽绒服躺在你脚边。
把这堆衣服搞得一片乱麻——你想道，或许还有机会。

    ++（尝试把检查与没检查的衣物混到一团）

    [灵巧检定]#check:DEX+1d8>godly
    {CHECK:
        - SUCCESS:你成功抢先一步，装作走动的样子，实际将两团衣服踢到了一起。对方看了你一眼——但似乎没有起疑心。
        - HUGE_SUCCESS:你成功抢先一步，装作走动的样子，实际将两团衣服踢到了一起。对方看了你一眼——但似乎没有起疑心。
        - FAIL:可这没有奏效。他把衣服夺将过去，就地检查起来。#MON:-5
        - HUGE_FAIL:可这没有奏效。你掩饰的动作显然惹恼了他，他一巴掌扇在你脸上，随后指了指自己身上的枪。他就地检查起来。#HP:-1 #MON:-5
        }


    ++（不做冒险）
    你看着那些摇晃的步枪，还是没有出手。他搜走了你衣服边边角角的钱财——毕竟他们以此谋生。#MON:-5


+（让他检查）
你老实交出了手里的背包——比起险境，当下才是你最需要注意的问题。你看着那几杆枪，仍然有些胆寒。#MON:-5


-半晌，你看着那些劫匪沾沾自喜地走入森林，留下一地狼藉。
领队重振队伍，有几个人骂骂咧咧地走出了道，显然不再相信这名犯错的导游。
你不知道此刻应该为劫掠感到无助，还是为自己留得一命而感到庆幸。唯有这种时刻，你会隐约对未来感到不安。

->beginning

= talk_with_leader

{在你旁边，领队经常回头观望是否有人掉队。他几乎不喘大气，熟练地用棍子拨开前路，仿佛只是在玩乐而已。很快，领队注意到你的眼神，示意样地点了下头。|他从鼻腔出了口气，继续盯着你。}

想问什么？#speaker:领队

-(talk_with_leader_1)

+[你见的中国人多吗？]

不多。大多是，更南边来的。你们不一样。#speaker:领队
他磕磕巴巴地说着中文，对你投来——尽力的——友好的目光。#speaker:

[智力检定]#check:INT+1d8>easy
{CHECK:
    - SUCCESS:这也难怪。走线客越来越多，当地人已经把中国人视作摇钱树。中文是业务的一环——也自然希望你能因此给出更多报酬。他尽力敛着焦急——却也急不可耐。
    - HUGE_SUCCESS:这也难怪。走线客越来越多，当地人已经把中国人视作摇钱树。中文是业务的一环——也自然希望你能因此给出更多报酬。他尽力敛着焦急——却也急不可耐。
    - FAIL:你看不出他究竟是耐心还是烦躁。
    - HUGE_FAIL:你看不出他究竟是耐心还是烦躁。
    }
    
到了地方才能，收钱。所以，不用担心。#speaker:领队

->talk_with_leader
    

+[你经常走这条路？]#speaker:

我带过二十几次队，没事故。#speaker:领队
这条路也就五六次，还不是特别，熟练。

    ++[路线不一样吗？]#speaker:
    
    不一样。这里都是打劫。一样的太久，就会被截。#speaker:领队
    
    [智力检定]#check:INT+1d8>formidable #speaker:
    {CHECK:
        - SUCCESS:是很有道理。不过雨林不是哪里都宜居，就算劫匪也会有据点。向导们或多或少有自己的情报渠道。
        - HUGE_SUCCESS:是很有道理。不过雨林不是哪里都宜居，就算劫匪也会有据点。向导们或多或少有自己的情报渠道。
        - FAIL:是很有道理。想到那群拿着枪的人，这段路忽然也变得和蔼可亲了。
        - HUGE_FAIL:是很有道理。想到那群拿着枪的人，这段路忽然也变得和蔼可亲了。
    }
    
    --(talk_with_leader_2_1)
    
        +++{CHECK == SUCCESS || CHECK == HUGE_SUCCESS}[土匪都在哪活动？]
        
        靠河。如果看到，有屋子在河边，就要远离。他们声音很大。#speaker:领队
        我们有段路会度过，我到时候说。
        显然，他是指有一段路很可能会遇上那群土匪。你最好提前做准备。#speaker:
        ~know_the_rob = true
        
        ->talk_with_leader_2_1
        
        +++[劫匪都是什么人？]
        
        有些是犯过罪的。他们最狠，把人关进去，然后等赎金，赎不起的卖给西边。那边有人收。#speaker:领队
        还有村子，一整个村。他们抢过，觉得好，就一直抢。但也不杀人。
        
        ->talk_with_leader_2_1
        
        +++[了解了，谢谢。]
        
        ->talk_with_leader
    


+[还有多久才到？]

这才开个头。还要两天两夜。#speaker:领队
我们要在林子里睡两晚，晚上很冷，要找个平的地方。靠着树睡也可以，塞点衣服，暖和得多。#speaker:领队
~know_the_sleep_skill_1 = true

->talk_with_leader

+[没什么。]

对方也没有多做声，只是继续往前走了。

-->beginning

=== night ===

+[独白] ->dialoge
//+[偷窃]
//+[袭击]



-->END

= dialoge

{know_the_sleep_skill_1 == true:
营火在慢慢淡去。你枕着睡袋的余温，平静地进入梦乡。
-else:
你躺在冰冷的睡袋里，周围黑洞洞的,寒意浸入你的脊骨。你只得等着困意将你拖入梦乡。#SAN:-1
}

->beginning