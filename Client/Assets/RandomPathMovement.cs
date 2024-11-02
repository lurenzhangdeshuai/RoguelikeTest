using UnityEngine;
using System.Collections.Generic;

public class Mover : MonoBehaviour
{
    public List<Transform> waypoints; // 使用 List 便于管理目标点
    public float moveSpeed = 1.0f;
    private int currentWaypointIndex = -1; // 初始为 -1，表示尚未选择目标点
    private float pauseDuration = 0f; // 停顿时间
    private bool isPaused = false; // 是否正在停顿
    public Animator animator; // 动画组件
    private Quaternion targetRotation; // 目标旋转
    private float rotationSpeed = 5f; // 旋转速度

    // 定义动画 ID，包括所有非移动动画
    private int[] nonMovingAnimations = { 1, 2, 3, 4, 5, 7, 8, 16 }; // 非移动动画 ID

    void Start()
    {
        animator = GetComponent<Animator>();
        ChooseNextWaypoint(); // 随机选择第一个目标点
        targetRotation = transform.rotation; // 初始化目标旋转
    }

    void Update()
    {
        if (waypoints != null && waypoints.Count > 0)
        {
            // 如果正在停顿，更新计时器
            if (isPaused)
            {
                pauseDuration -= Time.deltaTime;
                if (pauseDuration <= 0)
                {
                    isPaused = false; // 停顿结束
                    ChooseNextWaypoint(); // 随机选择下一个目标点
                }
                // 平滑旋转到 180 度
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                return; // 退出，等待停顿结束
            }

            // 获取当前目标点的位置
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;

            // 移动到目标点
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 计算朝向目标点的方向
            Vector3 direction = (targetPosition - transform.position).normalized;
            if (direction != Vector3.zero) // 确保方向有效
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }

            // 判断移动方向并播放相应的动画
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f) // 仍在移动
            {
                if (direction.y > 0) // 向上移动
                {
                    SetInt(20); // Fly Up
                }
                else if (direction.y < 0) // 向下移动
                {
                    SetInt(21); // Fly Down
                }
                else // 水平移动
                {
                    SetInt(17); // Walk
                }
            }

            // 检查是否到达目标点
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // 如果当前索引需要停顿
                if (currentWaypointIndex == 1 || currentWaypointIndex == 3 || 
                    currentWaypointIndex == 6 || currentWaypointIndex == 7 || 
                    currentWaypointIndex == 8)
                {
                    isPaused = true; // 开始停顿
                    pauseDuration = Random.Range(1f, 3f); // 随机停顿时间

                    // 在停顿时调整转向为 180 度
                    targetRotation = Quaternion.Euler(0, 180, 0);

                    // 随机播放一个非移动的动画
                    int randomAnimationId = nonMovingAnimations[Random.Range(0, nonMovingAnimations.Length)];
                    SetInt(randomAnimationId);
                }
                else
                {
                    ChooseNextWaypoint(); // 随机选择下一个目标点
                }
            }
        }
        else
        {
            // 如果没有目标点，设置动画为IDLE
            SetInt(1); // 1代表静止的动画ID
        }
    }

    // 播放相应的动画
    // 动画 ID 对应关系：
    // 1: IDLE, 2: Yes, 3: No, 4: Eat, 5: Roar, 6: Jump, 7: Die, 
    // 8: Rest, 9: Walk, 10: Walk Left, 11: Walk Right, 
    // 12: Run, 13: Run Left, 14: Run Right, 15: Fire, 
    // 16: Sick, 17: Fly, 18: Fly Left, 19: Fly Right, 
    // 20: Fly Up, 21: Fly Down, 22: Fly Fire, 23: Damage
    public void SetInt(int id)
    {
        animator.SetInteger("animation", id);
    }

    private void ChooseNextWaypoint()
    {
        // 随机选择下一个目标点
        int nextIndex;
        do
        {
            nextIndex = Random.Range(0, waypoints.Count);
        } while (nextIndex == currentWaypointIndex); // 确保不会选到当前点

        currentWaypointIndex = nextIndex; // 更新当前目标点索引
    }
}
