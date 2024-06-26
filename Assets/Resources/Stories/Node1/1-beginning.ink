INCLUDE ../0-global.ink

->beginning

=== beginning ===

-随一阵颠簸，你周遭的声音忽然大了。你听见橡胶与石头摩擦的声音，前方传来一阵惊呼…… #speaker:爬虫脑
你在哪？你感受着自己的身体……它瘫软着，倚在某个摇摇晃晃的事物上。在你旁边，某种黏腻的液体正反复碰撞，发出“啵”样的声响。#continue:

// select_1

 * [很陌生]。->select_1_1
 * [是了——熟悉的海浪。]->select_1_2

=select_1_1

你还记得潮湿的触感，那是淅淅沥沥的雨水，清爽、流动。可你感受到了——几滴液体飞溅到你的脸上，它们是沉重的，仿佛要将你的面部拖曳下去……
鼻腔向你发出警告。那是股咸腥的气味。
~SAN--
->going_1

=select_1_2

你还记得海风的触感，潮湿、咸腥。在岸上走动时，那股气息总是刺激你的鼻腔，直至熟稔。
可一股独属于泥土的腥气混进来——它越来越近了。
你回忆中的沿岸逐渐模糊，直至变了颜色……成为一片红色泥泞，还有生长其上的茂密雨林。
->going_1

=going_1

——你在哪？你再次诘问自己。#continue
*[尝试睁开眼睛。]
-你睁开——又忽地眯了眼。
喑蓝色的深空中，太阳几乎缩成一个斑点，却比以往更加煞人。
你感受到你的皮肤，它先被汗给浸湿，又被晒干了水分，只留下一片油腻，甚至析出盐来。
你慢慢起身。
*[环顾四周。]
-这是艘蓝色的橡皮艇。船上有十来个人，大多是黑色皮肤。
你就坐在他们中间，听那些人用陌生的语言谈笑。白色的浪花在你身旁涌动，说话声与波涛此起彼伏，又被更为嘈杂的嗡鸣所遮掩——那是发动机的噪音。
忽然，你瞧见了旁边的中年男人。他有着和你一样的黄色皮肤，戴一顶卡其色的鸭舌帽。显然，他和你来自同一个国家。他抱着背包，两脚叉得很开，做着非常悠闲的姿势。
似乎是注意到你的目光，他轻轻挑了下眉。

VAR NPC1 = "偷渡客"
VAR know_about_OceanSleepTime = false
VAR know_about_NPC1comeform = false

->dialogue_1

=dialogue_1

-{怎么了？|还有问题吗？} #speaker:偷渡客
~temp question = 0
{know_about_OceanSleepTime:
~question ++
}
{know_about_NPC1comeform:
~question ++
}


//select_2
{

-question != 2: 

 *[我睡了多久？] #speaker:
 ~know_about_OceanSleepTime = true
 ->select_2_1
 
 *[你是从哪来的？] #speaker:
 ~know_about_NPC1comeform = true
 ->select_2_2
 
 -else:
 
 *[我们好像快到了。]#continue
 ->going_2
 }
 
=select_2_1
 {NPC1}- 四个小时了，兄弟。
 {NPC1}- 哈，你心可真够大的。你至少该跟我打声招呼。
  *[为什么？]
 话音刚落，他朝你挪了挪身子，一边谨慎地环顾四周。
-{NPC1}- 没看到周围吗？他们全都是南美*黑鬼*。这群人手脚脏得很。
{NPC1}- 你想想，这群人要去国外避难，除了讨口子，多半是做过脏活。
{NPC1}- 说难听点，有些就是尾随你来的，专门盯着你动手。
 
 *[你不能这么揣测别人。]//select_2_1_1
 
 --男人笑出声。他在阳光里眯了眼。
{NPC1}- 哈，兄弟，这只是*自由之路*半途，咱们还没到地方。前面还有好几个小国，连着一整个墨西哥。
{NPC1}- 别对他们抱太大同理心。我给你举个例子。
说着，男人小心地指了指前头。你循着看过去，见那里正并排坐着三个高大的男人，咧着嘴谈笑。
{NPC1}- 左边那个杀过人，中间那个是强奸犯。
 
 **[你怎么知道？]
 {NPC1}- 他们刚才就在谈这事，甚至炫耀。
 
 ***[你听得懂他们说话？]
 {NPC1}- 来之前做了点功课，但是没用。给点钱就能请个翻译。
 {NPC1}- 总之，这趟路比你想得凶险。

->dialogue_1_going_1


 *[感谢提醒，我会注意。]//select_2_1_2
 
 {NPC1}- 嗯哼。你都到这儿了，最好别功亏一篑。
 {NPC1}- 我听说不少死半道上的，要么被偷了，要么被骗到没钱。

