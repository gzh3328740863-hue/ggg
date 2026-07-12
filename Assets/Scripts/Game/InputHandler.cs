using UnityEngine;

/// <summary>
/// 输入处理器 - 负责处理玩家的输入
/// </summary>
public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }

    // 输入事件
    public delegate void JumpInputEvent();
    public delegate void MoveInputEvent(float direction);

    public event JumpInputEvent OnJumpInput;
    public event MoveInputEvent OnMoveInput;

    private float horizontalInput = 0f;
    private bool jumpPressed = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameActive) return;

        // 获取水平方向输入
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // 获取跳��输入
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }

        ProcessInput();
    }

    private void ProcessInput()
    {
        // 处理跳跃
        if (jumpPressed)
        {
            OnJumpInput?.Invoke();
            jumpPressed = false;
        }

        // 处理移动
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            OnMoveInput?.Invoke(horizontalInput);
        }
    }

    /// <summary>
    /// 获取当前的水平输入
    /// </summary>
    public float GetHorizontalInput()
    {
        return horizontalInput;
    }

    /// <summary>
    /// 检查是否有跳跃输入
    /// </summary>
    public bool HasJumpInput()
    {
        return jumpPressed;
    }
}
