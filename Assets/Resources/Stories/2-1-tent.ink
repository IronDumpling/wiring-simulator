INCLUDE 0-global.ink

->position_tent_1

=== position_tent_1 ===

{been_tent_1 == 0:
待你走近了，你才看清那顶帐篷上的红色十字：它已经被泥巴糊成灰黄。按你的经验，这应当是无国界医生的帐篷。#speaker:
你走进去，见前台正坐着一名白人女子。她瞥你一眼，给你指指桌上的一沓文件。
你跟着过去看看：那是份纸质表格，上面密密麻麻地写了两列。左边一栏混杂着各种语言，大多是拉丁文，还有些简洁的中文汉字，显然都是姓名；右边则是日期，即便国籍和语言差异巨大，这些阿拉伯数字仍然整齐划一地标注着今日。

~been_tent_1 ++


-else:

有些透风的帐篷里边，三个人安静地做着工作。 #speaker:
~been_tent_1 ++

}


{translator_you_employ == 3 && not know_the_tent_post_1 && get_the_aid_1:
你那粗犷的翻译忽然眼神一亮。 #speaker: time:10min
我认识他们！ #speaker:翻译
他高声说着，连前台的志愿者都抬起头。随即，他朝前几步，开始与那两人攀谈。 #speaker:
他们说话的声音越来越小，都低着头，不时朝你投来一瞥。
很快，你的翻译又走到你旁边。
他们说做腻了。如果你想，可以做他们的岗位，能赚钱。 #speaker:翻译
说完，他有些得意地站回到一旁。 #speaker:
~know_the_tent_post_1 = true

->position_tent_1
}




*{not know_doctor_1_can_speak_Chinese}（向女人搭话）你会说中文吗？ #time:10min
->NPC_doctor_1

+{know_doctor_1_can_speak_Chinese}（向女人搭话）我想问问。
->NPC_doctor_1

+{konw_you_can_buy_EOL}{not negative_event_4_complete}朝两名工作人员打手势。#time:10min

他们会过意，假意到帐篷外面与你谈笑——实际上是用药物和食品换了你的一张钞票。#money:1cost
~MON --
~buy_the_EOL ++

->position_tent_1

*填上个人信息 #time:10min

+离开
->END

-你麻利地填上信息，很快，旁边的两个黑人靠过来。他们冲你笑笑，给你递了一大一小的两个盒子。
你端详了一下：大的是一盒干粮，小的则是预防热带病的药物。
（获得援助干粮、临时药品）
收下东西，你正要走出去，却发觉那两个人正用奇怪的眼光盯着你，似乎祈求着什么。
{translator_you_employ > 0 :
 
  这是在讨钱。 #speaker:翻译
  你的翻译提醒你。 #speaker:
 }



*给他们塞钱。（金钱-1）
~MON --
 你摸出一张皱巴巴的美元钞票。他们见状，果然都跟上来。
 你刻意避开那名前台的视线，在角落把钞票交给他们。两人对视一眼，又小心地塞了两个盒子给你。
 （获得援助干粮、临时药品）
 随即，他们小声地向你说了些什么，又指指物资。你听得不太清楚。
 
 
 
 {translator_you_employ == 0:
 他们又说了一遍。你确认自己没办法听懂，只能挥挥手，任凭他们回到岗位上。
 }

 {translator_you_employ == 1||translator_you_employ == 2 :
 你的翻译忽然也靠过来，几乎是凑到你耳边了。
 他们愿意用低价卖你这些物资，你可以再买。 #speaker:翻译
 他的声音同两名工人一样低。似乎见你理解了意思，工作人员都兴奋地笑笑，随后又回到岗位上了。#speaker:
 ~konw_you_can_buy_EOL = true
 }
 
 {translator_you_employ == 3:
 这名粗犷的翻译忽然眼神一亮。
 我认识他们！#speaker:翻译
 他高声说着，连前台的志愿者都抬起头。随即，他朝前几步，开始与那两人攀谈。#speaker:
 他们说话的声音越来越小，都低着头，不时朝你投来一瞥。
 很快，你的翻译又走到你旁边。
 他们说做腻了。如果你想，可以做他们的岗位，能赚钱。#speaker:翻译
 ~know_the_tent_post_1 = true
 
 他顿了一下，似乎在犹豫什么，脸也红起来了。#speaker:
 你还可以花钱，买这些物资。他们愿意卖。#speaker:翻译
 他的声音同两名工人一样低。似乎见你理解了意思，他们都兴奋地笑笑，随后又回到岗位上了。#speaker:
 ~konw_you_can_buy_EOL = true
 }
 