->dialogue_1_going_1

 
 =dialogue_1_going_1
 
 -{NPC1}- 而且——你已经被偷过一次东西了。

*[什么情况？！]

-{NPC1}- 就在你睡觉的时候，后面那个小个子想捞你的手机。
-{NPC1}- 别急——他没偷着，我正好瞧见了，他没敢动手。
-他说着，几乎是戏谑样地朝小偷笑笑。矮个子撇了撇嘴，咕哝出一句你听不懂的语言。男人笑得更大声了。
-{NPC1}- 瞧，别在这儿睡着了，不然就等着徒步穿行半个大洲吧。
-他安静下来，又恢复了先前的姿势。

 ->dialogue_1
 
 =select_2_2
 {NPC1}- 我？中国人呗。还能是新加坡的不成？
 ->selector_2_2
 
 
 =selector_2_2
 {NPC1}- {怎么，想和老乡聊聊天了？|接着问吧。|还想问什么吗？}
 {他大方地笑起来，似乎是来了兴致。|他说着，一边打量着你。|}
 VAR know_about_NPC1way = false
 VAR know_about_NPC1name = false
 
 {-not know_about_NPC1name||not know_about_NPC1way:
 
//select_2_2
*[你叫什么名字？]
{NPC1}- 我姓莘，草字头，底下一个辛苦的辛。#speaker:莘先生
~ NPC1 = "莘先生"
~know_about_NPC1name = true

->selector_2_2

*[你走的哪条线？]
{NPC1}- 从土耳其到厄尔多瓜。沿途都有产业链，比想象中还方便。
{NPC1}- 前面走河道和陆路，到集结点以后，最后坐个车就能抵达哥斯达黎加。
{NPC1}- 这是最常用的路线了，稳妥一点。
~know_about_NPC1way = true

->selector_2_2

-else:

*[你为什么会走线呢？]
男人轻哼了一口气。
{NPC1}- 受不了体制呗。从上到下烂成一团，连大气都没法出一口。更何况……
他忽然噤声了。似乎是意识到自己戾气太重，他咳嗽两声，又恢复到先前的姿势，敛了些笑。
{NPC1}- 大概是这样。听说不少走线的和我一样，但更多是生活所迫。
{NPC1}- 那你呢？
->select_2_2_3
}

=select_2_2_3

*[国内生活过不下去。]
{NPC1}- 哦，那很正常。大环境如此，上升空间也就那样了。
他自顾自地摸了摸下巴。
{NPC1}- 狗急了还会跳墙。人要是被逼着走线，那估计过得是真够惨的。
{NPC1}- 就当我同情你吧，老兄。
~HP = HP + 9
~SAN = SAN + 3
~MON = MON + 5

*[我需要言论自由——哪怕一点。]
{NPC1}- 那和我一样，理解。都替他们抗压了，连骂几句的资格都没有？
{NPC1}- 据说前面会遇到记者。要是你家里没啥挂念，就可以对着镜头，狠狠地骂一通。
{NPC1}- 要是成功了，还能激励更多人出来哩。
~HP = HP + 4
~SAN = SAN + 6
~MON = MON + 7


*[我被国内封杀了，只能出来混。]
男人显然是愣住了。半晌，他拍了拍你的肩。
{NPC1}- 不容易啊，兄弟。一整个国家机器都拿来对付普通人，真是畜生。
{NPC1}- 那你更要到地方了。到时候摊牌，把证据发出来，那帮人估计急得跳脚，又拿你没办法。
{NPC1}- 我看好你。
~HP = HP + 6
~SAN = SAN + 6
~MON = MON + 5

-他朝你竖一个拇指。
~SAN ++
->dialogue_1


 
=going_2

 你瞧见远处的陆地逐渐清晰。莘先生也向着那边眺望。
 {NPC1}- 是要上岸了，又要忙活一阵……
 说着，他拉开背包外层，一只手伸进里边摸索。很快，男人手里多了两袋塑料封装的面包。他将其中一个递给你。
 {NPC1}- 过会儿有得忙活，现在就吃吧。
 
 *[谢谢。]#continue

 -获得消耗品：面包

（使用消耗品的教程）

随又一阵橡皮艇与石头摩擦的声音响起，小船靠上了海岸。你感觉身下一阵颠簸。船夫吆喝着偷渡者们下船。你和莘先生都起过身，稳稳地拿上自己的背包。
浅滩并不热闹，你只能远远望见零散的人影朝岸上走去。或许是海路过于凶险，鲜有人优先选择这条线路。
但更前边——当地的居民已经准备好了。

{NPC1}- 我先打听下消息，七点去休息站充会儿电。你可以再来找我，就当搭个伴。
{NPC1}- 建议你找个翻译，行动方便得多。蛇头那边应该就有不少。
{NPC1}- 就这样——祝你顺利。

 *[也祝你顺利。]
 *[再见。]
--> END
