INCLUDE ../0-global.ink

#title:休息站

->position_restroom_1

=== position_restroom_1 ===

{ been_restroom_1 == 0:

与其说休息站，这里更像是一排带顶的木头椅子。偷渡客们或坐或躺在椅子上，两脚护着底下的大包小包。
不同肤色的孩子们在走道上奔跑、玩耍，他们还不知道自己从何处来——又将往何处去。


-else:

简陋的休息处。连当地人也知道，此处不宜久居。

}


*{meet_shen_2 == false}[寻找莘先生]

你瞧见了那顶熟悉的帽子。#speaker:
->negative_event_3

*[离开]->END

= negative_event_3

#time:20min

~meet_shen_2 = true

 *[又见面了，莘先生。]
 -他仍然保持着船上那个舒适的姿势：两腿分开，把背包夹在里头；身体则靠在椅子上。
 {translator_you_employ == 1:
 
 莘先生…… #speaker:翻译
 翻译也嘀咕着他的名字。#speaker:
 //翻译认出他似乎不是第一次来此处。
 
 }
 
 
 老兄，亏你能找到我。#speaker:莘先生
 他忽地抬起头，朝你笑笑。#speaker:
 {怎么说，有打算了吗？|}#speaker:莘先生
 
 *[我打算现在就走。]#speaker:
 
 对方似乎愣了一下。
 现在？现在已经傍晚了，朋友。我建议挑个好时间出发。#speaker:莘先生
 
    **[你说的没错。]
    **[你有其他想法吗？]
 
        我打算明天中午动身。
 
    **{know_the_travel_time_1}[路程有好几天。我们总得在外边过夜，哪天都一样。]#dice:INT+1d8>easy
 
 *[我准备之后再走。]
 
 那正好。我打算明天或者后天动身。
 
 -方便的话，我们可以组个队，免得在路上遇到麻烦。听说有人烟的地方劫匪和小偷也多。
 你觉得如何？
 
 *[没问题。]#speaker:
 很好。我们明天中午在这儿碰头吧。#speaker:莘先生
 我今晚都在这儿休息，你随时可以再来找我。
 
 *[我再考虑考虑。]#speaker:
 行，我们都有考量。#speaker:莘先生
 我明天中午走，你随时可以来找我。
 
 -
 *[再见。]#speaker:
 -
 +[继续]-> END