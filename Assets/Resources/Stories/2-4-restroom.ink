INCLUDE 0-global.ink

->position_restroom_1

=== position_restroom_1 ===

{ been_restroom_1 == 0:

与其说休息站，这里更像是一排带顶的木头椅子。偷渡客们或坐或躺在椅子上，两脚护着底下的大包小包。
不同肤色的孩子们在走道上奔跑、玩耍，他们还不知道自己从何处来——又将往何处去。


-else:

简陋的休息处。连当地人也知道，此处不宜久居。

}



{time<2h && not meet_shen_2:

你找了一阵，莘先生不在这里。应该如先前所说，他七点才会在这儿歇脚。
}



{time>=2h && not meet_shen_2:


你瞧见了那顶熟悉的帽子。#speaker:
->negative_event_3

}
 
*离开。

->ShoreVillage

= negative_event_3

#time:20min

~meet_shen_2 = true

 *又见面了，莘先生。
 他仍然保持着船上那个舒适的姿势：两腿分开，把背包夹在里头；身体则靠在椅子上。
 {translator_you_employ == 1:
 
 莘先生…… #speaker:翻译
 翻译也嘀咕着他的名字。#speaker:
 
 }
 
 
 老兄，亏你能找到我。#speaker:莘先生
 他忽地抬起头，朝你笑笑。#speaker:
 
 

-> END
    -> END