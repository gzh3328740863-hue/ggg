# Unity 跑酷游戏 - 设置指南

## 1. 项目初始化

### 1.1 创建Unity项目结构
```
1. 在 Unity 中创建新项目 (2021.3 或更高版本)
2. 导入项目中的所有脚本文件
3. 确保以下文件夹存在:
   - Assets/Scripts/
   - Assets/Scenes/
   - Assets/Prefabs/
```

### 1.2 配置项目设置
```
Edit > Project Settings > Physics2D:
- Gravity: (0, -20)
- Default Material: 创建新的Physics2D Material，设置摩擦力为0
```

## 2. 创建游戏场景

### 2.1 创建基础GameObject
```
1. 创建空对象作为 GameManager:
   - 添加 GameManager.cs 脚本
   - 添加 ScoreManager.cs 脚本
   - 添加 InputHandler.cs 脚本
   - 添加 UIManager.cs 脚本
   - 添加 ObstacleSpawner.cs 脚本

2. 创建玩家 (Player):
   - 3D Sprite
   - 添加 SpriteRenderer 组件
   - 添加 BoxCollider2D 组件 (设置为 Trigger)
   - 添加 Rigidbody2D 组件:
     * Body Type: Dynamic
     * Gravity Scale: 2
     * Constraints: Freeze Rotation Z
   - 添加 PlayerController.cs 脚本
   - 添加 PlayerAnimator.cs 脚本
   - 添加 Animator 组件

3. 创建地面 (Ground):
   - 2D Sprite
   - 添加 BoxCollider2D 组件
   - 添加 Rigidbody2D 组件:
     * Body Type: Static

4. 创建障碍 (Obstacle):
   - 2D Sprite
   - 添加 BoxCollider2D 组件 (设置为 Trigger)
   - 添加 Rigidbody2D 组件:
     * Body Type: Dynamic
     * Is Kinematic: true
   - 添加 Obstacle.cs 脚本
   - 标签设置为 "Obstacle"
```

### 2.2 创建地面检测
```
1. 在 Player 下创建子对象 "GroundCheck"
2. 设置位置在玩家脚下
3. 在 PlayerController 中设置:
   - Ground Check: 选择 GroundCheck transform
   - Ground Layer: 选择 "Ground" 层 (需要创建此层)
```

### 2.3 配置层和碰撞
```
1. 创建层:
   - Player
   - Obstacle
   - Ground

2. 设置碰撞矩阵 (Edit > Project Settings > Physics2D):
   - Player 可与 Obstacle、Ground 碰撞
   - Obstacle 不与 Obstacle 碰撞
```

## 3. 创建UI

### 3.1 Canvas 设置
```
1. 创建 Canvas
2. 添加以下子对象:

   a. ScoreText (TextMeshPro):
      - 位置: 左上角
      - 内容: "Score: 0"
   
   b. HighScoreText (TextMeshPro):
      - 位置: 右上角
      - 内容: "High Score: 0"
   
   c. GameOverPanel (Image):
      - 颜色: 半透明黑色
      - 包含子对象:
        * GameOverTitle (Text): "Game Over"
        * FinalScoreText (TextMeshPro): 当前分数
        * HighScoreText (TextMeshPro): 最高分
        * RestartButton: 重新开始
        * MenuButton: 返回菜单
   
   d. PausePanel (Image):
      - 颜色: 半透明灰色
      - 包含子对象:
        * PauseTitle (Text): "Paused"
        * ResumeButton: 继续游戏
        * MenuButton: 返回菜单
```

### 3.2 配置UIManager
```
在 UIManager Inspector 中:
1. 设置所有 Text 组件引用
2. 设置所有 Panel 引用
3. 设置所有 Button 引用
```

## 4. 创建预制体

### 4.1 Player Prefab
```
1. 将 Player GameObject 拖到 Assets/Prefabs/
2. 保存为 Player.prefab
```

### 4.2 Obstacle Prefab
```
1. 创建 Obstacle GameObject (如上所述)
2. 将其拖到 Assets/Prefabs/
3. 保存为 Obstacle.prefab
4. 在 ObstacleSpawner 中设置 Obstacle Prefab 引用
```

## 5. 配置数值

### 5.1 PlayerController 参数
```
- Move Speed: 8
- Jump Force: 15
- Fall Multiplier: 2.5
- Low Jump Multiplier: 2
- Left Boundary: -5
- Right Boundary: 5
```

### 5.2 GameManager 参数
```
- Game Speed: 5
- Speed Increment: 0.5
- Speed Increment Interval: 10
```

### 5.3 ObstacleSpawner 参数
```
- Spawn Interval: 2
- Min Spawn Interval: 0.8
- Spawn Interval Decrease Rate: 0.05
```

### 5.4 ScoreManager 参数
```
- Points Per Second: 10
- Points Per Obstacle Avoided: 50
```

## 6. 测试游戏

```
1. 按 Play 按钮
2. 使用空格键跳跃
3. 使用 A/D 或左右箭头移动
4. 按 ESC 暂停/继续
5. 躲避障碍获得分数
6. 碰到障碍游戏结束
```

## 7. 常见问题

### 玩家不能移动?
- 检查 InputHandler 是否添加到场景中
- 检查 Rigidbody2D 是否配置正确
- 检查 PlayerController 脚本是否订阅了输入事件

### 玩家穿过障碍?
- 检查 BoxCollider2D 是否为 Trigger
- 检查碰撞层配置
- 检查 OnTriggerEnter2D 是否正确处理

### 分数不显示?
- 检查 Canvas 是否在场景中
- 检查 UIManager 中的文本组件引用
- 检查是否调用了 UpdateScoreDisplay

### 游戏速度不增加?
- 检查 GameManager 中的 speedIncrementInterval
- 检查 GameManager 是否正确更新游戏速度

## 8. 下一步改进

```
- 添加音效系统
- 实现不同的障碍类型
- 添加角色升级系统
- 创建关卡系统
- 添加排行榜
- 实现道具系统
- 添加粒子效果
```