*给他们竖个拇指。
他们看到你的举动，先是面面相觑——又摇了摇头。
你听见志愿者朝他们喊了什么，两人又转过身，投入了下一轮工作。

{translator_you_employ == 1:

  别在意。这种讨钱的到处都有，总不能都给好处。#speaker:翻译
  他朝你摆了摆手。#speaker:
}

{translator_you_employ == 2:

  说实话，有点出乎我的意料…… #speaker:翻译
  他的笑容僵在脸上——他想笑，却不敢出声。#speaker:

}

{translator_you_employ == 3:

  哎呀，希望他们别在意。#speaker:翻译
  他轻叹口气，显然是在惋惜——但不是对你，而是对里边的两个。#speaker:
}

你尴尬于这场失败的交流。这甚至算不上鼓励。
~SAN ++


*（喊话）嘿，兄弟，你们想要什么吗？
你刚喊出声，就听到旁侧的一阵响动。是那名志愿者。她站起身，打翻了身旁的一叠纸巾。
他们要干什么？#speaker:志愿者

{not know_doctor_1_can_speak_Chinese:
你愣了一下。在场并没有其他人——只有你们四个。#speaker:
先生？#speaker:志愿者
你张大了嘴。错不了，那几句流利的中文是从志愿者口中说出来的：一个怎么看都是欧洲面孔的白人。#speaker:
}

**没什么。#speaker:
那最好。这里物资是溢出的，但总不能倒卖。#speaker:志愿者
她瞥一眼旁边的工作人员——你看出来，比起厌恶，她眼神里更多的是悲悯。#speaker:
祝你顺利吧，先生。#speaker:志愿者
又有人走进来。志愿者没再看你，立刻便换了种奇怪的语言，开始与下一位难民交谈。#speaker:


**他们像是在向我求助。#speaker:
该死…… #speaker:志愿者
她转过头，用当地语言——发音极为狰狞，应当是什么粗口——朝那两人呼喊。黑人们没有回嘴，而是用堪称迁怒的眼光瞟了你一眼。你感觉很不舒服。#speaker:
~SAN--
志愿者很快又换了中文，显然是向你发话。#speaker:
小心点，他们经常倒卖库里的过期物资。在森林里用这些东西，可能要你的命。#speaker:志愿者
她皱了眉，轻轻叹了口气。#speaker:
帐篷里陷入一阵沉默。你意识到——该离开了。

-
*走出帐篷。
~ get_the_aid_1 = true
->END

=NPC_doctor_1

{know_doctor_1_can_speak_Chinese == false:

我会。#speaker:志愿者

~know_doctor_1_can_speak_Chinese = true

你发觉她疲惫地叹了口气。似乎是念在人少的份上，她才不得已向你回应。#speaker:
有什么需要帮助的吗？#speaker:志愿者

-else:
还要问什么吗？#speaker:志愿者
}

+你是政府的志愿者？#speaker: #time:10min
不，我不是政府的人。那些人在集结点。#speaker:志愿者
这里是私人设置的援助区域。我和那边两个当地人一样，都是雇来的。
她朝帐篷出口偏了偏头。你看到那边的两个黑人正在整理小山般的盒子。#speaker:

++私人的？#speaker:
没错，手术和治疗都要收费。你可以当成私人诊所。#speaker:志愿者
不过我们和政府有合作关系。每个月都有人道主义援助送过来。你登记一下，就可以免费领取食物和药品。
她说得很小声，又指了指桌上的表单。#speaker:

->NPC_doctor_1

+你知道我该怎么去集结点吗？#speaker: #time:10min
唉…… #speaker:志愿者
她重重地叹了口气，几乎是故意让你听到了。#speaker:
穿过雨林，到巴霍奇基托，然后坐船到拉哈斯布兰卡斯集结点。#speaker:志愿者
徒步，翻山，坐船，甚至坐飞机——随你怎么走。要么提前规划好，要么去问蛇头。求求你了，别来问我这种事，哪怕是拿我当消遣。
她明显已经很烦躁了。你能瞧见她浓重的黑眼圈。#speaker:

->NPC_doctor_1

+没什么了。#speaker:
那最好。#speaker:志愿者

-> position_tent_1

-> END
