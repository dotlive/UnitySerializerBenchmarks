# UNITY Serializer 横向比较
关于序列化，无论是.net还是unity自身都提供了一定保障。然而人总是吃着碗里想着锅里，跑去github挖个宝是常有的事。看看各家大佬的本事。最有趣的就是每个开源库首页上各个都有吊打隔壁的意思。

## 测试结果

测试环境 
* Intel(R) Core(TM) i5-7500 CPU @ 3.40GHz 
* 16.0 GB 
* Unity 2018.3.9f1 .NET 4.x
* 测试均为Windowns Standalone Release配置

| 开源库 | 序列化 | 反序列化 | 继承特性 | il2cpp | il2cpp序列化 | il2cpp反序列化 | 需要外加代码或标签 | 高级特性 |
| - | - | - | - | - | - | - | - | - | 
| BinaryFormatter | 80 | 80 | yes | yes | 65 | 58 | no | .NET自带 |
| JsonUtility | 16 | 31 | no | yes | 20 | 16 | no | Unity自带 |
| OdinSerializer | 22 | 48 | yes | yes | 211 | 244 | no | Unity.Object也能参与序列化 |
| MessagePack-CSharp | 1 | 3 | no | no | N/A | N/A | yes | 据说可生通过标记配合模板成代码进行il2cpp |
| NetSerializer | 5 | 10 | yes | no | N/A | N/A | yes | MPL协议谨慎 |
| Newtonsoft.Json | 25 | 29 | yes | no | N/A | N/A | no | |
